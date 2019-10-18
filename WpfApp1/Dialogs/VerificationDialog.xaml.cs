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
using TerminalListener;
using System.Threading;

namespace WpfApp1.Dialogs
{
    /// <summary>
    /// Interaction logic for VerificationDialog.xaml
    /// </summary>
    public partial class VerificationDialog : Window
    {
        private OrderItems verificationOrder = new OrderItems();
        private BaseProccess listener = new BaseProccess();
        private VerificationResponse response = new VerificationResponse();
        private readonly Configurations configurations = Configurations.Instance;

        public VerificationDialog(ref OrderItems order)
        {
            InitializeComponent();
            this.verificationOrder = order;
            this.OrderIdText.Visibility = Visibility.Hidden;
            this.PersonNameText.Visibility = Visibility.Hidden;
            this.TakeChequeText.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.verificationOrder.Clear();
            this.listener.StopListen();
            this.DialogResult = true;
        }

        private void PrintCheque()
        {
            CustomPrinter printer = new CustomPrinter();
            printer.AddLine("     SmartSchool", new Font("Courier New", 14, System.Drawing.FontStyle.Bold));
            printer.AddLine("       Столовая", new Font("Courier New", 14, System.Drawing.FontStyle.Bold));
            printer.AddBlankLine();      
            printer.AddSeparateLine("Номер заказа:", response.Id, new Font("Courier New", 10, System.Drawing.FontStyle.Bold));
            printer.AddSeparateLine("ФИО:", response.Name, new Font("Courier New", 10, System.Drawing.FontStyle.Bold));
            printer.AddSeparateLine("Школа:", response.School, new Font("Courier New", 10, System.Drawing.FontStyle.Bold));
            printer.AddSeparateLine("Класс:", response.Klass, new Font("Courier New", 10, System.Drawing.FontStyle.Bold));
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

            this.Dispatcher.Invoke(new Action(() => this.verificationOrder.Clear()));
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Start FaceID Listening");
            listener.LoginDevice(
                configurations.data["FaceID"]["ip"],
                configurations.data["FaceID"]["port"],
                configurations.data["FaceID"]["user"],
                configurations.data["FaceID"]["password"]
            );

            listener.StartListen();

            Thread t = new Thread(new ThreadStart(this.FaceID_Worker));
            t.Start();
            Console.WriteLine("End Window_Loader");
        }

        private void FaceID_Worker()
        { 
            for (int i = 0; i <= 15; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Tick");
                if (listener.nedded_user_id != null && listener.nedded_user_id != "0") 
                {                    
                    break;
                }
            }
            
            listener.StopListen();

            if (listener.nedded_user_id != null) {
                try
                {
                    verificationOrder.UserId = Int32.Parse(listener.nedded_user_id);
                }
                catch (Exception)
                {
                    listener.StartListen();
                    Thread t = new Thread(new ThreadStart(this.FaceID_Worker));
                    t.Start();

                    return;
                }

                APIClient client = new APIClient();
                response = client.verificationPost(verificationOrder);
                this.Dispatcher.Invoke(new Action(() => this.AuthorizationText.Visibility = Visibility.Hidden ));

                if (response.Status == "True")
                {
                    this.Dispatcher.Invoke(new Action(() => {
                        this.PersonNameText.Text = "Приятного аппетита " + response.Name + "!";
                        this.PersonNameText.Visibility = Visibility.Visible;

                        this.OrderIdText.Text = "Номер заказа: " + response.Id;
                        this.OrderIdText.Visibility = Visibility.Visible;
                        this.TakeChequeText.Visibility = Visibility.Visible;
                    }));
                    PrintCheque();
                } 
                else
                {
                    this.Dispatcher.Invoke(new Action(() => {
                        this.OrderIdText.Text = response.Message;
                        this.OrderIdText.Visibility = Visibility.Visible;
                    }));
                }
            }

            Console.WriteLine("USER ID = " + listener.nedded_user_id);
        }
    }
}
