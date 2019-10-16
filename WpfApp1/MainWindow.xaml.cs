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
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading;
using WpfApp1.Entities;
using WpfApp1.Dialogs;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly APIClient api = new APIClient();
        private List<MenuItems> foodMenu = new List<MenuItems>();
        private OrderItems orders = new OrderItems();

        public MainWindow()
        {
            InitializeComponent();

            icLeftMenu.ItemsSource = api.LoadLeftMenu();

            foodMenu = api.getFullMenuItems();
            icMainItemsMenu.ItemsSource = foodMenu;

            orderElement.DataContext = this.orders;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string categoryId = (sender as Button).Tag.ToString();
                this.foodMenu = await Task.Run(() => api.getMenuItemsByCategory(categoryId));
                icMainItemsMenu.ItemsSource = this.foodMenu;
            } catch (Exception ex)
            {
                Console.WriteLine("Error in Button_Click1: " + ex.Message);
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string index = (sender as TextBlock).Tag.ToString();
            int currentMenuIndex = foodMenu.FindIndex(x => x.Id.ToString() == index); 
            int currentQuantity = foodMenu[currentMenuIndex].Quantity;
            if (currentQuantity > 1)
            {
                foodMenu[currentMenuIndex].Quantity = currentQuantity - 1;
            }

        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            string index = (sender as TextBlock).Tag.ToString();
            int currentMenuIndex = foodMenu.FindIndex(x => x.Id.ToString() == index);
            foodMenu[currentMenuIndex].Quantity = foodMenu[currentMenuIndex].Quantity + 1;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string index = (sender as Button).Tag.ToString();
            MenuItems food = foodMenu.Find(x => x.Id.ToString() == index);
            
            //int currentFoodinOrderIndex = orders.orders.FindIndex(x => x.Id == food.Id);
            //if (currentFoodinOrderIndex != -1)
            //{
            //    orders.ChangeQuantity(currentFoodinOrderIndex, food.Quantity);
            //} 
            //else
            //{
                orders.Add(new MenuItems()
                {
                    Id = food.Id,
                    Description = food.Description,
                    Name = food.Name,
                    Photo = food.Photo,
                    Price = food.Price,
                    Quantity = food.Quantity,
                });
           // }
            
            Console.WriteLine(index);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Modal Window");
            OrdersDialog ordersDialog = new OrdersDialog(ref orders);
            ordersDialog.Owner = this;
            ordersDialog.ShowDialog();
        }
    }

}
