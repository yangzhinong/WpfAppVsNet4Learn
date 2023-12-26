using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace WinformRxDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int count = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            var btn1EventStream = Observable.FromEventPattern<EventArgs>(button1, nameof(Button.Click))
                                            .Select(_=> 1);

            var btn2EventStream = Observable.FromEventPattern<EventArgs>(button2, nameof(Button.Click))
                                            .Select(_=> -1);

            btn1EventStream.Merge(btn2EventStream)
                            .Scan(0, (result, s) => result + s)
                            .Subscribe(x => this.Text = x.ToString());



            //var mouseUp= Observable.FromEventPattern<MouseEventArgs>(this, nameof(MouseUp));

            //var movements= Observable.FromEventPattern<MouseEventArgs> (this, nameof(MouseMove));


        }
        
       
    }



    public static class ConsoleEx
    {
        public static IDisposable SubscribeConsole<T>(this IObservable<T> observable, string name)
        {
            return observable.Subscribe(x => Console.WriteLine($"{name} -> {x}"));
        }
    }
}
