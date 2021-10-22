using System;

namespace 测试
{
    enum enumTest
    {
        Up,
        Down,
        Left,
        Right,
    }
    class test
    {
        public test() { }
        public test(int a) { }
        public test(int a, string b) { }
        private int i = 1;
        public int j = 0;
        public string str = "123";
        public void tsetMove() { Console.WriteLine("In testMove"); }
    }
    class Program
    {
        public int a = 0;
        public string b = "1123";
        public void TestFun() { }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
