using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace IgorPr_6_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Write somethin: ");
            string input = Console.ReadLine();
            string patern = @"(\b\d\w+ | \w+\d\b)";
            Console.WriteLine("Result: " + Regex.Match(input, patern).Length);

        }
    }
}
