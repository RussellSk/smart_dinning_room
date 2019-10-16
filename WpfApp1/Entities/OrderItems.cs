using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Entities
{
    public class OrderItems : INotifyPropertyChanged
    {
        public ObservableCollection<MenuItems> orders = new ObservableCollection<MenuItems>();
        private int order_amount = 0;

        public OrderItems(ObservableCollection<MenuItems> orders)
        {
            this.orders = orders;
        }

        public OrderItems() { }
        public ObservableCollection<MenuItems> Orders
        {
            get
            {
                return this.orders;
            }
            set
            {
                this.orders = value;
                this.NotifyPropertyChanged("Orders");
            }
        }

        public int OrderAmount
        {
            get
            {
                return this.order_amount;
            }
            set { }
        }

        public int OrderSum
        {
            get
            {
                int sum = 0;
                foreach(MenuItems item in this.orders)
                {
                    sum += item.Quantity * item.Price;
                }

                return sum;
            }
            set { }
        }

        public void ChangeQuantity(int index, int quantity)
        {
            this.order_amount += quantity;
            orders[index].Quantity += quantity;
            this.NotifyPropertyChanged("OrderAmount");
            this.NotifyPropertyChanged("OrderSum");
        }

        public void Add(MenuItems menuItems)
        {
            this.order_amount += menuItems.Quantity;
            orders.Add(menuItems);
            this.NotifyPropertyChanged("OrderAmount");
            this.NotifyPropertyChanged("OrderSum");
            this.NotifyPropertyChanged("Orders");
        }

        public void Remove(int index)
        {
            if (orders.Count() > 0)
            {
                this.order_amount -= orders[index].Quantity;
                orders.RemoveAt(index);
                this.NotifyPropertyChanged("OrderAmount");
                this.NotifyPropertyChanged("OrderSum");
                this.NotifyPropertyChanged("Orders");
            }  
        }

        public void Clear()
        {
            this.orders.Clear();
            this.order_amount = 0;
            this.NotifyPropertyChanged("OrderAmount");
            this.NotifyPropertyChanged("OrderSum");
            this.NotifyPropertyChanged("Orders");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
