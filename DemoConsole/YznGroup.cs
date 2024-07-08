using DemoConsole.MyGroup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DemoConsole
{
    public class YznGroup
    {
        public static readonly int ERROR_ORDER_LOSE2 = Lose(1, 3);
        /// <summary>颠倒的序号的丢包数:如2,1</summary>
        public const int ERROR_ORDER_MAX = 4; 
        public static readonly int ERROR_ORDER_LOSE126 = Lose(2, 1);
        public static readonly int ERROR_ORDER_LOSEMAX = Lose(127, 127-ERROR_ORDER_MAX);
        const int DSECOND = 60000; //定义1分钟内的毫秒
        const int DELAT = 500; //0.5秒
        static Boolean HasErrorOrderMsgCnt = true;

        public static void Do()
        {
            int maxAllowLose = 4 * ERROR_ORDER_MAX;
            var ddd = JsonConvert.DeserializeObject<List<MyGroup.AnalysisSource>>(File.ReadAllText("c:\\4.json"));
            var lst = ddd.Select(x => x.No).ToList();
            var groupCont = 2;
            var dic= new Dictionary<int, List<int>>();
            var index = 0;
            foreach(var item in lst)
            {
                index++;
                //Console.WriteLine($"index={index} , item= {item}");
                var newKey = dic.Keys.Count;
                if (dic.Keys.Count == 0)
                {
                    dic.Add(0, new List<int> { item });
                } 
                else
                {

                    var minlostKey = 0;
                    var minlostValue = int.MaxValue;
                    var minTotalValue = int.MaxValue;
                    var minSwapCount = int.MaxValue;
                    foreach(var key in dic.Keys)
                    {
                        var lstClone = JsonConvert.DeserializeObject<List<int>>( JsonConvert.SerializeObject( dic[key]));
                        var startIndex = lstClone.Count - ERROR_ORDER_MAX - 1;
                        if (startIndex < 0) startIndex = 0;
                        lstClone.Add(item);
                        var swapCount= ReOrderAnalysisSourceByNo2(lstClone);
                        (int t,int ll)=  DoLoseAnalysis(lstClone);

                        if (ll < minlostValue)
                        {
                            minlostValue = ll;
                            minlostKey = key;
                        } else if (ll == minlostValue && swapCount < minSwapCount)
                        {
                            minlostValue = ll;
                            minlostKey = key;
                        }
                        if (swapCount < minSwapCount)
                        {
                            minSwapCount = swapCount;
                        }
                    }
                    if (minlostValue> maxAllowLose && dic.Count< groupCont)
                    {
                        dic[newKey] = new List<int> { item };
                    } else
                    {
                        dic[minlostKey].Add(item);
                        ReOrderAnalysisSourceByNo2(dic[minlostKey]);
                    }
                }
            }

            Console.WriteLine("Result:");
            foreach(var key in dic.Keys)
            {
                Console.WriteLine($"Group {key}  = "  +  string.Join(" , " , dic[key]));

               (int ii,int jj)=  DoLoseAnalysis(dic[key]);
                Console.WriteLine($"Total={ii} , lose= {jj}");
            }
        }

        


        /// <summary>
        /// 分析丢包数据
        /// </summary>
        /// <param name="lst">消息号列表</param>
        /// <param name="ret">更新分析结果</param>
        public static (int,int) DoLoseAnalysis(List<int> lst, int startIndex=0)
        {
#if DEBUG
            //Console.WriteLine("msgCnt = " + JsonConvert.SerializeObject(lst));
#endif
            if (startIndex < 0) startIndex = 0;
            if (lst.Count == 0) return (0,0);
            if (lst.Count() == 1)
            {
                return (1,0); //数据太少无法计算丢包率
            }
            else
            {
                if (lst.All(x => x == lst[0]))
                {//如果所有的msgCount都一样,总数并认为没有丢包,不然会影响频率计算(应该是rsu有问题才会造成这种情况)
                    return (lst.Count,0);
                }
                var total = 1; //总计实际发送包数
                var lose = 0;
                for (var i = startIndex; i < lst.Count()-startIndex - 1; i++)
                {
                    var y1 = lst[i];
                    var y2 = lst[i + 1];
                    if (y1 != y2)
                    { //如果上下两帧同一个编号,认为是rsu,重复发了同一帧
                        total++;
                        int loseThis = HasErrorOrderMsgCnt ? LoseRef(y1, y2) : Lose(y1, y2); //本次丢包数
                        if (loseThis > 0)
                        {
                            total += loseThis; //修正总计的消息数
                            lose += loseThis;   //总计丢包数
#if DEBUG
                            //Console.WriteLine($"  -- 异常丢{loseThis}包 位置:{i} ==" + string.Join(",", lst.Skip(i).Take(5)));
#endif
                        }
                    }
                }
                return (total,lose);
            }
        }

        public static int ReOrderAnalysisSourceByNo2(List<int> lstAnalysisSource, int iStartIndex = 0)
        {
            int swapCount = 0;
            if (!HasErrorOrderMsgCnt) return swapCount; 

            const int INVALID_POSITION = -1;
            for (var i = iStartIndex; i < lstAnalysisSource.Count - 1; i++)
            {
                var item1 = lstAnalysisSource[i];
                var item2 = lstAnalysisSource[i + 1];
                //Console.WriteLine($"位置:{i}= {item1}, {item2}");
                if (Lose(item1, item2) > ERROR_ORDER_LOSEMAX)
                {
                    // 从item1向前最多找4个位置,并且接收时间不超过DELAT秒
                    int findCount = 0;
                    int findPosition = INVALID_POSITION;
                    for (var j = i; j >= 0; j--)
                    {
                        findCount++;
                        if (findCount > ERROR_ORDER_MAX) break;
                        item1 = lstAnalysisSource[j];
                        if (Lose(item1, item2) >= ERROR_ORDER_LOSEMAX)
                        {
                            findPosition = j;
                        }
                    }

                    if (findPosition != INVALID_POSITION)
                    {
                        SwapAnalysisSource2(lstAnalysisSource, findPosition, i + 1);
                        swapCount += i - findPosition;
                    }
                }
            }
            return swapCount;
        }

        /// <summary>
        /// 如果出现MsgCnt颠倒的情况如(72,71)则把数据交换重排
        /// </summary>
        /// <param name="lstAnalysisSource"></param>
        public void ReOrderAnalysisSourceByNo(List<AnalysisSource> lstAnalysisSource, int iStartIndex=0)
        {
            if (!HasErrorOrderMsgCnt) return;
            
            const int INVALID_POSITION = -1;
            for (var i = iStartIndex; i < lstAnalysisSource.Count - 1; i++)
            {
                var item1 = lstAnalysisSource[i];
                var item2 = lstAnalysisSource[i + 1];
                Console.WriteLine($"位置:{i}= {item1.No}, {item2.No}");
                if (Lose(item1.No, item2.No) > ERROR_ORDER_LOSEMAX)
                {
                    // 从item1向前最多找4个位置,并且接收时间不超过DELAT秒
                    int findCount = 0;
                    int findPosition = INVALID_POSITION;
                    long? tmpRecTimestamp = null;
                    for (var j = i; j >= 0; j--)
                    {
                        findCount++;
                        if (findCount > ERROR_ORDER_MAX) break;
                        item1 = lstAnalysisSource[j];
                        if (Lose(item1.No, item2.No) >= ERROR_ORDER_LOSEMAX)
                        {
                            if (IsT1BeforeT2RecTimestamp(item1.RecTimestamp, item2.RecTimestamp) &&
                                (tmpRecTimestamp == null || tmpRecTimestamp != null && IsT1BeforeT2RecTimestamp(item1.RecTimestamp, tmpRecTimestamp.Value)))
                            {
                                tmpRecTimestamp = item1.RecTimestamp;
                                findPosition = j;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    if (findPosition != INVALID_POSITION)
                    {
                        SwapAnalysisSource(lstAnalysisSource, findPosition, i + 1);

                        Console.WriteLine(string.Join(",", lstAnalysisSource.Select(x => x.No)));

                    }
                }
            }
        }


        private void SwapAnalysisSource(List<AnalysisSource> lstAnalysisSource, int i, int j)
        {
            var item2 = lstAnalysisSource[j];
            var recTimestamps = lstAnalysisSource.Skip(i).Select(x => x.RecTimestamp).ToList();
            lstAnalysisSource.RemoveAt(j);
            lstAnalysisSource.Insert(i, item2);
            for (var k = i; k <= j; k++)
            {
                lstAnalysisSource[k].RecTimestamp = recTimestamps[k - i];
            }
        }

        private static void SwapAnalysisSource2(List<int> lstAnalysisSource, int i, int j)
        {
            var item2 = lstAnalysisSource[j];
            lstAnalysisSource.RemoveAt(j);
            lstAnalysisSource.Insert(i, item2);
        }


        /// <summary>
        /// 是否t1在t2的前面
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsT1BeforeT2RecTimestamp(long t1, long t2)
        {
            var delay = t2 - t1;

            if (delay < 0)
            {
                delay = DSECOND + delay;
            }
            return delay < DELAT;
        }


        /// <summary>计算两个序号的丢包数</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Lose(int y1, int y2)
        {
            if (y1 == y2) return 0;
            return (y2 > y1) ? y2 - y1 - 1 : y2 + 127 - y1; //本次丢包数
        }
        /// <summary>计算两个序号的丢包数(相位差)</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int LoseRef(int y1, int y2)
        {
            var lose = Lose(y1, y2);
            if (lose > 64)
            {
                lose = 126 - lose;
            }
            return lose;
        }
    }
}
