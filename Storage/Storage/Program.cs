using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Storage
{
    [Serializable]
    public class Client
    {
        public string name;
        public Client(string Name) => name = Name;
        public Client() => name = "Anonim";
        public override string ToString()
        {
            return name;
        }
    }
    [Serializable]
    public class Order
    {
        public string name;
        public double area;
        public DateTime dateStart;
        public DateTime dateEnd;
        public double price;
        public Client client;
        public Order(string Name, double Area, DateTime Date, TimeSpan time, Client cl, double k)
        {
            name = Name;
            area = Area;
            dateStart = Date;
            dateEnd = dateStart + time;
            client = cl;
            price = area * k * time.TotalDays;
        }
        public Order()
        {
            name = "Nothing";
            area = 0;
            price = 0;
            client = new Client();
        }
        public string Name { get { return name; } }
        public double Area { get { return area; } }
        public TimeSpan TimeToEnd { get { return dateEnd - DateTime.Now; } }
        public double Price { get { return price; } }
        public override string ToString()
        {
            return $"{name}\tArea: {price}\tDate of end: {dateEnd.Year}.{dateEnd.Month}.{dateEnd.Day}\tPrice: {price}\tClient: {client}";
        }
    }
    class Storage
    {
        double area;
        double rarea;
        double price;
        List<Order> orders;
        string dateBase;

        public Storage(double Area, double Price, string DateBase)
        {
            area = Area;
            rarea = area;
            price = Price;
            dateBase = DateBase + ".xml";
            orders = new List<Order>();
        }
        public double ResidualArea { get { return rarea; } }

        XmlSerializer GetXmlSerializer()
        {
            Type[] types = { typeof(Client) };
            return new XmlSerializer(typeof(List<Order>), types);
        }
        List<Order> GetOrders()
        {
            if (!File.Exists(dateBase))
                return new List<Order>();
            FileStream fs = new FileStream(dateBase, FileMode.OpenOrCreate, FileAccess.Read);
            using (TextReader tr = new StreamReader(fs))
            {
                XmlSerializer xml = GetXmlSerializer();
                return (List<Order>)xml.Deserialize(tr);
            }
        }
       
        public void AddOrder(Order order)
        {
            if (order.Area > ResidualArea)
                throw new Exception("All area is occupied");
            if (order == null)
                throw new ArgumentNullException();
            orders = GetOrders();
            orders.Add(order);
            FileStream fs = new FileStream(dateBase, FileMode.Create, FileAccess.Write);
            using (TextWriter tw = new StreamWriter(fs))
            {
                XmlSerializer xml = GetXmlSerializer();
                xml.Serialize(tw, orders);
            }
                
        }
        public void AddOrder(string Name, double Area, TimeSpan time, Client cl)
        {
            if(Area > ResidualArea)
                throw new Exception("All area is occupied");
            Order order = new Order(Name, Area, DateTime.Now, time, cl, price);
            orders = GetOrders();
            orders.Add(order);
            FileStream fs = new FileStream(dateBase, FileMode.Create, FileAccess.Write);
            using (TextWriter tw = new StreamWriter(fs))
            {
                XmlSerializer xml = GetXmlSerializer();
                xml.Serialize(tw, orders);
            }
        }

        public void PrintOrders()
        {
            foreach (var or in GetOrders())
                Console.WriteLine(or);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Storage str = new Storage(1000, 0.5, "orders");
                str.AddOrder("Meet", 10, new TimeSpan(TimeSpan.TicksPerDay * 10), new Client("Dima"));
                str.AddOrder("Milk", 50, new TimeSpan(TimeSpan.TicksPerDay * 7), new Client("Dana"));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
