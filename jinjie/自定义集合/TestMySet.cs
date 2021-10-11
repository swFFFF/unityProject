using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 自定义集合迭代器
{
    class TestMySet
    {
        static void Main(string[] args)
        {
            MySet<int> mySet = new MySet<int>();
            mySet.Add(1);
            mySet.Add(2);
            mySet.Add(3);
            mySet.Add(2);

            //mySet.Remove(3);

            //for(int i = 0; i < mySet.Count; i++)
            //{
            //    Console.WriteLine("k {0}, v {1}", i, mySet[i]);
            //}

            foreach(var i in mySet)
            {
                Console.WriteLine("v {0}", i);
            }

            mySet.Clear();

            for (int i = 0; i < mySet.Count; i++)
            {
                Console.WriteLine("k {0}, v {1}", i, mySet[i]);
            }
            Console.ReadLine();
        }
    }
}
