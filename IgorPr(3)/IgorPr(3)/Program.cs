using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgorPr_3_
{
    class Program
    {
        static void Swap<T>(ref T a, ref T b)
        {
            T pr = a;
            a = b;
            b = pr;
        }
        static void Sort<T>(ref T[] arr) where T : IComparable
        {
            for(int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                    if (arr[i].CompareTo(arr[j]) < 0)
                        Swap(ref arr[i], ref arr[j]);
            }
        }


        class Product : IComparable

        {
            public string Name { get; set; }
            public double Price { get; set; }

            public Product(string n = "Nothing", double p = 0)
            {
                Name = n;
                Price = p;
            }

            public int CompareTo(object obj)
            {
                if (obj == null)
                    throw new ArgumentNullException();
                if (!(obj is Product))
                    throw new ArgumentException();
                return Price.CompareTo((obj as Product).Price);
            }
            public override string ToString()
            {
                return $"{Name}: {Price}";
            }
        }


        static void Main(string[] args)
        {
            Random rand = new Random();
            int[] arr = new int[10];

            for (int i = 0; i < arr.Length; i++)
                arr[i] = rand.Next(0, 41);

            foreach (var el in arr)
                Console.Write($"{el,3}");
            Console.WriteLine();

            Sort(ref arr);

            foreach (var el in arr)
                Console.Write($"{el,3}");
            Console.WriteLine();

            Product[] prod = new Product[] {
                new Product("Fish", rand.Next(40, 501)),
                new Product("Meet", rand.Next(30, 101)),
                new Product("Bread", rand.Next(8, 20))
            };

            foreach (var el in prod)
                Console.Write($"{el} ");
            Console.WriteLine();

            Sort(ref prod);

            foreach (var el in prod)
                Console.Write($"{el} ");
            Console.WriteLine();

        }
    }
}
