using System;
using System.Collections.Generic;

namespace 队列
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            Console.WriteLine(queue.Peek());
            Console.WriteLine(queue.Dequeue());

            foreach (var i in queue)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
