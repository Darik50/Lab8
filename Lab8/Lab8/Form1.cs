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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    int ErrNumWar = Convert.ToInt32(textBox1.Text);
                }
                if (textBox2.Text != "")
                {
                    int ErrCode = Convert.ToInt32(textBox2.Text);
                }
                if (textBox3.Text != "")
                {
                    DateTime ErrDate = Convert.ToDateTime(textBox3.Text);
                }
                if (textBox4.Text != "")
                {
                    int ErrDays = Convert.ToInt32(textBox4.Text);
                }
                int Dobl = 0;
                label15.Text = "NumWar";
                label16.Text = "Code";
                label17.Text = "Name";
                label18.Text = "Date";
                label19.Text = "Days";
                label20.Text = "Count";
                label21.Text = "Price";
                label23.Text = "NumWar";
                label24.Text = "Code";
                label25.Text = "Name";
                label26.Text = "Date";
                label27.Text = "Days";
                label28.Text = "Count";
                label22.Text = "Price";
                var Doc = from products in XDocument.Load(Path.Combine(Environment.CurrentDirectory, "Doc.xml")).Descendants("Product")
                          where (products.Element("NumWar").Value == textBox1.Text || textBox1.Text == "")
                          && (products.Attribute("Code").Value == textBox2.Text || textBox2.Text == "")
                          && (products.Element("Date").Value == textBox3.Text || textBox3.Text == "")
                          && (products.Element("Days").Value == textBox4.Text || textBox4.Text == "")
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
                foreach (var i in Doc)
                {
                    if (((DateTime)i.Date).AddDays((int)i.Days) >= DateTime.Today)
                    {
                        label15.Text = label15.Text + "\n" + (i.NumWar.ToString());
                        label16.Text = label16.Text + "\n" + (i.Code.ToString());
                        label17.Text = label17.Text + "\n" + (i.Name.ToString());
                        label18.Text = label18.Text + "\n" + (i.Date.ToString("yyyy.MM.dd"));
                        label19.Text = label19.Text + "\n" + (i.Days.ToString());
                        label20.Text = label20.Text + "\n" + (i.Count.ToString());
                        label21.Text = label21.Text + "\n" + (i.Price.ToString());
                    }
                    else
                    {
                        label23.Text = label23.Text + "\n" + (i.NumWar.ToString());
                        label24.Text = label24.Text + "\n" + (i.Code.ToString());
                        label25.Text = label25.Text + "\n" + (i.Name.ToString());
                        label26.Text = label26.Text + "\n" + (i.Date.ToString("yyyy.MM.dd"));
                        label27.Text = label27.Text + "\n" + (i.Days.ToString());
                        label28.Text = label28.Text + "\n" + (i.Count.ToString());
                        label22.Text = label22.Text + "\n" + (i.Price.ToString());
                    }
                    Dobl++;
                }
                if(Dobl == 0)
                {
                    MessageBox.Show("Data not found!");
                }
            }
            catch
            {
                MessageBox.Show("Incorrect data entered!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int ErrNumWar = Convert.ToInt32(textBox5.Text);
                int ErrCode = Convert.ToInt32(textBox6.Text);
                string ErrName = textBox7.Text;
                DateTime ErrDate = Convert.ToDateTime(textBox8.Text);
                int ErrDays = Convert.ToInt32(textBox9.Text);
                int ErrCount = Convert.ToInt32(textBox10.Text);
                int ErrPrice = Convert.ToInt32(textBox11.Text);
                int a = 0;
                for(int i = 0; i < textBox7.Text.Length; i++)
                {
                    if(textBox7.Text.Substring(i, 1) == " ")
                    {
                        a++;
                    }
                }
                int b = 0;
                var Doc1 = from products in XDocument.Load(Path.Combine(Environment.CurrentDirectory, "Doc.xml")).Descendants("Product")
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
                    if(i.Code.ToString() == textBox6.Text)
                    {
                        b++;
                    }
                }
                if (textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "" && textBox10.Text != "" && textBox11.Text != "" && a != textBox7.Text.Length && b == 0)
                {
                    var Doc = XDocument.Load(Path.Combine(Environment.CurrentDirectory, "Doc.xml"));
                    Doc.Root.Add(new XElement("Product",
                            new XAttribute("Code", textBox6.Text),
                            new XElement("NumWar", textBox5.Text),
                            new XElement("Name", textBox7.Text),
                            new XElement("Date", textBox8.Text),
                            new XElement("Days", textBox9.Text),
                            new XElement("Count", textBox10.Text),
                            new XElement("Price", textBox11.Text)));
                    Doc.Save(Path.Combine(Environment.CurrentDirectory, "Doc.xml"));
                }
                else
                {
                    MessageBox.Show("Incorrect data entered!");
                }
            }
            catch
            {
                MessageBox.Show("Incorrect data entered!");
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int ErrCode = Convert.ToInt32(textBox13.Text);
                var Doc = XDocument.Load(Path.Combine(Environment.CurrentDirectory, "Doc.xml"));
                Doc.Element("Products").Elements("Product").Where(x =>
                x.Attribute("Code").Value == textBox13.Text).FirstOrDefault().Remove();
                Doc.Save(Path.Combine(Environment.CurrentDirectory, "Doc.xml"));
            }
            catch
            {
                MessageBox.Show("Incorrect data entered!");
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
