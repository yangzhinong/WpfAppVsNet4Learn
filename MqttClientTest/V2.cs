using MQTTnet;
using MQTTnet.Client;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MqttClientTest
{
    public class V2
    {
        public static void Do()
        {
            Task.Run(async () =>
            {
                var mqttFactory = new MqttFactory();
                using (var mqttClient = mqttFactory.CreateMqttClient())
                {
                    var mqttClientOptions = new MqttClientOptionsBuilder()
                                                    .WithTcpServer("127.0.0.1", 1883)
                                                    .Build();
                    try
                    {
                        using (var timeoutToken = new CancellationTokenSource(TimeSpan.FromSeconds(1)))
                        {
                            await mqttClient.ConnectAsync(mqttClientOptions, timeoutToken.Token);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine("Timeout while connecting.");
                    }

                    await mqttClient.PingAsync(CancellationToken.None);

                    var rnd = new Random();

                    var di = new System.IO.DirectoryInfo("C:\\data_rzpc\\rsi");
                    var files = di.GetFiles().OrderBy(x => x.CreationTime.Ticks).ToList();
                    foreach (var file in files )
                    {
                        await Task.Delay(50);
                        var txt =   System.IO.File.ReadAllText(file.FullName);
                        var topic = "caeri/v1/v2x/RSI";


                        var mqttMessage = new MqttApplicationMessageBuilder()
                            .WithTopic(topic)
                            .WithPayload(txt)
                            .Build();
                        await mqttClient.PublishAsync(mqttMessage, CancellationToken.None);
                    }




                    Console.ReadLine();

                    var mqttClientDisconnectOptions = new MqttClientDisconnectOptionsBuilder()
                                                          .WithReason(MqttClientDisconnectOptionsReason.NormalDisconnection)
                                                          .Build();

                    await mqttClient.DisconnectAsync(mqttClientDisconnectOptions);
                }
            });

            Console.ReadLine();
        }

        /// <summary>
        /// 从上传路径获取本地数据库
        /// </summary>
        /// <param name="dbPath"></param>
        /// <returns></returns>
        public static IFreeSql GetLocalFreeSqlDb(string dbPath, bool useAutoSyncStructure = true)
        {
            System.IO.Directory.CreateDirectory(dbPath);
            var file = System.IO.Path.Combine(dbPath, "db.sqlite");
            return new FreeSql.FreeSqlBuilder()
                     .UseConnectionString(FreeSql.DataType.Sqlite, $@"Data Source={file};PRAGMA journal_mode=WAL;")
                     .UseAutoSyncStructure(useAutoSyncStructure)
                     .Build();
        }

        private static async Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            await Task.Delay(100);

            Console.WriteLine("topic:" + arg.ApplicationMessage.Topic);

            Console.WriteLine("body:" + Encoding.UTF8.GetString(arg.ApplicationMessage.PayloadSegment.ToArray()));
        }
    }
}
