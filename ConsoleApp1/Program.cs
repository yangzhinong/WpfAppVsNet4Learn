using MethodTimer;
using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var myclass = new MyClass();
            myclass.MyMethod();
            Console.Read();
        }
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
}
