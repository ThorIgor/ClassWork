using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Market
{
    [Serializable]
    abstract class Goods
    {
        protected string name;
        public string Name { get { return name; } }
        protected double price;
        public double Price { get { return price; } }
        public uint Quantity { get; set; }
        public void SetPrice(double p)
        {
            if (p < 0)
                throw new Exception("Price < 0");
            else
                price = p;
        }
 
    }
    [Serializable]
    class Produkt : Goods, IComparable
    {
        public enum ProduktCategory {Milk, Meet, Fish, Bread, Sweets};
        DateTime expirationDate;
        ProduktCategory category;
        public ProduktCategory Category { get { return category; } }

        public Produkt(string n, double p, uint q, DateTime eDate, ProduktCategory c)
        {
            name = n;
            price = p;
            Quantity = q;
            expirationDate = eDate;
            category = c;
            
        }
        public override string ToString()
        {
            return $"{name} {price} {Quantity} Expiration date: {expirationDate.Day}.{expirationDate.Month}.{expirationDate.Year} Category: {category.ToString()}";
        }
        public int CompareTo(object obj)
        {
            if (obj is Produkt)
                return  this.Category - (obj as Produkt).Category;
            else
                return int.MinValue;
        }
    }
    [Serializable]
    class Ware : Goods, IComparable
    {
        public enum WareCategory { Chemistry, Textile, Stationery }
        WareCategory category;
        public WareCategory Category { get { return category; } }

        public Ware(string n, double p, uint q, WareCategory c)
        {
            name = n;
            price = p;
            Quantity = q;
            category = c;
        }
        public override string ToString()
        {
            return $"{name} {price} {Quantity} Category: {category.ToString()}";
        }
        public int CompareTo(object obj)
        {
            if (obj is Ware)
                return this.Category - (obj as Ware).Category;
            else
                return int.MinValue;
        }
    }

    class Market
    {
        string dateBase;
        public string DateBase { get { return dateBase; } }

        public Market(string dateB) => dateBase = dateB + ".dat";

        public void AddGoods(Goods goods)
        {
            var G = Goods;
            if (G.Exists((x) => x.Name == goods.Name))
            {
                int i = G.FindIndex((x) => x.Name == goods.Name);
                G[i].Quantity += goods.Quantity;
                G[i].SetPrice(goods.Price);
            }
            else
                G.Add(goods);
            Goods = G;
        }
        public void AddRangeGoods(IEnumerable<Goods> goods)
        {
            var G = Goods;
            foreach(var g in goods)
            {
                if (G.Exists((x) => g.Name == x.Name))
                {
                    int i = G.FindIndex((x) => x.Name == g.Name);
                    G[i].Quantity += g.Quantity;
                    G[i].SetPrice(g.Price);
                }
                else
                    G.Add(g);
            }
            Goods = G;
        }
        public void Remove(string name)
        {
            var G = Goods;
            G.Remove(G.Find((x) => x.Name == name));
            Goods = G;
        }

        List<Goods> Goods
        {
            get
            {
                if (!File.Exists(dateBase))
                    return new List<Goods>();
                using (var FS = new FileStream(dateBase, FileMode.Open))
                {
                    BinaryFormatter BF = new BinaryFormatter();
                    return BF.Deserialize(FS) as List<Goods>;
                }

            }

            set
            {
                using (var FS = new FileStream(dateBase, FileMode.Create))
                {
                    BinaryFormatter BF = new BinaryFormatter();
                    BF.Serialize(FS, value);
                }
            }
        }
        void SortDateBase()
        {
            var produkts = Goods.Where((x) => x is Produkt).ToList().ConvertAll(new Converter<Goods, Produkt>((x) => x as Produkt) );
            var wares = Goods.Where((x) => x is Ware).ToList().ConvertAll(new Converter<Goods, Ware>((x) => x as Ware));
            produkts.Sort();
            wares.Sort();
            var goods = new List<Goods>();
            goods.AddRange(produkts);
            goods.AddRange(wares);
            Goods = goods;
        }

        public void Print()
        {
            SortDateBase();
            foreach(var el in Goods)
            {
                if (el is Produkt)
                    Console.WriteLine(el as Produkt);
                else
                    Console.WriteLine(el as Ware);
            }
        }
    }

    class Program
    { 
        static void Main(string[] args)
        {
            Market market = new Market("Market");
            market.AddGoods(new Ware("Domes", 34.8, 50, Ware.WareCategory.Chemistry));
            market.AddGoods(new Produkt("Mouv", 89.3, 20, new DateTime(2019, 4, 27), Produkt.ProduktCategory.Fish));
            market.AddGoods(new Produkt("Radumo", 19.7, 20, new DateTime(2019, 4, 27), Produkt.ProduktCategory.Milk));
            market.Print();
        }
    }
}
