using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalListener
{
    public class Listener
    {
        public BaseProccess proccess = new BaseProccess();

        public void StartListenDevice(DeviceStruct device)
        {
            proccess.LoginDevice(
                device.address,
                device.port,
                device.login,
                device.password);

            proccess.StartListen();
        }

        ~Listener()
        {
            Console.WriteLine("Listener Destructor");
        }
    }
}
