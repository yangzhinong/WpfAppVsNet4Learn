using MethodTimer;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DemoFody;

internal class Program
{
    static async Task Main(string[] args)
    {
        //var writer = new StringWriter();

        //var printer = Proxy.CreateProxy<HelloWorldPrinter>(async invocation =>
        //{
        //    await writer.WriteAsync("John says, \"");
        //    await invocation.Proceed();
        //    await writer.WriteAsync("\"");
        //    return null;
        //});
        ////await new HelloWorldPrinter().SayHello(writer);
        //await printer.SayHello(writer);

        //var s = writer.ToString();
        //await Console.Out.WriteLineAsync("ok");
        Add(5, 2);
        _= AddAsync(5, 4);

        Divide(3, 2);
        var myclass = new MyClass();
        myclass.MyMethod();
        Console.Read();
    }

    [Time]
    static int Add(int a, int b) => a + b;

    [Logging]
    static Task<int> AddAsync(int a, int b) => Task.FromResult(a + b);

    [Logging]
    static decimal Divide(decimal a, decimal b) => a / b;

}

public class MyClass
{
    [Time]
    public void MyMethod()
    {
        //Some code u are curious how long it takes
        Console.WriteLine("Hello");
    }
}

public class HelloWorldPrinter
{
    public async virtual Task SayHello(TextWriter writer)
    {
        await writer.WriteAsync("Hello World!");
    }
}
