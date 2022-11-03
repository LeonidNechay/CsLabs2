using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"D:\CsLabs2\Lab2\XMLFile1.xml";

            ProductsList exportedproducts = new ProductsList();

            exportedproducts.Add("Wheat", "Ukraine", 112);
            exportedproducts.Add("Car", "Germany", 83);
            exportedproducts.Add("HIMARS", "USA", 10);
            exportedproducts.Add("Phone", "China", 83);
            exportedproducts.Add("Corn", "Ukraine", 92);

            exportedproducts.CreatePO(fileName);

            ProductsList exportedproducts2 = new ProductsList();

            exportedproducts2.ReadPO(fileName);

            foreach (Product exportproduct in exportedproducts2.exportedproducts)
            {
                Console.WriteLine(exportproduct.ToString());
            }

            exportedproducts2.FindByMostExpensive();

            Console.WriteLine();

            var task = exportedproducts.exportedproducts
            .GroupBy(group => group.Volume, group => group.Country)
            .Select(item => new { item.Key, Value = item.Count() });

            Console.WriteLine();
           exportedproducts2.TotalExportFromCountry();
        }
    }
    public class Product
    {
        protected string name;
        public string Name { get { return name; } set { name = value; } }

        protected string country;
        public string Country { get { return country; } set { country = value; } }

        protected float volume;
        public float Volume { get { return volume; } set { volume = (value >= 0 ? value : 0); } }

        public Product(string name, string country, float volume)
        {
            this.name = name;
            this.country = country;
            this.volume = volume;
        }
        public Product()
        {

        }
        public override string ToString()
        {
            return $"{country}'s total export: {volume} {name}";
        }
    }
    public class ProductsList
    {
        public List<Product> exportedproducts = new List<Product>();

        public ProductsList()
        {

        }
        public void Add(string product, string country, float volume)
        {
            exportedproducts.Add(new Product(product, country, volume));
        }
        public void CreatePO(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProductsList));
            FileStream file = new FileStream(filename, FileMode.OpenOrCreate);
            using (file)
            {
                serializer.Serialize(file, this);
            }
        }
        public void ReadPO(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProductsList));
            FileStream file = new FileStream(filename, FileMode.OpenOrCreate);
            using (file)
            {
                ProductsList obj = (ProductsList)serializer.Deserialize(file);
                this.exportedproducts = obj.exportedproducts;
            }
        }
        public void FindByMostExpensive()
        {

            var mostexpensive = this.exportedproducts.Max(item => item.Volume);

            float mostexpensiveproduct = mostexpensive;

            Console.WriteLine();
            Console.WriteLine($"Max product value: {mostexpensiveproduct}");
        }
        public void TotalExportFromCountry()
        {
            Dictionary<string, float> task = new Dictionary<string, float> { };
            foreach(Product item in exportedproducts)
            {
                if (task.ContainsKey(item.Country))
                {
                    task[item.Country]+=item.Volume;
                }
                else
                {
                    task.Add(item.Country, item.Volume);
                }
            }
            foreach(var item in task)
            {
                Console.WriteLine($"Total export from {item.Key}: {item.Value}");
            }
        }
    }
}
