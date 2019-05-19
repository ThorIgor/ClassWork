using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Storage
{
    [Serializable]
    class Client
    {
        string name;

        public Client(string Name) => name = Name;
        public override string ToString() => name;
    }
    [Serializable]
    class Order
    {
        string name;
        double area;
        DateTime dateStart;
        DateTime dateEnd;
        double price;
        Client client;
        
        public Order(string Name, double Area, DateTime Start, TimeSpan Time, Client Client, double k)
        {
            name = Name;
            area = Area;
            dateStart = Start;
            dateEnd = dateStart + Time;
            client = Client;
            price = area * Time.Days * k;
        }
        public string Name { get { return name; } }
        public double Area { get { return area; } }
        public Client Client { get { return client; } }
        public override string ToString() => $"{name}\t{area}\t{dateEnd.Year}.{dateEnd.Month}.{dateEnd.Day}\t{price}\t{client}";
    }
    class Storage
    {
        double area;
        double residualArea;
        double price;
        string dateBaseActive;
        string dateBaseTakenOut;

        public Storage(double Area, double Price, string DateBase)
        {
            area = Area;
            residualArea = area;
            price = Price;
            dateBaseActive = DateBase + "Active.dat";
            dateBaseTakenOut = DateBase + "TakenOut.dat";
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                    return;
                price = value;
            }
        }
        public double ResidualArea { get { return residualArea; } }

        List<Order> GetOrdersActive()
        {
            try
            {
                using (var fs = new FileStream(dateBaseActive, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    return (List<Order>)bf.Deserialize(fs);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new List<Order>();
        }
        List<Order> GetOrdersTakenOut()
        {
            try
            {
                using (var fs = new FileStream(dateBaseTakenOut, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    return (List<Order>)bf.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new List<Order>();
        }

        bool SerializeActive(List<Order> orders)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (var fs = new FileStream(dateBaseActive, FileMode.Create, FileAccess.Write))
                    bf.Serialize(fs, orders);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        bool SerializeTakenOut(List<Order> orders)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (var fs = new FileStream(dateBaseTakenOut, FileMode.Create, FileAccess.Write))
                    bf.Serialize(fs, orders);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public void AddOrder(Order order)
        {
            List<Order> orders = GetOrdersActive();
            orders.Add(order);
            if(SerializeActive(orders))
                residualArea -= order.Area;
        }
        public void AddOrder(string Name, double Area, DateTime Start, TimeSpan Time, Client Client)
        {
            Order order = new Order(Name, Area, Start, Time, Client, price);
            if (order.Area > residualArea)
                throw new Exception("All area is occupied");
            List<Order> orders = GetOrdersActive();
            orders.Add(order);
            if (SerializeActive(orders))
                residualArea -= order.Area;
        }

        public void TakeOut(Order order)
        {
            List<Order> orders = GetOrdersActive();
            orders.Remove(order);
            if (SerializeActive(orders))
                residualArea += order.Area;
            orders = GetOrdersTakenOut();
            orders.Add(order);
            SerializeTakenOut(orders);
        }
        public void TakeOut(string Name, double Area, DateTime Start, TimeSpan Time, Client Client)
        {
            Order order = new Order(Name, Area, Start, Time, Client, price);
            List<Order> orders = GetOrdersActive();
            orders.Remove(order);
            if (SerializeActive(orders))
                residualArea += order.Area;
            orders = GetOrdersTakenOut();
            orders.Add(order);
            SerializeTakenOut(orders);
        }

        public void PrintActive()
        {
            foreach (var order in GetOrdersActive())
                Console.WriteLine(order);
        }
        public void PrintTakenOut()
        {
            foreach (var order in GetOrdersTakenOut())
                Console.WriteLine(order);
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
