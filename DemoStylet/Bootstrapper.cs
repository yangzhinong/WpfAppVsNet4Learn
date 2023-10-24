using Stylet;
using StyletIoC;

namespace DemoStylet
{
    public class Bootstrapper:Bootstrapper<MainShellViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
           // base.ConfigureIoC(builder);
        }

        protected override void Configure()
        {
           // base.Configure();
        }
    }
}
