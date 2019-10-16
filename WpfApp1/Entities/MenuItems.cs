using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Entities
{
    public class MenuItems : INotifyPropertyChanged
    {
        private int quantity;
        public MenuItems() { this.quantity = 1; }
        public MenuItems(int Id, String Name, String Description, int Price, String Photo, int Quantity) 
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
            this.Photo = Photo;
            this.Quantity = Quantity;
        }
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public int Price { get; set; }
        public String Photo { get; set; }
        public int Quantity { 
            get 
            {
                return this.quantity;
            } 
            set 
            {
                if (this.quantity != value)
                {
                    this.quantity = value;
                    this.NotifyPropertyChanged("Quantity");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
