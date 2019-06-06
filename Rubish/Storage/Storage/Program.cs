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
            Close = false;
        }
        public string Name { get { return name; } }
        public double Area { get { return area; } }
        public Client Client { get { return client; } }
        public DateTime DateStart { get { return dateStart;} }
        public DateTime DateEnd { get { return dateEnd; } }
        public double Price { get { return price; } }
        public bool Close { get; set; }
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
            File.Create(dateBaseActive);
            File.Create(dateBaseTakenOut);
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
        } //Set or get price for storage
        public double ResidualArea { get {
                var orders = GetOrdersActive();
                residualArea = area;
                foreach (var order in orders)
                    residualArea -= order.Area;
                return residualArea;
            } }

        public List<Order> GetOrdersActive()
        {
            try
            {
                using (var fs = new FileStream(dateBaseActive, FileMode.Open, FileAccess.Read))
                {
                    if (File.Exists(dateBaseActive))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        return (List<Order>)bf.Deserialize(fs);
                    }
                    else
                        File.Create(dateBaseActive);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new List<Order>();
        } // Get list of orders from dataBaseActive
        List<Order> GetOrdersTakenOut()
        {
            try
            {
                using (var fs = new FileStream(dateBaseTakenOut, FileMode.Open, FileAccess.Read))
                {
                    if (File.Exists(dateBaseTakenOut))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        return (List<Order>)bf.Deserialize(fs);
                    }
                    else
                        File.Create(dateBaseTakenOut);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new List<Order>();
        } // Get list of orders from dateBaseTakeOut

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
            bool dop = true;
            foreach (var ord in orders)
            {
                if (order == ord)
                {
                    dop = false;
                    break;
                }
            }
            if (dop)
                throw new Exception("Order isnt there");
            order.Close = true;
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
            order.Close = true;
            List<Order> orders = GetOrdersActive();
            orders.Remove(order);
            if (SerializeActive(orders))
                residualArea += order.Area;
            orders = GetOrdersTakenOut();
            orders.Add(order);
            SerializeTakenOut(orders);
        }

        public Order GetOrder(int i)
        {
            var orders = GetOrdersActive();
            return orders[i];
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
        public void PrintDebtors()
        {
            List<Order> orders = GetOrdersActive().FindAll((x) => x.DateEnd < DateTime.Now);
            foreach (var order in orders)
                Console.WriteLine(order);
        }
        public void PrintEndingOrder()
        {
            List<Order> orders = GetOrdersActive().FindAll((x) => x.DateEnd - DateTime.Now < TimeSpan.FromDays(2));
            foreach (var order in orders)
                Console.WriteLine(order);
        }
        public void PrintReport(DateTime from, DateTime to)
        {
            double profit = 0;
            List<Order> orders = (GetOrdersActive().Concat(GetOrdersTakenOut()) as List<Order>).FindAll((x) => x.DateEnd > from && x.DateEnd < to);
            foreach (var order in orders)
            {
                Console.Write(order);
                if (order.Close)
                {
                    Console.WriteLine($" Closed");
                    profit += order.Price;
                }
                else
                    Console.WriteLine($" Opened");
            }
            Console.WriteLine($"Orders: {orders.Count}");
            Console.WriteLine($"Profit: {profit}");
        }

    }
    class Program
    {
        class ConsoleMenu
        {
            uint maxP;
            uint pozM;
            uint menu;
            Storage storage;
            public ConsoleMenu(Storage st)
            {
                storage = st;
                pozM = 0;
                menu = 0;
            }

            int Move(ConsoleKey m)
            {
                switch(m)
                {
                    case ConsoleKey.DownArrow:
                        if (pozM < maxP)
                            pozM++;
                        return -1;
                    case ConsoleKey.UpArrow:
                        if (pozM > 0)
                            pozM--;
                        return -1;
                    case ConsoleKey.Enter:
                        return (int)pozM;
                    default: return int.MinValue;
                }
            } 
            void Enable(uint n)
            {
                if (pozM == n)
                    Console.Write(">");
                maxP = n;
            }
            void CreatOrder()
            {
                try
                {
                    Console.Clear();
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Area: ");
                    double area = double.Parse(Console.ReadLine());
                    Console.Write("Date: ");
                    DateTime date = DateTime.Parse(Console.ReadLine());
                    Console.Write("Time: ");
                    TimeSpan time = TimeSpan.FromDays(double.Parse(Console.ReadLine()));
                    Console.Write("Client: ");
                    Client client = new Client(Console.ReadLine());
                    storage.AddOrder(name, area, date, time, client);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            public void Draw()
            {
                Console.Clear();
                if(menu == 0)
                {
                    Console.WriteLine("Storage");
                    Console.WriteLine($"Time: {DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}\tResidual area: {storage.ResidualArea}\tPrice: {storage.Price} ");
                    Enable(0);
                    Console.WriteLine("Add order");
                    Enable(1);
                    Console.WriteLine("Take out order");
                    Enable(2);
                    Console.WriteLine("Reports");
                    Enable(3);
                    Console.WriteLine("Exit");
                }
                if(menu == 1)
                {
                    var orders = storage.GetOrdersActive();
                    Enable(0);
                    Console.WriteLine("Back");
                    for(int i = 0; i < orders.Count; i++)
                    {
                        Enable((uint)i + 1);
                        Console.WriteLine($"{i+1}." + orders[i]);
                    }
                }
                if(menu == 2)
                {
                    Console.WriteLine("Reports");
                    Enable(0);
                    Console.WriteLine("All orders");
                    Enable(1);
                    Console.WriteLine("Opened orders");
                    Enable(2);
                    Console.WriteLine("Closed orders");
                    Enable(3);
                    Console.WriteLine("Debtors");
                    Enable(4);
                    Console.WriteLine("Ending orders");
                    Enable(5);
                    Console.WriteLine("Back");
                }
            }
            public bool Input()
            {
                try
                {
                    int dop = Move(Console.ReadKey().Key);
                    if (dop >= 0)
                    {
                        switch (menu)
                        {
                            case 0:
                                if (dop == 0)
                                    CreatOrder();
                                if (dop == 1)
                                    menu = 1;
                                if (dop == 2)
                                    menu = 2;
                                if (dop == 3)
                                    return true;
                                pozM = 0;
                                return false;
                            case 1:
                                if (dop == 0)
                                    menu = 0;
                                else
                                    storage.TakeOut(storage.GetOrder(dop - 1));
                                pozM = 0;
                                return false;
                            case 2:
                                if (dop == 0)
                                {
                                    storage.PrintActive();
                                    storage.PrintTakenOut();
                                    Console.ReadKey();
                                }
                                if (dop == 1)
                                {
                                    storage.PrintActive();
                                    Console.ReadKey();
                                }
                                if (dop == 2)
                                {
                                    storage.PrintTakenOut();
                                    Console.ReadKey();
                                }
                                if (dop == 3)
                                    storage.PrintDebtors();
                                if (dop == 4)
                                    storage.PrintEndingOrder();

                                if (dop == 5)
                                    menu = 0;
                                pozM = 0;
                                return false;
                            default:
                                return false;
                        }
                    }
                    else
                        return false;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return false;
            }
        }
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Area: ");
                double area = double.Parse(Console.ReadLine());
                Console.Write("Price: ");
                double price = double.Parse(Console.ReadLine());
                Storage storage = new Storage(area, price, "Storage");
                ConsoleMenu menu = new ConsoleMenu(storage);
                bool end = false;
                while (!end)
                {
                    menu.Draw();
                    end = menu.Input();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
