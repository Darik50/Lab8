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
        string path;
        RepDat repDat;
        public Form1()
        {
            InitializeComponent();
            path = Path.Combine(Environment.CurrentDirectory, "Doc.xml");
            try
            {
                repDat = new RepDat(path);
            }
            catch
            {
                MessageBox.Show("Incorrect data entered!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
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
                var Doc = repDat.Search(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
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
                int a = 0;
                for(int i = 0; i < textBox7.Text.Length; i++)
                {
                    if(textBox7.Text.Substring(i, 1) == " ")
                    {
                        a++;
                    }
                }                
                if (textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && textBox9.Text != "" && textBox10.Text != "" && textBox11.Text != "" && a != textBox7.Text.Length)
                {                    
                    repDat.Add(textBox6.Text, textBox5.Text, textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text);
                    MessageBox.Show("Add successfully!");
                }
                else
                {
                    MessageBox.Show("Incorrect data entered!");
                }
            }
            catch
            {
                MessageBox.Show("Incorrect data entered!!!");
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
                repDat.Del(textBox13.Text);
                MessageBox.Show("Deleted successfully!");
            }
            catch
            {
                MessageBox.Show("Incorrect data entered!");
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            repDat.CreateDefFile();
            path = Path.Combine(Environment.CurrentDirectory, "Doc.xml");
        }      

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
