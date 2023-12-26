using System;
using System.Reactive.Linq;

namespace DemoRx
{
    internal class DemoSample
    {
        public static void Demo1()
        {
            var interval = Observable.Interval(TimeSpan.FromMilliseconds(1500));
            interval.Sample(TimeSpan.FromSeconds(1))
                    .SubscribeConsole("Sample1");

        }
    }
}
