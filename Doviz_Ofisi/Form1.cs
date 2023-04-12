using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Doviz_Ofisi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.Add("Dolar Alış");
            comboBox1.Items.Add("Dolar Satış");
            comboBox1.Items.Add("Euro Alış");
            comboBox1.Items.Add("Euro Satış");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string bugun = "https://www.tcmb.gov.tr/kurlar/today.xml";
            var xmldosya = new XmlDocument();
            xmldosya.Load(bugun);
            DateTime date = Convert.ToDateTime(xmldosya.SelectSingleNode("Tarih_Date").Attributes["Tarih"].Value);
            label3.Text = "Son Güncellenme: " + date.ToString();

            string dolarAlis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            lblDolarAl.Text = dolarAlis;

            string dolarSatis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
            lblDolarSat.Text = dolarSatis;

            string euroAlis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
            lblEuroAl.Text = euroAlis;

            string euroSatis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
            lblEuroSat.Text = euroSatis;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex==0)
            {
                textBox1.Text = lblDolarAl.Text;
            }

            if (comboBox1.SelectedIndex == 1)
            {
                textBox1.Text = lblDolarSat.Text;
            }

            if (comboBox1.SelectedIndex == 2)
            {
                textBox1.Text = lblEuroAl.Text;
            }

            if (comboBox1.SelectedIndex == 3)
            {
                textBox1.Text = lblEuroSat.Text;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double kur, miktar, tutar;
            kur = Convert.ToDouble(textBox1.Text);
            miktar = Convert.ToDouble(textBox2.Text);
            tutar = kur * miktar;
            textBox3.Text = tutar.ToString();
            textBox4.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace(".", ",");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double kur = Convert.ToDouble(textBox1.Text);
            int miktar = Convert.ToInt32(textBox2.Text);
            int tutar = Convert.ToInt32(miktar / kur);
            textBox3.Text = tutar.ToString();
            double kalan;
            kalan = miktar % kur;
            textBox4.Text = kalan.ToString();

        }
    }
}
