using SexyProxy;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DemoFody
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var writer = new StringWriter();

            var printer = Proxy.CreateProxy<HelloWorldPrinter>(async invocation =>
            {
                await writer.WriteAsync("John says, \"");
                await invocation.Proceed();
                await writer.WriteAsync("\"");
                return null;
            });
            //await new HelloWorldPrinter().SayHello(writer);
            await printer.SayHello(writer);

            var s = writer.ToString();
            await Console.Out.WriteLineAsync("ok");
            Console.Read();
        }
    }


    public class HelloWorldPrinter
    {
        public async virtual Task SayHello(TextWriter writer)
        {
            await writer.WriteAsync("Hello World!");
        }
    }
}
