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
            //Database database = new Database();
            //database.init();
            //List<DeviceStruct> devices = database.Get_Devices();
            List<DeviceStruct> devices = new List<DeviceStruct>();
            devices.Add(new DeviceStruct()
            {
                address = "192.168.1.108",
                port = "37777",
                login = "admin",
                password = "qwerty12345678",
                name = "device",
            });

            List<Listener> listeners = new List<Listener>();
            
            // Создание объектов прослушивателей устройств
            for (int i = 0; i < devices.Count; i++)
            {
                Console.WriteLine("ip = " + devices[i].address + " port = " + devices[i].port);
                Listener listener = new Listener();
                listeners.Add(listener);
            }

            // Начало прослушивания
            int count = 0;
            foreach (DeviceStruct device in devices)
            {
                listeners[count].StartListenDevice(new DeviceStruct()
                {
                    address = device.address,
                    port = device.port,
                    login = device.login,
                    password = device.password,
                });
                count++;
            }

            Console.ReadKey();
        }
    }
}
