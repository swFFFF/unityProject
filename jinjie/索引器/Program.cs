using System;

namespace 索引器
{

    class Program
    {
        public class TestIndex<T>
        {
            public T[] datas;

            public TestIndex(int size)
            {
                datas = new T[size];
            }

            public T this[int index]
            {
                get
                {
                    if (index >= datas.Length || index < 0)
                    {
                        Console.WriteLine("数组下标异常：{0}", index);
                        return datas[0];
                    }
                    return datas[index];
                }

                set
                {
                    if(index >= datas.Length || index < 0)
                    {
                        Console.WriteLine("数组下标异常：{0}", index);
                        return;
                    }
                    datas[index] = value;
                }
            }

        }

        static void Main(string[] args)
        {
            TestIndex<int> testIndex = new TestIndex<int>(2);
            testIndex[0] = 0;
            testIndex[1] = 1;
            testIndex.datas[1] = 2;
            for(int i = 0; i < testIndex.datas.Length; i++)
            {
                Console.WriteLine("hello {0}", testIndex[i]);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
