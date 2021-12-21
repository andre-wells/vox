using System;
using System.Collections.Generic;
using System.Linq;

namespace Question1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            for (int i = -1; i < 10; i++)
            {
                Console.WriteLine($"For Range {i} to 10: {Foo(Enumerable.Range(i, 10))}");
            }            
        }

        static int Foo(IEnumerable<int> l)
        {
            // Returns the smallest positive integer (1 or larger) that does *not* occur in the list
            // This assumes that 1 is a valid response for arguments where 1 *is* in the list.

            var min = l.Min();
            if (min - 1 > 1)
                return min - 1;
            else
                return 1;            
        }
    }
}
