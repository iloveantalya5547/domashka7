using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Windows.Media.Animation;

namespace Practical7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        BankPayment userPayment = new BankPayment();
        BinaryFormatter binFormat = new BinaryFormatter();
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(BankPayment));
        bool binformat;
        bool xmlformat;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = userPayment;
            btnshow.Click += Btnshow_Click;
            btnclose.Click += Btnclose_Click;
        }

        private void btnsavedata_click(object sender, RoutedEventArgs e)
        {
            if (binformat == true && xmlformat == false)
            {
                using (Stream fStream = new FileStream("UserBankPayment.dat",
          FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, userPayment);
                }
            }
            else if (binformat == false && xmlformat == true)
            {

                using (FileStream fs = new FileStream("UserBankPayment.xml", FileMode.OpenOrCreate))
                {
                    xmlSerializer.Serialize(fs, userPayment);
                }
            }


            this.Close();
        }
        private void btnloaddata_click(object sender, RoutedEventArgs e)
        {

        }

        private void BinChecked(object sender, RoutedEventArgs e)
        {
            binformat = true;
            xmlformat = false;
        }

        private void XmlChecked(object sender, RoutedEventArgs e)
        {
            binformat = false;
            xmlformat = true;
        }
       
        private void Btnclose_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = Resources["CloseMenu"] as Storyboard;
            sb.Begin(LeftMenu);
        }
        private void Btnshow_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = Resources["OpenMenu"] as Storyboard;
            sb.Begin(LeftMenu);
        }

        [Serializable]
        public class BankPayment
        {
            public string Sender { get; set; }
            public string Receiver { get; set; }
            public int Sum { get; set; }
            public string Payment { get; set; }

            public BankPayment() { }
            public BankPayment(string sender, string receiver, int sum, string payment)
            {
                Sender = sender;
                Receiver = receiver;
                Sum = sum;
                Payment = payment;
            }
        }

    }
  
}
