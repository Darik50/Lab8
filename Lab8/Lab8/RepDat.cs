using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace Lab8
{
    public class RepDat
    {
        string _path;
        public RepDat(string path)
        {
            _path = path;
            Check();
        }
        
        public bool Del(string Code)
        {
            var Doc = XDocument.Load(_path);
            Doc.Element("Products").Elements("Product").Where(x =>
                x.Attribute("Code").Value == Code).FirstOrDefault().Remove();
            Doc.Save(Path.Combine(Environment.CurrentDirectory, "Doc.xml"));
            return true;
        }
        public bool CreateDefFile()
        {
            List<Products> Tab = new List<Products> {
            new Products{NumWar = 1, Code = 11, Name = "Apple", Date = new DateTime(2020, 4, 10), Days = 30, Count = 12000, Price = 10  },
            new Products{NumWar = 2, Code = 12, Name = "Apple", Date = new DateTime(2020, 4, 12), Days = 30, Count = 14000, Price = 15  },
            new Products{NumWar = 2, Code = 13, Name = "Apple", Date = new DateTime(2020, 3, 14), Days = 30, Count = 19000, Price = 8  },
            new Products{NumWar = 1, Code = 14, Name = "Apple", Date = new DateTime(2020, 3, 15), Days = 30, Count = 11000, Price = 20  }};
            XDocument Doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Products"));
            foreach (var i in Tab)
            {
                Doc.Root.Add(new XElement("Product",
                    new XAttribute("Code", i.Code),
                    new XElement("NumWar", i.NumWar),
                    new XElement("Name", i.Name),
                    new XElement("Date", i.Date.ToString("yyyy.MM.dd")),
                    new XElement("Days", i.Days),
                    new XElement("Count", i.Count),
                    new XElement("Price", i.Price)));
            }
            Doc.Save(Path.Combine(Environment.CurrentDirectory, "Doc.xml"));
            return true;
        }
        public IEnumerable<Products> GetProd()
        {
            var Doc = from products in XDocument.Load(_path).Descendants("Product")
                      select new Products
                      {
                          Code = (int)products.Attribute("Code"),
                          NumWar = (int)products.Element("NumWar"),
                          Name = (string)products.Element("Name"),
                          Date = (DateTime)products.Element("Date"),
                          Days = (int)products.Element("Days"),
                          Count = (int)products.Element("Count"),
                          Price = (int)products.Element("Price")
                      };
            return Doc;
        }
        public IEnumerable<Products> Search(string NumWar, string Code, string Date, string Days)
        {
            var Doc = from products in XDocument.Load(_path).Descendants("Product")
                      where (products.Element("NumWar").Value == NumWar || NumWar == "")
                      && (products.Attribute("Code").Value == Code || Code == "")
                      && (products.Element("Date").Value == Date || Date == "")
                      && (products.Element("Days").Value == Days || Days == "")
                      select new Products
                      {
                          Code = (int)products.Attribute("Code"),
                          NumWar = (int)products.Element("NumWar"),
                          Name = (string)products.Element("Name"),
                          Date = (DateTime)products.Element("Date"),
                          Days = (int)products.Element("Days"),
                          Count = (int)products.Element("Count"),
                          Price = (int)products.Element("Price")
                      };
            return Doc;
        }
        public bool Check()
        {
            var Doc1 = from products in XDocument.Load(_path).Descendants("Product")
                       select new Products
                       {
                           Code = (int)products.Attribute("Code"),
                           NumWar = (int)products.Element("NumWar"),
                           Name = (string)products.Element("Name"),
                           Date = (DateTime)products.Element("Date"),
                           Days = (int)products.Element("Days"),
                           Count = (int)products.Element("Count"),
                           Price = (int)products.Element("Price")
                       };
            foreach (var i in Doc1)
            {
                int ErrNumWar = Convert.ToInt32(i.NumWar);
                int ErrCode = Convert.ToInt32(i.Code);
                string ErrName = i.Name;
                DateTime ErrDate = Convert.ToDateTime(i.Date);
                int ErrDays = Convert.ToInt32(i.Days);
                int ErrCount = Convert.ToInt32(i.Count);
                int ErrPrice = Convert.ToInt32(i.Price);
            }
            return true;
        }
        public bool Add(string Code, string NumWar, string Name, string Date, string Days, string Count, string Price)
        {
            var Doc = XDocument.Load(_path);
            Doc.Root.Add(new XElement("Product",
                    new XAttribute("Code", Code),
                    new XElement("NumWar", NumWar),
                    new XElement("Name", Name),
                    new XElement("Date", Date),
                    new XElement("Days", Days),
                    new XElement("Count", Count),
                    new XElement("Price", Price)));
            Doc.Save(Path.Combine(Environment.CurrentDirectory, "Doc.xml"));
            return true;
        }


    }
}
