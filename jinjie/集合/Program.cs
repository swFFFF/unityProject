using System;
using System.Collections;
using System.Collections.Generic;

namespace 集合
{
    class Program
    {
        //非泛型集合
        ArrayList arrayList = new ArrayList();

        //泛型集合
        List<int> list = new List<int>();
        static LinkedList<int> linkedList = new LinkedList<int>();

        static void Main(string[] args)
        {
            linkedList.AddFirst(1);
            linkedList.AddLast(2);
            linkedList.AddAfter(linkedList.First, 3);

            foreach(var i in linkedList)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
