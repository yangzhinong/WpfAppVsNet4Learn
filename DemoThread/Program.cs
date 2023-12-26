using Newtonsoft.Json.Linq;
using System.Text;


namespace DemoThread;

internal class Program
{
    static Dictionary<int, string> dic = new();
    static List<Task> tasks = new();
    private readonly static object _locker= new object();
    static ProgressStatus _status;
    static void Main(string[] args)
    {
        //var queue = new PCQueue(5);
        //var cts= new CancellationTokenSource();
        //var t= queue.EnqueueTask(() => { Thread.Sleep(5000); Console.WriteLine("Hello"); }, cts.Token);

        //if (t is Task<object> tt)
        //{
        //    cts.Cancel();
        //    Console.WriteLine(tt.Result);
        //}
        //LazyInitializer.EnsureInitialized(ref queue, () => new PCQueue(5));


        //Lazy<PCQueue> _dds = new Lazy<PCQueue>(() => new PCQueue(5), true);
        GeoCoordinate point1 = new GeoCoordinate(37.7749, -122.4194); // 旧金山的坐标
        GeoCoordinate point2 = new GeoCoordinate(34.0522, -118.2437); // 洛杉矶的坐标
        var s = File.ReadAllText("C:\\bsm1.txt");

        var obj = JObject.Parse(s);

        var id = obj["bsmFrame"]["id"];
        var ss = id.ToString();
        
        //Encoding utf8 = Encoding.GetEncoding(65001);
        var bytes= Encoding.UTF8.GetBytes(ss);
        var sAsc= Encoding.ASCII.GetString(bytes);
        var ddd = Convert.ToBase64String(bytes);
        var byt2= Convert.FromBase64String(ss);
        var ss3= System.Text.Encoding.UTF8.GetString(byt2);


        //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        //Encoding gb2312 = Encoding.GetEncoding("GB2312");
        //var temp1 = Encoding.Convert(utf8, gb2312, bytes);
        //var temp2= Encoding.Convert(utf8, Encoding.ASCII, bytes);

        //var s2= gb2312.GetString(temp1);
        //var s3= Encoding.ASCII.GetString(temp2);
        Console.WriteLine(s);
        
        Console.ReadLine();
    }

    public class ProgressStatus
    {
        volatile ProgressStatus _expensive;
        public readonly int PercentComplete;
        public readonly string StatusMessage;
        [ThreadStatic]
        static int _x;
        readonly ThreadLocal<Random> _y = new(() => new Random(Guid.NewGuid().GetHashCode()));

        LocalDataStoreSlot _secSol = Thread.GetNamedDataSlot("dd");
        
        // This class might have many more fields...

        public ProgressStatus(int percentComplete, string statusMessage)
        {
            PercentComplete = percentComplete;
            StatusMessage = statusMessage;
            var ddd = new System.Threading.Timer(dd, null, 200, Timeout.Infinite);
        }

        private void dd(object? state)
        {
            throw new NotImplementedException();
        }

        public ProgressStatus Expensive
        {
            get
            {
                if (_expensive == null)
                {
                    var instace = new ProgressStatus(0, "Success");
                    Interlocked.CompareExchange(ref _expensive, instace, null);
                    
                }
                return _expensive;
            }
        }
    }
}
