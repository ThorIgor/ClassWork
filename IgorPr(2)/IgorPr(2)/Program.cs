using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgorPr_2_
{
    delegate void GreenhouseDel(Greenhouse gh); 
    class Greenhouse
    {
        double curT, maxT, minT;
        public event GreenhouseDel TooHot, TooCold, Well;

        public Greenhouse(double curt, double maxt, double mint)
        {
            curT = curt;
            maxT = maxt;
            minT = mint;
        }
        public double CurrentT
        {
            get { return curT; }
            set
            {
                curT = value;
                if (curT > maxT)
                    TooHot(this);
                else if (curT < minT)
                    TooCold(this);
                else
                    Well(this);
            }
        }
        public void print()
        {
            Console.WriteLine($"Temperature: {curT}, Max temperatur: {maxT}, Min temperature: {minT}");
        }
    }
    class Heater
    {
        public static void Warm(Greenhouse gh)
        {
            Console.WriteLine($"Heater is working. Temperature: {gh.CurrentT}");
            gh.CurrentT += 5;
           
        }
    }
    class Cooler
    {
        public static void Cool(Greenhouse gh)
        {
            Console.WriteLine($"Cooler is working. Temperature: {gh.CurrentT}");
            gh.CurrentT -= 5;
           
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            Greenhouse house = new Greenhouse(20, 28, 24);
            house.TooCold += Heater.Warm;
            house.TooHot += Cooler.Cool;
            house.Well += (Greenhouse gh) => Console.WriteLine($"Temperature is well. Temperature: {gh.CurrentT}");
            while (true)
            {
                Console.Clear();
                double TemChange = rand.Next(-5, 6);
                house.print();
                Console.WriteLine($"Temperature change: {TemChange}");
                house.CurrentT += TemChange;
                string end = Console.ReadLine();
                if (end == "exit" || end == "Exit")
                    break;
            }
        }
    }
}
