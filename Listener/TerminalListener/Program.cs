using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalListener
{
    class Program
    {
        static void Main(string[] args)
        {
            // Получение всех девайсов из БД
            Listener listener = new Listener();


                listener.StartListenDevice(new DeviceStruct()
                {
                    address = "192.168.1.108",
                    port = "37777",
                    login = "admin",
                    password = "qwerty12345678",
                });

            Console.ReadKey();
        }
    }
}
