using System;
using System.Collections.Generic;

namespace Stacks
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stacks = new Stack<int>();
            stacks.Push(1);
            stacks.Push(2);
            stacks.Push(3);

            Console.WriteLine("peek {0}", stacks.Peek());
            Console.WriteLine("pop {0}", stacks.Pop());
            foreach (var i in stacks)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
