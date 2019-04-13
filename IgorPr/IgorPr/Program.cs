using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgorPr_1_
{
    delegate void Del(int a, ConsoleColor c);
    
    class Program
    {
        static void DrawTriangle(int height, ConsoleColor c)
        {
            Console.ForegroundColor = c;
            for (int i = 1; i <= height; i++)
            {
                for (int j = 1; j <= i; j++)
                    Console.Write("*");
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void DrawSquere(int height, ConsoleColor c)
        {
            Console.ForegroundColor = c;
            for (int i = 1; i <= height; i++)
            {
                for (int j = 1; j <= height; j++)
                    Console.Write("*");
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Main(string[] args)
        {
            Del d = DrawTriangle;
            d += DrawSquere;
            d(5, ConsoleColor.DarkYellow);


        }
    }
}
