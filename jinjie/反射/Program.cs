using System;
using System.Reflection;

namespace 反射
{
    class TestClass
    {
        public int Test1(string test1, int test2)
        {
            Console.WriteLine("test1 call {0}, {1}", test1, test2);
            return 0;
        }

        private void Test2()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TestClass testClass = new TestClass();
            MethodInfo[] methodInfo = testClass.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public |BindingFlags.DeclaredOnly);

            for (int i = 0; i < methodInfo.Length; i++) 
            {
                Console.WriteLine("方法 {0}", methodInfo[i].Name);
                Console.WriteLine("参数 ", methodInfo[i].GetParameters());
                for (int j = 0; j < methodInfo[i].GetParameters().Length; j++)
                {
                    Console.WriteLine("名称 {0}， 类型：{1}", 
                        methodInfo[i].GetParameters()[j].Name, methodInfo[i].GetParameters()[j].ParameterType);
                }

                if(methodInfo[i])
                {

                }
                Console.WriteLine("返回值 ： {0}, 返回类型 {1}",methodInfo[i].ReturnParameter.Name, methodInfo[i].ReturnParameter.ParameterType);
            }
            Console.ReadLine();
        }
    }
}
