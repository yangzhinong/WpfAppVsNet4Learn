using System;
using System.Linq;
using System.Reactive.Linq;

namespace DemoRx
{
    public class DemoBuffer
    {
        public static void DemoSkip1()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1)).Take(10);
            source.Buffer(3, 1)
                  .SubscribeConsoleJson("Buffer(3,1)");
             
        }

        public static void DemoSkip3()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1)).Take(10);
            source.Buffer(3, 3)
                  .SubscribeConsoleJson("Buffer(3,3)");

        }

        public static void DemoSkip5()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1)).Take(10);
            source.Buffer(3, 5)
                  .SubscribeConsoleJson("Buffer(3,3)");

        }
    }
}
