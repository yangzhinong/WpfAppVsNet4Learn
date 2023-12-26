using Newtonsoft.Json;
using Reactive.Bindings.Notifiers;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DemoRx
{
    class Sample
    {
        public string Property1 { get; set; }
    }
    internal class Program
    {
        private static StockTicker _ticker;
        static void Main(string[] args)
        {
            //Console.WriteLine("MessageBroker");
            //RunMessageBroker();
            //var subject = new Subject<string>(); 
            //WriteSequenceToConsole(subject); 
            //subject.OnNext("a"); 
            //subject.OnNext("b");
            //subject.OnCompleted();
            //subject.OnNext("c");

            var subject = new Subject<int>();
            //subject.SubscribeConsole("subject");
            //subject.ElementAtOrDefault(5)
            //       .SubscribeConsole("ElementAt");

            //subject.Scan(5,(acc, cur) => acc += cur)
            //       .SubscribeConsole("scan");

            //RunningMin(subject).SubscribeConsole("MyMin");

            //subject.GroupBy(x => x % 3)
            //      .Subscribe(grp =>  grp.Min()
            //                          .Subscribe(minValue => Console.WriteLine("{0} min value = {1}", grp.Key, minValue)),
            //                          () => Console.WriteLine("GroupBy Complete"));

            //subject.GroupBy(x => x % 3)
            //    .SelectMany(grp => grp.Max().Select(value => new { grp.Key, value }))
            //    .SubscribeConsole("GroupBy");

            Func<int, char> letter = i => (char) (i+64);
            //var offset = 10;
            //Observable.Interval( TimeSpan.FromSeconds(3))
            //    .Select(x => x+1)
            //    .Take(3)
            //    .SelectMany(GetSubValues)
            //    .SubscribeConsole("TiemrAndOffset");

            //var qery = from i in Observable.Range(1, 5)
            //           where i % 2 == 0
            //           from j in GetSubValues(i)
            //           select new { i, j };

            //qery.SubscribeConsole("qery");

            //DemoAndThenWhen.UseAndTheWhen();

            //DemoBuffer.DemoSkip5();
            //DemoDelay.Demo1();
            // DemoSample.Demo1();

            //DemoColdAndHot.PublishLast();
            //DemoColdAndHot.Replay();
            //DemoSchedulingAndThreading.Demo1();
            //DemoSchedulingAndThreading.Demo2();
            //DemoSchedulingAndThreading.Demo4();
            // DemoSchedulingAndThreading.DemoDelay();
            //DemoSchedulingAndThreading.Demo6();
            //DemoSchedulingAndThreading.Demo7();
            //DemoSchedulingAndThreading.Demo8(ImmediateScheduler.Instance);
            Thread.CurrentThread.Name = "HHH_Main";
             DemoSchedulingAndThreading.Demo8(TaskPoolScheduler.Default);
            //DemoSchedulingAndThreading.Demo6_1();
            //DemoSchedulingAndThreading.Demo6_2();

            // DemoSchedulingAndThreading.DemoShareState(TaskPoolScheduler.Default);
            //DemoSchedulingAndThreading.DemoShareListState(NewThreadScheduler.Default);
            //DemoSchedulingAndThreading.DemoShareState(new EventLoopScheduler());
            //Task.Run(() =>
            //{
            //    System.Threading.Thread.Sleep(100);
            //    source.SubscribeConsole("Other ");
            //});

       

            //Observable.Range(1, 5)
            //    .Where(i => i % 2 == 0)
            //    .SelectMany(GetSubValues)
            //    .SubscribeConsole("dd");



            //subject.Any(x=>x>2).SubscribeConsole("Any");
            //while (true)
            //{
            //    if (int.TryParse(Console.ReadLine(), out int i)) {
            //        subject.OnNext(i);
            //    } else
            //    {
            //        subject.OnError(new Exception("You exit"));
            //        //subject.OnCompleted();
            //        //break;
            //    }
            //}

            //StartAction();
           Console.ReadKey();
        }

        private static void Done(IAsyncResult cookie)
        {
            Func <string,int> target = (Func<string,int>) cookie.AsyncState;
            var result=  target.EndInvoke(cookie);

            var ss = new ManualResetEvent(false);
            ss.Set();
            ss.Reset();

            var dd = new AutoResetEvent(false);
            dd.Set();


        }

        private static int Work(string s,int i)
        {
            return s.Length;
        }

        private static IObservable<long> GetSubValues(int offset)
        {
            return Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(4))
                .Select(x => (offset * 10) + x)
                .Take(3);
        }

        private static IObservable<T> RunningMin<T>( IObservable<T> source)
        {
            T minOf(T x, T y) => Comparer<T>.Default.Compare(x, y) < 0 ? x : y;
            var min = source.Scan(minOf).DistinctUntilChanged();
            return min;
        }

        private static IObservable<T> MySkip<T>(IObservable<T> source, int n)
        {
            return source.SelectMany((x, i) =>
             {
                 if (i < n) { return Observable.Return(x); }
                 else
                 {
                     return Observable.Empty<T>();
                 }
             });
        }

        

        private static void StartAction()
        {
            var start = Observable.Start(() =>
            {
                Console.Write("Working away");
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    Console.Write(".");
                }

            });
            start.Subscribe(
                unit => Console.WriteLine("Unit published"),
                () => Console.WriteLine("Action Completed."));
        }

        private static IEnumerable<IObservable<long>> GetSequences()
        {
            Console.WriteLine("GetSequeces() called");
            Console.WriteLine("Yield 1st sequence");
            yield return Observable.Create<long>(o =>
            {
                Console.WriteLine("1st subscribed to");
                return Observable.Timer(TimeSpan.FromMilliseconds(500))
                             .Select(i => 1L)
                             .Subscribe(o);
            });

            Console.WriteLine("Yield 2st sequence");
            yield return Observable.Create<long>(o =>
            {
                Console.WriteLine("2st subscribed to");
                return Observable.Timer(TimeSpan.FromMilliseconds(300))
                             .Select(i => 2L)
                             .Subscribe(o);
            });

            Console.WriteLine("Yield 3st sequence");
            yield return Observable.Create<long>(o =>
            {
                Console.WriteLine("3st subscribed to");
                return Observable.Timer(TimeSpan.FromMilliseconds(200))
                             .Select(i => 3L)
                             .Subscribe(o);
            });

            Console.WriteLine("GetSequences() complete");
        }

        private static void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        static async Task MainAsync(string[] args)
        {
            var b = new BusyNotifier();
            b.Subscribe(x => Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}: OnNext: {x}"));

            await Task.WhenAll(
                Task.Run(async () =>
                {
                    using (b.ProcessStart())
                    {
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}: Process1 started.");
                        await Task.Delay(TimeSpan.FromSeconds(1));
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}: Process1 finished.");
                    }
                }),
                Task.Run(async () =>
                {
                    using (b.ProcessStart())
                    {
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}: Process2 started.");
                        await Task.Delay(TimeSpan.FromSeconds(2));
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}: Process2 finished.");
                    }
                }));
        }

        static void RunMessageBroker()
        {
            // global scope pub-sub messaging
            MessageBroker.Default.Subscribe<MyClass>(x =>
            {
                Console.WriteLine("A:" + x);
            });

            var d = MessageBroker.Default.Subscribe<MyClass>(x =>
            {
                Console.WriteLine("B:" + x);
            });

            // support convert to IObservable<T>
            MessageBroker.Default.ToObservable<MyClass>().Subscribe(x =>
            {
                Console.WriteLine("C:" + x);
            });

            MessageBroker.Default.Publish(new MyClass { MyProperty = 100 });
            MessageBroker.Default.Publish(new MyClass { MyProperty = 200 });
            MessageBroker.Default.Publish(new MyClass { MyProperty = 300 });

            d.Dispose(); // unsubscribe
            MessageBroker.Default.Publish(new MyClass { MyProperty = 400 });
        }

        static async Task RunAsyncMessageBroker()
        {
            int aCount = 0;
            // asynchronous message pub-sub
            AsyncMessageBroker.Default.Subscribe<MyClass>(async x =>
            {
                Interlocked.Increment(ref aCount);
                Console.WriteLine("A:" + x + "  " + aCount.ToString());
                await Task.Delay(TimeSpan.FromSeconds(2));
                Interlocked.Increment(ref aCount);
            });

            var d = AsyncMessageBroker.Default.Subscribe<MyClass>(async x =>
            {
                Console.WriteLine("B:" + x);
                await Task.Delay(TimeSpan.FromSeconds(4));
            });

            // await all subscriber complete
            await AsyncMessageBroker.Default.PublishAsync(new MyClass { MyProperty = 100 });
            await AsyncMessageBroker.Default.PublishAsync(new MyClass { MyProperty = 200 });
            await AsyncMessageBroker.Default.PublishAsync(new MyClass { MyProperty = 300 });

            //d.Dispose(); // unsubscribe
            await AsyncMessageBroker.Default.PublishAsync(new MyClass { MyProperty = 400 });
        }

        static void WriteSequenceToConsole(IObservable<string> sequence)
        {
            //The next two lines are equivalent.
            sequence.Subscribe(value=>Console.WriteLine(value));
            //sequence.Subscribe(Console.WriteLine);
        }

        public static void TestScheduler(IScheduler scheduler)
        {
            scheduler.Schedule(Unit.Default,
            (s, _) => Console.WriteLine("Action1 - Thread:{0}",
            Thread.CurrentThread.ManagedThreadId));
            scheduler.Schedule(Unit.Default,
            (s, _) => Console.WriteLine("Action2 - Thread:{0}",
            Thread.CurrentThread.ManagedThreadId));
        }
    }
    public class MyClass
    {
        public int MyProperty { get; set; }

        public override string ToString()
        {
            return "MP:" + MyProperty;
        }
    }
    public static class ConsoleEx
    {
        public static IDisposable SubscribeConsole<T>(this IObservable<T> observable, string name)
        {
           return observable.Subscribe(x => Console.WriteLine($"{name} ->  {x}") ,
                                       ex => Console.WriteLine(name + " - " + ex.ToString()),
                                       () => Console.WriteLine($"{name} - OnCompledted() "));
        }

        public static IDisposable SubscribeConsoleJson<T>(this IObservable<T> observable, string name)
        {
            return observable.Subscribe(x => Console.WriteLine($"{name} ->  { JsonConvert.SerializeObject( x)}"),
                                        ex => Console.WriteLine(name + " - " + ex.ToString()),
                                        () => Console.WriteLine($"{name} - OnCompledted() "));
        }
    }

    public class DrasticChange
    {
        public decimal NewPrice { get; set; }
        public string Symbol { get; set; }
        public decimal ChangeRatio { get; set; }
        public decimal OldPrice { get; set; }
    }

    public class StockTick
    {
        public string QuoteSymbol { get; set; }
        public decimal Price { get; set; }

        //other properties
    }
    public class StockTicker 
    {
        public event EventHandler<StockTick> StockTick = delegate { };

        public void Notify(StockTick tick)
        {
            StockTick(this, tick);
        }
    }
}
