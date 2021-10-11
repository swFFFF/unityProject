using System;

namespace jinjie
{
    public delegate void MyDelegate(string str);
    class Program
    {
        private static event MyDelegate myEvent;
        /// <summary>
        /// 显示方法备注
        /// </summary>
        static void Print1(string str)
        {
            Console.WriteLine("Print1 {0}", str);
        }
        static void Print2(string str)
        {
            Console.WriteLine("Print2 {0}", str);
        }

        static void Main(string[] args)
        {
            myEvent += Print1;
            myEvent += Print2;
            myEvent("hello");
            myEvent?.Invoke("111");//myEvent 防空
            //MyDelegate myDelegate = new MyDelegate(Print1);
            //myDelegate += Print2;
            //myDelegate("111");
            Console.WriteLine("交换后 a:{0}, b:{1}");
        }
    }
}
