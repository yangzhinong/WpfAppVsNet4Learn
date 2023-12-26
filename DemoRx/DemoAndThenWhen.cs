using System;
using System.Linq;
using System.Reactive.Linq;

namespace DemoRx
{
    internal class DemoAndThenWhen
    {

        public static void FirstUseZip()
        {
            var one = Observable.Interval(TimeSpan.FromSeconds(1)).Take(5);
            var two = Observable.Interval(TimeSpan.FromMilliseconds(250)).Take(10);
            var three = Observable.Interval(TimeSpan.FromMilliseconds(150)).Take(14);

            var zippedSequence = one.Zip(two, (lhs, rhs) => new
            {
                One = lhs,
                Two = rhs
            }).Zip(three, (lhs, rhs) => new
            {
                One = lhs.One,
                Two = lhs.Two,
                Tree = rhs
            });
            zippedSequence.SubscribeConsole("Zip");
        }

        public static void UseAndTheWhen()
        {
            var one = Observable.Interval(TimeSpan.FromSeconds(1)).Take(5);
            var two = Observable.Interval(TimeSpan.FromMilliseconds(250)).Take(10);
            var three = Observable.Interval(TimeSpan.FromMilliseconds(150)).Take(14);

            //var patten= one.And(two).And(three);
            //var plan = patten.Then((first, second, third) => new
            //{
            //    One = first,
            //    Two = second,
            //    Three = third
            //});
            //var zippedSequence = Observable.When(plan);
            //zippedSequence.SubscribeConsole("UseAndThenWhen");

            Observable.When(one.And(two).And(three).Then((x, y, z) => new { x, y, z }))
                  .SubscribeConsole("UseAndThenWhen");

        }
    }
}
