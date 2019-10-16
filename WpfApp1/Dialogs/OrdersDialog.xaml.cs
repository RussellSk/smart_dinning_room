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

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// Interaction logic for OrdersDialog.xaml
    /// </summary>
    public partial class OrdersDialog : Window
    {
        public OrderItems dialogOrder = new OrderItems();
        public OrdersDialog(ref OrderItems order)
        {
            InitializeComponent();
            this.dialogOrder = order;
            this.icOrdersElements.ItemsSource = this.dialogOrder.Orders;
            this.OrderSumElement.DataContext = this.dialogOrder;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String index = (sender as Button).Tag.ToString();
            MenuItems searchItem = new MenuItems();
            foreach(MenuItems item in this.dialogOrder.Orders)
            {
                if (item.Id.ToString().Equals(index))
                {
                    searchItem = item;
                }
            }
            this.dialogOrder.Remove(this.dialogOrder.Orders.IndexOf(searchItem));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            VerificationDialog verificationDialog = new VerificationDialog(ref this.dialogOrder);
            verificationDialog.Owner = this.Owner;
            verificationDialog.ShowDialog();
            this.DialogResult = true;
        }
    }
}
