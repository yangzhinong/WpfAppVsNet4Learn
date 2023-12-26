using System;
using System.Diagnostics;

namespace DemoRx
{
    public class TimerIt : IDisposable
    {
        private readonly string _name;
        private readonly Stopwatch _watch;

        public TimerIt(string name)
        {
            _name= name;
            _watch = Stopwatch.StartNew();
        }


        public void Dispose()
        {
            _watch.Stop();
            Console.WriteLine($"{_name} took {_watch.Elapsed}");
        }
    }
}
