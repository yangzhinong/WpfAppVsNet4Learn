using AspectInjector.Broker;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AopDemo
{
    public static class Exts
    {
        public static TaskAwaiter GetAwaiter(this TimeSpan ts)
        {
            return Task.Delay(ts).GetAwaiter();
        }

        public static TaskAwaiter GetAwaiter(this int second)
        {
            return Task.Delay(TimeSpan.FromSeconds(second)).GetAwaiter();
        }
    }

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await TimeSpan.FromSeconds(10);
            await 2;
            var o = new Target();
            if (o is IHaveProperty i)
            {
                i.Data = "hello";
            }

            Console.WriteLine();
        }
    }

    [MyAspect]
    public class Target
    {
        public void Do()
        { }
    }

    public interface IHaveProperty
    {
        string Data { get; set; }
    }

    [Aspect(Scope.Global)]
    [Injection(typeof(MyAspect))]
    [Mixin(typeof(IHaveProperty))]
    public class MyAspect : Attribute, IHaveProperty
    {
        public string Data { get; set; }
    }
}