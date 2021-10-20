using System;

namespace 委托
{
    #region 委托是什么
    //委托是 函数的容器
    //用来存储、传递函数
    //委托本质是一个类，用来定义函数的类型（返回值和参数的类型）
    #endregion
    class Program
    {
        class test
        {
            void TetsFun()
            {

            }
        }

        static void Main(string[] args)
        {
            test t = new test();
            t.TestFun();
            Console.WriteLine("Hello World!");
        }
        static void Fun()
        {
            Console.WriteLine("11");
        }
        static void Fun1()
        {
            Console.WriteLine("22");
        }

    }
}
