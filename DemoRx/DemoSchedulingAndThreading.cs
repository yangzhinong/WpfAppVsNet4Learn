using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace DemoRx
{
    public static class DemoSchedulingAndThreading
    {
        public static void Demo1()
        {
            Console.WriteLine("Starting on threadId:{0}", Thread.CurrentThread.ManagedThreadId);
            var subject = new Subject<object>();

            subject.Subscribe(
                o => Console.WriteLine("Received {1} on threadId:{0}",
                    Thread.CurrentThread.ManagedThreadId,
                    o));

            ParameterizedThreadStart notify = obj =>
            {
                Console.WriteLine("OnNext({1}) on threadId:{0}",
                                    Thread.CurrentThread.ManagedThreadId,
                                    obj);
                subject.OnNext(obj);
            };

            notify(1);
            new Thread(notify).Start(2);
            new Thread(notify).Start(3);
        }

        public static void Demo2()
        {
            Console.WriteLine("Starting on threadId:{0}", Thread.CurrentThread.ManagedThreadId);
            var source = Observable.Create<int>(
                o =>
                {
                    Console.WriteLine("Invoked on threadId:{0}", Thread.CurrentThread.ManagedThreadId);
                    o.OnNext(1);
                    o.OnNext(2);
                    o.OnNext(3);
                    o.OnCompleted();
                    Console.WriteLine("Finished on threadId:{0}",
                    Thread.CurrentThread.ManagedThreadId);
                    return Disposable.Empty;
                });

            source
                .SubscribeOn(Scheduler.ThreadPool)
                //.ObserveOn(DispatcherScheduler.Instance)
                .Subscribe(
                    o => Console.WriteLine("Received {1} on threadId:{0}",
                        Thread.CurrentThread.ManagedThreadId,
                        o),
                    () => Console.WriteLine("OnCompleted on threadId:{0}",
                        Thread.CurrentThread.ManagedThreadId));
            Console.WriteLine("Subscribed on threadId:{0}", Thread.CurrentThread.ManagedThreadId);

        }

        public static void Demo3()
        {
            var bufferSize = 4096;
            var source= new FileStream(@"c:\Somefile.txt", FileMode.Open, FileAccess.Read);
            var factory= Observable.FromAsyncPattern<byte[],int,int,int>(source.BeginRead, source.EndRead);

            var buffer = new byte[bufferSize];
            IObservable<int> reader = factory(buffer, 0, (int)source.Length);
            reader.Subscribe(bytesRead => Console.WriteLine("Read {0} bytes from file into buffer", bytesRead));
        }

        public static IObservable<byte> ToObservable(this FileStream source, int bufferSize, IScheduler scheduler)
        {
            var bytes = Observable.Create<byte>(o =>
            {
                var initialState = new StreamReaderState(source, bufferSize);
                var currentStateSubscription = new SerialDisposable();
                Action<StreamReaderState, Action<StreamReaderState>> iterator;
                iterator = (state, self) =>
                {
                    currentStateSubscription.Disposable= state.ReadNext()
                                    .Subscribe(bytesRead =>
                                    {
                                        for (var i = 0; i < bufferSize; i++)
                                        {
                                            o.OnNext(state.Buffer[i]);
                                        }
                                        if (bytesRead > 0)
                                        {
                                            self(state);
                                        } else
                                        {
                                            o.OnCompleted();
                                        }
                                    }, o.OnError);
                };
                var scheduledWork = scheduler.Schedule(initialState, iterator);
                return new CompositeDisposable(currentStateSubscription, scheduledWork);
            });

            return Observable.Using(() => source, _ => bytes);
        }

        public static void Demo4()
        {
            Console.WriteLine($"Main thread: {Environment.CurrentManagedThreadId}");
            var subject = new Subject<string>();

            subject.Subscribe(
                m => Console.WriteLine($"Received {m} on thread: {Environment.CurrentManagedThreadId}"));

            object sync = new object();
            ParameterizedThreadStart notify = arg =>
            {
                string message = arg?.ToString() ?? "null";
                Console.WriteLine(
                    $"OnNext({message}) on thread: {Environment.CurrentManagedThreadId}");
                lock (sync)
                {
                    subject.OnNext(message);
                }
            };

            notify("Main");
            new Thread(notify).Start("First worker thread");
            new Thread(notify).Start("Second worker thread");
        }

        public static void DemoDelay()
        {
            Console.WriteLine($"Main thread: {Environment.CurrentManagedThreadId}");
            var subject = new Subject<string>();

            subject
                .Delay(TimeSpan.FromSeconds(0.25))
                .Subscribe(
                m => Console.WriteLine($"Received {m} on thread: {Environment.CurrentManagedThreadId}"));

            object sync = new object();
            ParameterizedThreadStart notify = arg =>
            {
                string message = arg?.ToString() ?? "null";
                Console.WriteLine(
                    $"OnNext({message}) on thread: {Environment.CurrentManagedThreadId}");
                lock (sync)
                {
                    subject.OnNext(message);
                }
            };

            notify("Main 1");
            Thread.Sleep(TimeSpan.FromSeconds(0.1));
            notify("Main 2");
            Thread.Sleep(TimeSpan.FromSeconds(0.3));
            notify("Main 3");
            new Thread(notify).Start("First worker thread");
            Thread.Sleep(TimeSpan.FromSeconds(0.1));
            new Thread(notify).Start("Second worker thread");

            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        public static void Demo6()
        {
            
            Observable
                    .Range(1, 5)
                    .SelectMany(i => Observable.Range(i * 10, 5, TaskPoolScheduler.Default))
                    .Subscribe(
                    m => Console.WriteLine($"Received {m} on thread: {Environment.CurrentManagedThreadId}"));
        }

        public static void Demo6_1()
        {
            NaiveRange(1,5)
                    .SelectMany(i => NaiveRange(i * 10, 5))
                    .Subscribe(
                    m => Console.WriteLine($"Received {m} on thread: {Environment.CurrentManagedThreadId}"));
        }

        public static void Demo6_2()
        {

            Observable
                    .Range(1, 5, TaskPoolScheduler.Default)
                    //.SelectMany(i => Observable.Range(i * 10, 5))
                    .Subscribe(
                    m => Console.WriteLine($"Received {m} on thread: {Environment.CurrentManagedThreadId}"));
        }

        public static void Demo7()
        {
   
            Console.WriteLine($"Main thread: {Environment.CurrentManagedThreadId}");

            Observable.Range(1, 5)
                .SubscribeOn(new EventLoopScheduler((start) =>
                {
                    Thread t = new Thread(start) { IsBackground = false };
                    Console.WriteLine($"Created thread for EventLoopScheduler: {t.ManagedThreadId}");

                    return t;
                }))
                .Subscribe(
                    tick => Console.WriteLine($"{DateTime.Now}-{Environment.CurrentManagedThreadId}: Tick {tick}"));

            Console.WriteLine($"{DateTime.Now}-{Environment.CurrentManagedThreadId}: Main thread exiting");
        }

        public static void Demo8(IScheduler scheduler)
        {
            //var delay = TimeSpan.FromSeconds(1);
            //Console.WriteLine("Before schedue at {0:o}", DateTime.Now);


            //scheduler.Schedule(delay, () => Console.WriteLine("Inside schedule at {0:o}", DateTime.Now));

            //Console.WriteLine("After schedule at  {0:o}", DateTime.Now);

            Action<Action> work = (self) =>
            {
                Console.WriteLine("Running");
                self();
            };

            var token = scheduler.Schedule(work);
            Console.ReadLine();
            Console.WriteLine("Cancelling");
            token.Dispose();
            Console.WriteLine("Cancelled");

        }

        public static void Demo9(IScheduler scheduler)
        {
            var list= new List<int>();
            Console.WriteLine("Enter to quite:");
            var token = scheduler.Schedule(list, Work);
            Console.ReadLine();

            Console.WriteLine("Cancelling...");

            token.Dispose();
            Console.WriteLine("Cancelled");
        }


        public static IObservable<int> NaiveRange(int start, int count)
        {
            return System.Reactive.Linq.Observable.Create<int>(obs =>
            {
                for (int i = 0; i < count; i++)
                {
                    obs.OnNext(start + i);
                }

                return Disposable.Empty;
            });
        }

        public static void DemoShareState(IScheduler scheduler)
        {
            var myName = "Lee";
            scheduler.Schedule(() => Console.WriteLine("myName1 = {0}", myName));
            scheduler.Schedule(myName, (ss,state) => Console.WriteLine("myName2 = {0}", ss));
            myName = "John";
        }

        public static void DemoShareListState(IScheduler scheduler)
        {
            var list = new List<int>();
            
            scheduler.Schedule(list, (ss, innerSchedulder) => Console.WriteLine("myName2 = {0}", list.Count));
            list.Add(1);
        }

        public static IDisposable Work(IScheduler scheduler, List<int> list)
        {
            var tokenSource = new CancellationTokenSource();
            var cancelToken = tokenSource.Token;
            var task = new Task(() =>
            {
                Console.WriteLine();
                var sw = new SpinWait();
                for (int i = 0; i < 1000; i++)
                {
                    for (int j = 0; j < 3000; j++) sw.SpinOnce();

                    Console.Write(".");

                    list.Add(i);

                    if (cancelToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Cancellation requested");

                        // cancelToken.ThrowIfCancellationRequested();

                        return;
                    }
                }
            }, cancelToken);

            task.Start();

            return Disposable.Create(tokenSource.Cancel);
        }
    }



   
}
public sealed class StreamReaderState
{
    private readonly int _bufferSize;
    private readonly Func<byte[], int, int, IObservable<int>> _factory;

    public StreamReaderState(FileStream source, int bufferSize)
    {
        _bufferSize = bufferSize;
        _factory = Observable.FromAsyncPattern<byte[], int, int, int>(
            source.BeginRead,
            source.EndRead);
        Buffer = new byte[bufferSize];
    }

    public IObservable<int> ReadNext()
    {
        return _factory(Buffer, 0, _bufferSize);
    }

    public byte[] Buffer { get; set; }
}