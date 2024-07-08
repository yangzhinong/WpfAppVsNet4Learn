//using MQTTnet;
//using MQTTnet.Client;
//using Newtonsoft.Json.Linq;
using MQTTnet;
using MQTTnet.Client;
using System;
using System.IO;
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
                    //mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;
                    //await mqttClient.SubscribeAsync("test");
                    //await mqttClient.SubscribeAsync("test2");
                    //var mapMessage = new MqttApplicationMessageBuilder()
                    //   .WithTopic("caeri/v1/v2x/MAP")
                    //   .WithPayload("{\"mapFrame\":{\"msgCnt\":11,\"timestamp\":62622,\"nodes\":[{\"name\":\"189\",\"id\":{\"region\":5000,\"id\":189},\"refPos\":{\"lat\":297178860,\"long\":1065301540,\"elevation\":2321},\"inLinks\":[{\"name\":\"190-189\",\"upstreamNodeId\":{\"region\":5000,\"id\":190},\"speedLimits\":[{\"type\":\"vehicleMaxSpeed\",\"speed\":972}],\"linkWidth\":700,\"points\":[{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065317280,\"lat\":297193010}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2305}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065304870,\"lat\":297184470}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2297}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065300770,\"lat\":297181650}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2294}}}],\"movements\":[{\"remoteIntersection\":{\"region\":5000,\"id\":190},\"phaseId\":0},{\"remoteIntersection\":{\"region\":5000,\"id\":187},\"phaseId\":5},{\"remoteIntersection\":{\"region\":5000,\"id\":270},\"phaseId\":3}],\"lanes\":[{\"laneID\":1,\"laneWidth\":700,\"laneAttributes\":{\"shareWith\":\"0300\",\"laneType\":{\"present\":\"vehicle\",\"vehicle\":\"02\"}},\"maneuvers\":\"D000\",\"connectsTo\":[{\"remoteIntersection\":{\"region\":5000,\"id\":190},\"connectingLane\":{\"lane\":1,\"maneuvers\":\"1000\"},\"phaseId\":0},{\"remoteIntersection\":{\"region\":5000,\"id\":187},\"connectingLane\":{\"lane\":1,\"maneuvers\":\"4000\"},\"phaseId\":5},{\"remoteIntersection\":{\"region\":5000,\"id\":270},\"connectingLane\":{\"lane\":1,\"maneuvers\":\"8000\"},\"phaseId\":3}],\"speedLimits\":[{\"type\":\"vehicleMaxSpeed\",\"speed\":972}],\"points\":[{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065317280,\"lat\":297193010}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2305}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065304870,\"lat\":297184470}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2297}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065300770,\"lat\":297181650}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2294}}}]}]},{\"name\":\"270-189\",\"upstreamNodeId\":{\"region\":5000,\"id\":270},\"speedLimits\":[{\"type\":\"vehicleMaxSpeed\",\"speed\":972}],\"linkWidth\":700,\"points\":[{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065284450,\"lat\":297169270}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2271}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065297400,\"lat\":297178040}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2288}}}],\"movements\":[{\"remoteIntersection\":{\"region\":5000,\"id\":190},\"phaseId\":1},{\"remoteIntersection\":{\"region\":5000,\"id\":187},\"phaseId\":0},{\"remoteIntersection\":{\"region\":5000,\"id\":270},\"phaseId\":0}],\"lanes\":[{\"laneID\":1,\"laneWidth\":700,\"laneAttributes\":{\"shareWith\":\"0300\",\"laneType\":{\"present\":\"vehicle\",\"vehicle\":\"02\"}},\"maneuvers\":\"B000\",\"connectsTo\":[{\"remoteIntersection\":{\"region\":5000,\"id\":270},\"connectingLane\":{\"lane\":1,\"maneuvers\":\"1000\"},\"phaseId\":0},{\"remoteIntersection\":{\"region\":5000,\"id\":190},\"connectingLane\":{\"lane\":1,\"maneuvers\":\"8000\"},\"phaseId\":1},{\"remoteIntersection\":{\"region\":5000,\"id\":187},\"connectingLane\":{\"lane\":1,\"maneuvers\":\"2000\"},\"phaseId\":0}],\"speedLimits\":[{\"type\":\"vehicleMaxSpeed\",\"speed\":972}],\"points\":[{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065284450,\"lat\":297169270}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2271}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065297400,\"lat\":297178040}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2288}}}]}]},{\"name\":\"436-189\",\"upstreamNodeId\":{\"region\":5000,\"id\":436},\"speedLimits\":[{\"type\":\"vehicleMaxSpeed\",\"speed\":972}],\"linkWidth\":700,\"points\":[{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065312880,\"lat\":297163960}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2372}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065302690,\"lat\":297176670}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2281}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065301730,\"lat\":297177860}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2275}}}],\"movements\":[{\"remoteIntersection\":{\"region\":5000,\"id\":270},\"phaseId\":6},{\"remoteIntersection\":{\"region\":5000,\"id\":190},\"phaseId\":0}],\"lanes\":[{\"laneID\":1,\"laneWidth\":700,\"laneAttributes\":{\"shareWith\":\"0300\",\"laneType\":{\"present\":\"vehicle\",\"vehicle\":\"02\"}},\"maneuvers\":\"6000\",\"connectsTo\":[{\"remoteIntersection\":{\"region\":5000,\"id\":270},\"connectingLane\":{\"lane\":1,\"maneuvers\":\"4000\"},\"phaseId\":6},{\"remoteIntersection\":{\"region\":5000,\"id\":190},\"connectingLane\":{\"lane\":1,\"maneuvers\":\"2000\"},\"phaseId\":0}],\"speedLimits\":[{\"type\":\"vehicleMaxSpeed\",\"speed\":972}],\"points\":[{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065312880,\"lat\":297163960}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2372}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065302690,\"lat\":297176670}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2281}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065301730,\"lat\":297177860}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2275}}}]}]}]},{\"name\":\"190\",\"id\":{\"region\":5000,\"id\":190},\"refPos\":{\"lat\":297192730,\"long\":1065318630,\"elevation\":2328},\"inLinks\":[{\"name\":\"189-190\",\"upstreamNodeId\":{\"region\":5000,\"id\":189},\"speedLimits\":[{\"type\":\"vehicleMaxSpeed\",\"speed\":972}],\"linkWidth\":700,\"points\":[{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065301650,\"lat\":297180470}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2318}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065302180,\"lat\":297181290}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2318}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065315000,\"lat\":297190190}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2326}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065317440,\"lat\":297191900}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2328}}}],\"movements\":[{\"remoteIntersection\":{\"region\":5000,\"id\":189},\"phaseId\":0},{\"remoteIntersection\":{\"region\":5000,\"id\":191},\"phaseId\":2},{\"remoteIntersection\":{\"region\":5000,\"id\":368},\"phaseId\":0}],\"lanes\":[{\"laneID\":1,\"laneWidth\":700,\"laneAttributes\":{\"shareWith\":\"0300\",\"laneType\":{\"present\":\"vehicle\",\"vehicle\":\"02\"}},\"maneuvers\":\"B000\",\"connectsTo\":[{\"remoteIntersection\":{\"region\":5000,\"id\":189},\"connectingLane\":{\"lane\":1,\"maneuvers\":\"1000\"},\"phaseId\":0},{\"remoteIntersection\":{\"region\":5000,\"id\":191},\"connectingLane\":{\"lane\":1,\"maneuvers\":\"8000\"},\"phaseId\":2},{\"remoteIntersection\":{\"region\":5000,\"id\":368},\"connectingLane\":{\"lane\":1,\"maneuvers\":\"2000\"},\"phaseId\":0}],\"speedLimits\":[{\"type\":\"vehicleMaxSpeed\",\"speed\":972}],\"points\":[{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065301650,\"lat\":297180470}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2318}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065302180,\"lat\":297181290}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2318}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065315000,\"lat\":297190190}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2326}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065317440,\"lat\":297191900}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2328}}}]}]}]},{\"name\":\"187\",\"id\":{\"region\":5000,\"id\":187},\"refPos\":{\"lat\":297145080,\"long\":1065328630,\"elevation\":2538},\"inLinks\":[{\"name\":\"189-187\",\"upstreamNodeId\":{\"region\":5000,\"id\":189},\"speedLimits\":[{\"type\":\"vehicleMaxSpeed\",\"speed\":972}],\"linkWidth\":700,\"points\":[{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065300290,\"lat\":297178210}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2295}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065301800,\"lat\":297175910}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2301}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065312170,\"lat\":297163090}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2392}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065321960,\"lat\":297150890}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2474}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065325040,\"lat\":297146820}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2493}}}],\"movements\":[{\"remoteIntersection\":{\"region\":5000,\"id\":434},\"phaseId\":0},{\"remoteIntersection\":{\"region\":5000,\"id\":239},\"phaseId\":4},{\"remoteIntersection\":{\"region\":5000,\"id\":186},\"phaseId\":3}],\"lanes\":[{\"laneID\":1,\"laneWidth\":700,\"laneAttributes\":{\"shareWith\":\"0300\",\"laneType\":{\"present\":\"vehicle\",\"vehicle\":\"02\"}},\"maneuvers\":\"D000\",\"connectsTo\":[{\"remoteIntersection\":{\"region\":5000,\"id\":434},\"connectingLane\":{\"lane\":1,\"maneuvers\":\"1000\"},\"phaseId\":0},{\"remoteIntersection\":{\"region\":5000,\"id\":239},\"connectingLane\":{\"lane\":1,\"maneuvers\":\"4000\"},\"phaseId\":4},{\"remoteIntersection\":{\"region\":5000,\"id\":186},\"connectingLane\":{\"lane\":1,\"maneuvers\":\"8000\"},\"phaseId\":3}],\"speedLimits\":[{\"type\":\"vehicleMaxSpeed\",\"speed\":972}],\"points\":[{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065300290,\"lat\":297178210}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2295}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065301800,\"lat\":297175910}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2301}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065312170,\"lat\":297163090}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2392}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065321960,\"lat\":297150890}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2474}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065325040,\"lat\":297146820}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2493}}}]}]}]},{\"name\":\"270\",\"id\":{\"region\":5000,\"id\":270},\"refPos\":{\"lat\":297169570,\"long\":1065283700,\"elevation\":2270},\"inLinks\":[{\"name\":\"189-270\",\"upstreamNodeId\":{\"region\":5000,\"id\":189},\"speedLimits\":[{\"type\":\"vehicleMaxSpeed\",\"speed\":972}],\"linkWidth\":700,\"points\":[{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065298190,\"lat\":297178980}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2290}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065295110,\"lat\":297177750}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2285}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065286760,\"lat\":297171760}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2272}}}],\"movements\":[{\"remoteIntersection\":{\"region\":5000,\"id\":189},\"phaseId\":0}],\"lanes\":[{\"laneID\":1,\"laneWidth\":700,\"laneAttributes\":{\"shareWith\":\"0300\",\"laneType\":{\"present\":\"vehicle\",\"vehicle\":\"02\"}},\"maneuvers\":\"1000\",\"connectsTo\":[{\"remoteIntersection\":{\"region\":5000,\"id\":189},\"connectingLane\":{\"lane\":1,\"maneuvers\":\"1000\"},\"phaseId\":0}],\"speedLimits\":[{\"type\":\"vehicleMaxSpeed\",\"speed\":972}],\"points\":[{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065298190,\"lat\":297178980}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2290}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065295110,\"lat\":297177750}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2285}}},{\"posOffset\":{\"offsetLL\":{\"present\":\"position-LatLon\",\"position-LatLon\":{\"lon\":1065286760,\"lat\":297171760}},\"offsetV\":{\"present\":\"elevation\",\"elevation\":2272}}}]}]}]}]}}")
                    //   .Build();

                    //var spatMessage = new MqttApplicationMessageBuilder()
                    //   .WithTopic("caeri/v1/v2x/SPAT")
                    //   .WithPayload("{\"spatFrame\":{\"msgCnt\":101,\"moy\":62622,\"timeStamp\":100,\"name\":\"SPAT name\",\"intersections\":[{\"intersectionId\":{\"region\":5000,\"id\":189},\"status\":\"2000\",\"moy\":62622,\"timeStamp\":44058,\"phases\":[{\"id\":1,\"phaseStates\":[{\"light\":\"red\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":0,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":290,\"timeConfidence\":0,\"nextStartTime\":570,\"nextDuration\":420}}},{\"light\":\"permissive_green\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":290,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":540,\"timeConfidence\":0,\"nextStartTime\":990,\"nextDuration\":250}}},{\"light\":\"yellow\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":540,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":570,\"timeConfidence\":0,\"nextStartTime\":1240,\"nextDuration\":30}}}]},{\"id\":3,\"phaseStates\":[{\"light\":\"red\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":0,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":210,\"timeConfidence\":0,\"nextStartTime\":570,\"nextDuration\":340}}},{\"light\":\"permissive_green\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":210,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":540,\"timeConfidence\":0,\"nextStartTime\":910,\"nextDuration\":330}}},{\"light\":\"yellow\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":540,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":570,\"timeConfidence\":0,\"nextStartTime\":1240,\"nextDuration\":30}}}]},{\"id\":5,\"phaseStates\":[{\"light\":\"red\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":0,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":50,\"timeConfidence\":0,\"nextStartTime\":290,\"nextDuration\":460}}},{\"light\":\"permissive_green\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":50,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":260,\"timeConfidence\":0,\"nextStartTime\":750,\"nextDuration\":210}}},{\"light\":\"yellow\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":260,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":290,\"timeConfidence\":0,\"nextStartTime\":960,\"nextDuration\":30}}}]},{\"id\":6,\"phaseStates\":[{\"light\":\"permissive_green\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":0,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":20,\"timeConfidence\":0,\"nextStartTime\":570,\"nextDuration\":150}}},{\"light\":\"yellow\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":20,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":50,\"timeConfidence\":0,\"nextStartTime\":720,\"nextDuration\":30}}},{\"light\":\"red\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":50,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":570,\"timeConfidence\":0,\"nextStartTime\":750,\"nextDuration\":520}}}]},{\"id\":9,\"phaseStates\":[{\"light\":\"red\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":0,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":290,\"timeConfidence\":0,\"nextStartTime\":570,\"nextDuration\":420}}},{\"light\":\"permissive_green\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":290,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":570,\"timeConfidence\":0,\"nextStartTime\":990,\"nextDuration\":280}}},{\"light\":\"yellow\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":570,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":570,\"timeConfidence\":0,\"nextStartTime\":1270,\"nextDuration\":0}}}]},{\"id\":10,\"phaseStates\":[{\"light\":\"permissive_green\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":0,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":50,\"timeConfidence\":0,\"nextStartTime\":570,\"nextDuration\":180}}},{\"light\":\"yellow\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":50,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":50,\"timeConfidence\":0,\"nextStartTime\":750,\"nextDuration\":0}}},{\"light\":\"red\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":50,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":570,\"timeConfidence\":0,\"nextStartTime\":750,\"nextDuration\":520}}}]},{\"id\":11,\"phaseStates\":[{\"light\":\"red\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":0,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":50,\"timeConfidence\":0,\"nextStartTime\":210,\"nextDuration\":540}}},{\"light\":\"permissive_green\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":50,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":210,\"timeConfidence\":0,\"nextStartTime\":750,\"nextDuration\":160}}},{\"light\":\"yellow\",\"timing\":{\"present\":\"counting\",\"counting\":{\"startTime\":210,\"minEndTime\":0,\"maxEndTime\":0,\"likelyEndTime\":210,\"timeConfidence\":0,\"nextStartTime\":910,\"nextDuration\":0}}}]}]}]}}")
                    //   .Build();

                    //var rsiMessage = new MqttApplicationMessageBuilder()
                    //  .WithTopic("caeri/v1/v2x/SPAT")
                    //  .WithPayload(System.IO.File.ReadAllText("C:\\data_rzpc\\20240205123326\\spat9.txt"))
                    //  .Build();
                    //var rsmMessage = new MqttApplicationMessageBuilder()
                    //  .WithTopic("caeri/v1/v2x/RSM")
                    //  .WithPayload("{\"rsmFrame\":{\"msgCnt\":5,\"id\":\"ABCDEFG\",\"refPos\":{\"lat\":297178860,\"long\":1065301540,\"elevation\":1090},\"participants\":[{\"ptcType\":\"motor\",\"ptcId\":1,\"source\":\"selfinfo\",\"id\":\"1234567\",\"secMark\":5,\"pos\":{\"offsetLL\":{\"present\":\"position-LL1\",\"position-LL1\":{\"lon\":2047,\"lat\":2047}},\"offsetV\":{\"present\":\"offset1\",\"offset1\":63}},\"posConfidence\":{\"pos\":\"a500m\",\"elevation\":\"elev-200-00\"},\"transmission\":\"park\",\"speed\":8190,\"heading\":5,\"angle\":6,\"motionCfd\":{\"speedCfd\":\"prec100ms\",\"headingCfd\":\"prec01deg\",\"steerCfd\":\"prec0-02deg\"},\"accelSet\":{\"long\":4,\"lat\":5,\"vert\":6,\"yaw\":7},\"size\":{\"width\":9,\"length\":1,\"height\":8},\"vehicleClass\":{\"classification\":2,\"fuelType\":3}},{\"ptcType\":\"motor\",\"ptcId\":1,\"source\":\"selfinfo\",\"id\":\"1234567\",\"secMark\":5,\"pos\":{\"offsetLL\":{\"present\":\"position-LL1\",\"position-LL1\":{\"lon\":2047,\"lat\":2047}},\"offsetV\":{\"present\":\"offset1\",\"offset1\":63}},\"posConfidence\":{\"pos\":\"a500m\",\"elevation\":\"elev-200-00\"},\"transmission\":\"park\",\"speed\":8190,\"heading\":5,\"angle\":6,\"motionCfd\":{\"speedCfd\":\"prec100ms\",\"headingCfd\":\"prec01deg\",\"steerCfd\":\"prec0-02deg\"},\"accelSet\":{\"long\":4,\"lat\":5,\"vert\":6,\"yaw\":7},\"size\":{\"width\":9,\"length\":1,\"height\":8},\"vehicleClass\":{\"classification\":2,\"fuelType\":3}},{\"ptcType\":\"motor\",\"ptcId\":1,\"source\":\"selfinfo\",\"id\":\"1234567\",\"secMark\":5,\"pos\":{\"offsetLL\":{\"present\":\"position-LL1\",\"position-LL1\":{\"lon\":2047,\"lat\":2047}},\"offsetV\":{\"present\":\"offset1\",\"offset1\":63}},\"posConfidence\":{\"pos\":\"a500m\",\"elevation\":\"elev-200-00\"},\"transmission\":\"park\",\"speed\":8190,\"heading\":5,\"angle\":6,\"motionCfd\":{\"speedCfd\":\"prec100ms\",\"headingCfd\":\"prec01deg\",\"steerCfd\":\"prec0-02deg\"},\"accelSet\":{\"long\":4,\"lat\":5,\"vert\":6,\"yaw\":7},\"size\":{\"width\":9,\"length\":1,\"height\":8},\"vehicleClass\":{\"classification\":2,\"fuelType\":3}}]}}").Build();


                    //var gnssMessage = new MqttApplicationMessageBuilder()
                    //  .WithTopic("caeri/v1/devmon/GNSS")
                    //  .WithPayload("{\"satellites\":30,\"lon\":112.5698729,\"lat\":26.8778676,\"ele\":58.0629997253418}")
                    //  .Build();
                    //await mqttClient.PublishAsync(gnssMessage, CancellationToken.None);
                    //var bsmMessage = new MqttApplicationMessageBuilder()
                    //  .WithTopic("caeri/v1/v2x/BSM")
                    //  .WithPayload("{\"bsmFrame\":{\"msgCnt\":111,\"id\":\"ABCDEFG\",\"secMark\":1200,\"timeConfidence\":\"time-100-000\",\"pos\":{\"lat\":297178860,\"long\":1065301540,\"elevation\":1090},\"posAccuracy\":{\"semiMajor\":10,\"semiMinor\":20,\"orientation\":30},\"posConfidence\":{\"pos\":\"a2m\",\"elevation\":\"elev-000-02\"},\"transmission\":\"forwardGears\",\"speed\":8190,\"heading\":28700,\"angle\":125,\"motionCfd\":{\"speedCfd\":\"prec0-05ms\",\"headingCfd\":\"prec0-1deg\",\"steerCfd\":\"prec0-02deg\"},\"accelSet\":{\"long\":2000,\"lat\":2000,\"vert\":127,\"yaw\":32767},\"brakes\":{\"brakePadel\":\"on\",\"wheelBrakes\":\"0080\",\"traction\":\"engaged\",\"abs\":\"engaged\",\"scs\":\"on\",\"brakeBoost\":\"off\",\"auxBrakes\":\"unavailable\"},\"size\":{\"width\":100,\"length\":200,\"height\":50},\"vehicleClass\":{\"classification\":2,\"fuelType\":3},\"safetyExt\":{\"events\":{\"value\":\"0280\",\"length\":9},\"pathHistory\":{\"initialPosition\":{\"utcTime\":{\"year\":1,\"month\":2,\"day\":3,\"hour\":4,\"minute\":5,\"second\":6,\"offset\":7},\"pos\":{\"lat\":2089,\"long\":2088,\"elevation\":2099},\"heading\":100,\"transmission\":\"park\",\"speed\":200,\"posAccuracy\":{\"pos\":\"a5m\",\"elevation\":\"elev-000-01\"},\"timeConfidence\":\"time-000-000-000-000-05\",\"motionCfd\":{\"speedCfd\":\"prec0-05ms\",\"headingCfd\":\"prec0-1deg\",\"steerCfd\":\"prec2deg\"}},\"currGNSSstatus\":1,\"crumbData\":[{\"llvOffset\":{\"offsetLL\":{\"present\":\"position-LL1\",\"position-LL1\":{\"lon\":200,\"lat\":200}},\"offsetV\":{\"present\":\"offset1\",\"offset1\":63}},\"speed\":400,\"posAccuracy\":{\"pos\":\"a50m\",\"elevation\":\"elev-010-00\"},\"heading\":100},{\"llvOffset\":{\"offsetLL\":{\"present\":\"position-LL1\",\"position-LL1\":{\"lon\":200,\"lat\":200}},\"offsetV\":{\"present\":\"offset1\",\"offset1\":63}},\"speed\":400,\"posAccuracy\":{\"pos\":\"a50m\",\"elevation\":\"elev-010-00\"},\"heading\":100},{\"llvOffset\":{\"offsetLL\":{\"present\":\"position-LL1\",\"position-LL1\":{\"lon\":200,\"lat\":200}},\"offsetV\":{\"present\":\"offset1\",\"offset1\":63}},\"speed\":400,\"posAccuracy\":{\"pos\":\"a50m\",\"elevation\":\"elev-010-00\"},\"heading\":100},{\"llvOffset\":{\"offsetLL\":{\"present\":\"position-LL1\",\"position-LL1\":{\"lon\":200,\"lat\":200}},\"offsetV\":{\"present\":\"offset1\",\"offset1\":63}},\"speed\":400,\"posAccuracy\":{\"pos\":\"a50m\",\"elevation\":\"elev-010-00\"},\"heading\":100},{\"llvOffset\":{\"offsetLL\":{\"present\":\"position-LL1\",\"position-LL1\":{\"lon\":200,\"lat\":200}},\"offsetV\":{\"present\":\"offset1\",\"offset1\":63}},\"speed\":400,\"posAccuracy\":{\"pos\":\"a50m\",\"elevation\":\"elev-010-00\"},\"heading\":100}]},\"pathPrediction\":{\"radiusOfCurve\":1,\"confidence\":2},\"lights\":2},\"emergencyExt\":{\"responseType\":\"nonEmergency\",\"sirenUse\":\"inUse\",\"lightsUse\":\"arrowSignsActive\"}}}").Build();

                    //var i = 0;
                    //while (true)
                    //{
                    //    i++;

                    //    await Task.Delay(100);
                    //    await mqttClient.PublishAsync(mapMessage, CancellationToken.None);
                    //    if (i% 2 == 0)
                    //    {
                    //        await mqttClient.PublishAsync(spatMessage, CancellationToken.None);
                    //    }

                    //    if (i% 3 == 0)
                    //    {
                    //        await mqttClient.PublishAsync(rsiMessage, CancellationToken.None);
                    //        await mqttClient.PublishAsync(rsmMessage, CancellationToken.None);
                    //    }

                    //    if (i % 10 == 1)
                    //    {

                    //        await mqttClient.PublishAsync(bsmMessage, CancellationToken.None);
                    //    }

                    //}
                    var rnd = new Random();
                    //var basePath = "C:\\data_rzpc\\Import\\成都试验区\\daf\\原始数据文件夹\\20240109160517";
                    var basePath = "C:\\data_rzpc\\Import\\成都试验区\\daf\\原始数据文件夹\\20240109163159";
                    using (var db = GetLocalFreeSqlDb(basePath, false))
                    {
                        var list = db.Select<CollectionMessage>().ToList();
                        foreach (var item in list)
                        {
                            await Task.Delay(10);
                            var fileName = item.MessageFile;
                            var topic = "caeri/v1/v2x/" + item.MessageType.ToUpper();
                            if (fileName.Contains("GNSS"))
                            {
                                fileName = Path.Combine(basePath, "GNSS", Path.GetFileName(item.MessageFile));
                                topic = "caeri/v1/devmon/GNSS";
                            } else
                            {
                                fileName = Path.Combine(basePath, Path.GetFileName(item.MessageFile));
                            }
                              
                            var txt = System.IO.File.ReadAllText( fileName);
          


                            //if (item.MessageType == "map" || item.MessageType == "spat")
                            //{
                            //    var jt = JObject.Parse(txt);
                            //    var frame = (JObject)jt[item.MessageType + "Frame"];
                            //    if (item.MessageType == "map")
                            //    {
                            //        var old = frame["timestamp"].Value<long>();
                            //        frame.Add("recTimestamp", old + rnd.Next(100, 300));
                            //    }
                            //    else
                            //    {
                            //        var old = frame["timeStamp"].Value<long>();
                            //        frame.Add("recTimestamp", old + rnd.Next(100, 300));
                            //    }

                            //    txt = jt.ToString();
                            //}

                            var mqttMessage = new MqttApplicationMessageBuilder()
                                .WithTopic(topic)
                                .WithPayload(txt)
                                .Build();
                            await mqttClient.PublishAsync(mqttMessage, CancellationToken.None);
                        }
                    }



                    Console.ReadLine();

                    var mqttClientDisconnectOptions = new MqttClientDisconnectOptionsBuilder()
                                                          .WithReason(MqttClientDisconnectOptionsReason.NormalDisconnection)
                                                          .Build();

                    await mqttClient.DisconnectAsync(mqttClientDisconnectOptions);
                }
            });
            //V2.Do();
            //V20240117160359.Do();
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