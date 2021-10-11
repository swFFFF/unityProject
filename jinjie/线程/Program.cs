using System;
using System.Threading;
using System.Threading.Tasks;
namespace 线程
{
    class Program
    {
        static void TThreadCall()
        {
            Console.WriteLine("thread call");
            Thread.Sleep(5000);
            Console.WriteLine("THREAD CALL");
        }
        static void Main(string[] args)
        {
            Thread t = new Thread(TThreadCall);
            t.Start();
            //t.Join();
            t.Abort();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
