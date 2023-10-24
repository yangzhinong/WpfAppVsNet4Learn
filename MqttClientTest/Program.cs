using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MqttClientTest
{
    internal class Program
    {
        private static void Main(string[] args)
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
                    mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;
                    await mqttClient.SubscribeAsync("test");
                    await mqttClient.SubscribeAsync("test2");

                    Console.ReadLine();

                    var mqttClientDisconnectOptions = new MqttClientDisconnectOptionsBuilder()
                                                          .WithReason(MqttClientDisconnectOptionsReason.NormalDisconnection)
                                                          .Build();

                    await mqttClient.DisconnectAsync(mqttClientDisconnectOptions);
                }
            });

            Console.ReadLine();
        }

        private static async Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            await Task.Delay(100);

            Console.WriteLine("topic:" + arg.ApplicationMessage.Topic);

            Console.WriteLine("body:" + Encoding.UTF8.GetString(arg.ApplicationMessage.PayloadSegment.ToArray()));
        }
    }
}