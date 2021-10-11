using System;
using System.Collections.Generic;
using System.Linq;

namespace 有序字典
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary<int, string> d = new SortedDictionary<int, string>();
            d.Add(1, "a");
            d.Add(3, "c");
            d.Add(4, "d");
            d.Add(2, "b");

            foreach(var i in d.Reverse())//翻转集合元素顺序
            {
                Console.WriteLine("key {0}, value {1}", i.Key, i.Value);
            }
            Console.ReadLine(); ;
        }
    }
}
