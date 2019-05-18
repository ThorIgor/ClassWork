using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

namespace IgorPr_7_
{

    [Serializable]
    public class Product
    {
        public Product(string name = "Nothing", double price = 0, DateTime date = new DateTime())
        {
            Name = name;
            Price = price;
            Date = date;
        }
        public Product()
        {
            Name = "Nothing";
            Price = 0;
            Date = new DateTime();
        }
        public string Name;
        public double Price;
        public DateTime Date;
        public override string ToString()
        {
            return Name + "(" + Date.Year + "." + Date.Month + "." + Date.Day + "): " + Price;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Product pr1 = new Product("Meet", 25.5, new DateTime(2019, 4, 20)), pr2 = new Product();
            //Console.WriteLine("Pr1 - " + pr1);
            //using (FileStream fs = new FileStream("Product.dat", FileMode.OpenOrCreate))
            //{
            //    BinaryFormatter br = new BinaryFormatter();
            //    br.Serialize(fs, pr1);
            //    fs.Position = 0;
            //    pr2 = (Product)br.Deserialize(fs);
            //}
            //Console.WriteLine("Pr2 - " + pr2);

            List<Product> prod = new List<Product> {
                new Product("Meet", 37.5, new DateTime(2019, 5, 12)),
                new Product("Milk", 17.5, new DateTime(2019, 5, 9)),
                new Product("Fish", 62.3, new DateTime(2019, 4, 27)),
            }, result = new List<Product>();
            XmlSerializer xs = new XmlSerializer(typeof(List<Product>));
            using (StreamWriter sw = new StreamWriter("prod.xml"))
                xs.Serialize(sw, prod);
            using (StreamReader sr = new StreamReader("prod.xml"))
            {
                result = (List<Product>)xs.Deserialize(sr);
                foreach (var el in result)
                    Console.WriteLine(el);
            }
        }
    }
}
