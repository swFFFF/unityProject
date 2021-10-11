using System;


namespace TestExtensionMethods
{
    //①必须建一个静态类（任意有效命名），用来包含要添加的扩展方法

    public static class MyExtensions
    {
        //②要添加的扩展方法必须为一个静态方法
        public static int MyNewMethod(this string s,string s2)
        {
            return s2.Length;
        }

        public static string MyNewMethod(this int s)
        {
            return "haha";
        }
    }

    //测试扩展方法类
    class Program
    {
        static void Main(string[] args)
        {
            string str = "Hello Extension Methods in C# 3.0";
            Console.WriteLine(str);
            //③调用扩展方法,必须用对象来调用
            int len = str.MyNewMethod("11");
            string t = 1.MyNewMethod();
            Console.WriteLine(len);
            Console.WriteLine(t);
            Console.ReadKey();
        }
    }
}
