using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab8;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Lab8Tests
{
    [TestClass]
    public class UnitTest1
    {
        RepDat rd;
        string testpath = @"C:\Users\Znatok\Desktop\test.xml";

        public void createFile(List<Products> Tab)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Products"));
            foreach (var i in Tab)
            {
                doc.Root.Add(new XElement("Product",
                    new XAttribute("Code", i.Code),
                    new XElement("NumWar", i.NumWar),
                    new XElement("Name", i.Name),
                    new XElement("Date", i.Date.ToString("yyyy.MM.dd")),
                    new XElement("Days", i.Days),
                    new XElement("Count", i.Count),
                    new XElement("Price", i.Price)));
            }
            doc.Save(testpath);
        }

        public bool ListsEqual(List<Products> l1, List<Products> l2)
        {
            if (l1.Count != l2.Count)
            {
                return false;
            }

            for (int i = 0; i < l1.Count; i++)
            {
                if (l2[i].NumWar != l1[i].NumWar ||
                   l2[i].Code != l1[i].Code ||
                   l2[i].Name != l1[i].Name ||
                   l2[i].Date != l1[i].Date ||
                   l2[i].Days != l1[i].Days ||
                   l2[i].Count != l1[i].Count ||
                   l2[i].Price != l1[i].Price)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Default data: Compare input and real data.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            List<Products> tab = new List<Products> {
            new Products{NumWar = 1, Code = 11, Name = "Apple", Date = new DateTime(2020, 4, 10), Days = 30, Count = 12000, Price = 10  },
            new Products{NumWar = 2, Code = 12, Name = "Peach", Date = new DateTime(2020, 4, 12), Days = 30, Count = 14000, Price = 15  },
            new Products{NumWar = 2, Code = 13, Name = "Pineapple", Date = new DateTime(2020, 3, 14), Days = 30, Count = 19000, Price = 8  },
            new Products{NumWar = 1, Code = 14, Name = "Melon", Date = new DateTime(2020, 3, 15), Days = 30, Count = 11000, Price = 20  }};

            createFile(tab);

            rd = new RepDat(testpath);

            var res = rd.GetProd();

            List<Products> res2 = new List<Products>();

            res2.AddRange(res);

            if(!ListsEqual(res2, tab))
            {
                Assert.Fail();
            }

        }

        /// <summary>
        /// Repeating code in file
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            List<Products> tab = new List<Products> {
            new Products{NumWar = 1, Code = 11, Name = "Apple", Date = new DateTime(2020, 4, 10), Days = 30, Count = 12000, Price = 10  },
            new Products{NumWar = 2, Code = 11, Name = "Peach", Date = new DateTime(2020, 4, 12), Days = 30, Count = 14000, Price = 15  }};

            createFile(tab);

            rd = new RepDat(testpath);

            bool exceptionOccured = false;

            try
            {
                var res = rd.GetProd();
            }
            catch
            {
                exceptionOccured = true;
            }

            if (!exceptionOccured)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Empty name in file
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            List<Products> tab = new List<Products> {
            new Products{NumWar = 1, Code = 11, Name = "", Date = new DateTime(2020, 4, 10), Days = 30, Count = 12000, Price = 10  }};

            createFile(tab);

            rd = new RepDat(testpath);

            try
            {
                var res = rd.GetProd();
            }
            catch
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Search normal
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        {
            List<Products> tab = new List<Products> {
                new Products{NumWar = 1, Code = 11, Name = "Apple", Date = new DateTime(2020, 4, 10), Days = 30, Count = 12000, Price = 10  },
                new Products{NumWar = 1, Code = 12, Name = "Peach", Date = new DateTime(2020, 4, 12), Days = 30, Count = 14000, Price = 15  }};

            createFile(tab);

            rd = new RepDat(testpath);

            List<Products> expected = new List<Products> {
                new Products { NumWar = 1, Code = 11, Name = "Apple", Date = new DateTime(2020, 4, 10), Days = 30, Count = 12000, Price = 10 } };

            try
            {
                List<Products> res = new List<Products>();

                res.AddRange(rd.Search("", "11", "", ""));

                if(!ListsEqual(res, expected))
                {
                    Assert.Fail();
                }
            }
            catch
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Search absent data
        /// </summary>
        [TestMethod]
        public void TestMethod5()
        {
            List<Products> tab = new List<Products> {
                new Products{NumWar = 1, Code = 11, Name = "Apple", Date = new DateTime(2020, 4, 10), Days = 30, Count = 12000, Price = 10  },
                new Products{NumWar = 1, Code = 12, Name = "Peach", Date = new DateTime(2020, 4, 12), Days = 30, Count = 14000, Price = 15  }};

            createFile(tab);

            rd = new RepDat(testpath);

            List<Products> expected = new List<Products>();

            try
            {
                List<Products> res = new List<Products>();

                res.AddRange(rd.Search("", "45", "", ""));

                if(!ListsEqual(res, expected))
                {
                    Assert.Fail();
                }
            }
            catch
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Search wrong fromat data
        /// </summary>
        [TestMethod]
        public void TestMethod6()
        {
            List<Products> tab = new List<Products> {
                new Products{NumWar = 1, Code = 11, Name = "Apple", Date = new DateTime(2020, 4, 10), Days = 30, Count = 12000, Price = 10  },
                new Products{NumWar = 1, Code = 12, Name = "Peach", Date = new DateTime(2020, 4, 12), Days = 30, Count = 14000, Price = 15  }};

            createFile(tab);

            rd = new RepDat(testpath);

            List<Products> expected = new List<Products>();

            bool exceptionOccured = false;

            try
            {
                List<Products> res = new List<Products>();

                res.AddRange(rd.Search("", "Ildar", "", ""));
            }
            catch
            {
                exceptionOccured = true;
            }

            if (!exceptionOccured)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Add normal data
        /// </summary>
        [TestMethod]
        public void TestMethod7()
        {
            List<Products> tab = new List<Products> {
            new Products{NumWar = 1, Code = 11, Name = "Apple", Date = new DateTime(2020, 4, 10), Days = 30, Count = 12000, Price = 10  }};

            createFile(tab);

            rd = new RepDat(testpath);

            List<Products> expected = new List<Products> {
                new Products { NumWar = 1, Code = 12, Name = "Peach", Date = new DateTime(2020, 4, 10), Days = 35, Count = 50, Price = 10 } };

            try
            {
                rd.Add("12", "1", "Peach", new DateTime(2020, 4, 10).ToString("yyyy.MM.dd"), "35", "50", "10");

                List<Products> res = new List<Products>();
                res.AddRange(rd.Search("", "12", "", ""));

                if (!ListsEqual(res, expected))
                {
                    Assert.Fail();
                }
            }
            catch
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Add wrong format data
        /// </summary>
        [TestMethod]
        public void TestMethod8()
        {
            List<Products> tab = new List<Products> {
            new Products{NumWar = 1, Code = 11, Name = "Apple", Date = new DateTime(2020, 4, 10), Days = 30, Count = 12000, Price = 10  }};

            createFile(tab);

            rd = new RepDat(testpath);

            bool exceptionOccured = false;

            try
            {
                rd.Add("Ildar", "Ogorodnik", "45", new DateTime(2020, 4, 10).ToString("yyyy.MM.dd"), "LoL", "KeK", "ChebureK");
            }
            catch
            {
                exceptionOccured = true;
            }

            if (!exceptionOccured)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Delete normal data
        /// </summary>
        [TestMethod]
        public void TestMethod9()
        {
            List<Products> tab = new List<Products> {
                new Products{NumWar = 1, Code = 11, Name = "Apple", Date = new DateTime(2020, 4, 10), Days = 30, Count = 12000, Price = 10  },
                new Products{NumWar = 1, Code = 12, Name = "Peach", Date = new DateTime(2020, 4, 12), Days = 30, Count = 14000, Price = 15  }};

            createFile(tab);

            List<Products> expected = new List<Products>{
                new Products{NumWar = 1, Code = 11, Name = "Apple", Date = new DateTime(2020, 4, 10), Days = 30, Count = 12000, Price = 10  }};
            
            rd = new RepDat(testpath);

            try
            {               
                rd.Del("12");

                List<Products> res = new List<Products>();
                res.AddRange(rd.GetProd());

                if (!ListsEqual(res, expected))
                {
                    Assert.Fail();
                }
            }
            catch
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Delete absent data
        /// </summary>
        [TestMethod]
        public void TestMethodA()
        {
            List<Products> tab = new List<Products> {
                new Products{NumWar = 1, Code = 11, Name = "Apple", Date = new DateTime(2020, 4, 10), Days = 30, Count = 12000, Price = 10  },
                new Products{NumWar = 1, Code = 12, Name = "Peach", Date = new DateTime(2020, 4, 12), Days = 30, Count = 14000, Price = 15  }};

            createFile(tab);

            List<Products> expected = tab;

            rd = new RepDat(testpath);

            try
            {
                rd.Del("45");

                List<Products> res = new List<Products>();
                res.AddRange(rd.GetProd());

                if (!ListsEqual(res, expected))
                {
                    Assert.Fail();
                }
            }
            catch
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Delete wrong format data
        /// </summary>
        [TestMethod]
        public void TestMethodB()
        {
            List<Products> tab = new List<Products> {
                new Products{NumWar = 1, Code = 11, Name = "Apple", Date = new DateTime(2020, 4, 10), Days = 30, Count = 12000, Price = 10  },
                new Products{NumWar = 1, Code = 12, Name = "Peach", Date = new DateTime(2020, 4, 12), Days = 30, Count = 14000, Price = 15  }};

            createFile(tab);

            rd = new RepDat(testpath);

            bool exceptionOccured = false;

            try
            {
                rd.Del("Ildar");    
            }
            catch
            {
                exceptionOccured = true;
            }

            if (!exceptionOccured)
            {
                Assert.Fail();
            }
        }

    }
}
