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
using System.Windows.Shapes;
using WpfApp1.Entities;
using System.Drawing;
using System.Drawing.Printing;

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// Interaction logic for VerificationDialog.xaml
    /// </summary>
    public partial class VerificationDialog : Window
    {
        private OrderItems verificationOrder = new OrderItems();
        public VerificationDialog(ref OrderItems order)
        {
            InitializeComponent();
            this.verificationOrder = order;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.verificationOrder.Clear();
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CustomPrinter printer = new CustomPrinter();
            printer.AddLine("     SmartShcool", new Font("Courier New", 14, System.Drawing.FontStyle.Bold));
            printer.AddLine("       Столовая", new Font("Courier New", 14, System.Drawing.FontStyle.Bold));
            printer.AddBlankLine();      
            printer.AddSeparateLine("Номер заказа:", "222", new Font("Courier New", 10, System.Drawing.FontStyle.Bold));
            printer.AddSeparateLine("id клиента: ", "Smith", new Font("Courier New", 10, System.Drawing.FontStyle.Bold));
            printer.AddBlankLine();
            foreach (MenuItems item in verificationOrder.Orders)
            {
                printer.AddLine(item.Name, new Font("Courier New", 10, System.Drawing.FontStyle.Bold));
                printer.AddLineEnd(" =" + item.Price.ToString() + " тг.", new Font("Courier New", 10, System.Drawing.FontStyle.Bold));
                printer.AddSeparateLine("x" + item.Quantity, " =" + (item.Price * item.Quantity).ToString() + " тг.", new Font("Courier New", 10, System.Drawing.FontStyle.Bold));
            }

            printer.AddBlankLine();
            printer.AddCenteredLine("СПАСИБО ЗА ЗАКАЗ!", new Font("Courier New", 10, System.Drawing.FontStyle.Bold));
            printer.AddBlankLine();
            printer.Print();

            this.verificationOrder.Clear();
            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
