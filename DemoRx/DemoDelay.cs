using System;
using System.Reactive.Linq;

namespace DemoRx
{
    internal class DemoDelay
    {
        public static void Demo1()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(1))
                                .Take(5)
                                .Timestamp();

            var delay = source.Delay(DateTimeOffset.Now.AddSeconds(2));

            source.SubscribeConsole("原始的");
            delay.SubscribeConsole("延迟后的");
            
        }
    }
}
