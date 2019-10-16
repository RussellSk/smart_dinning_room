using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalListener
{
    class Listener
    {
        private BaseProccess proccess = new BaseProccess();

        public void StartListenDevice(DeviceStruct device)
        {
            proccess.LoginDevice(
                device.address,
                device.port,
                device.login,
                device.password);

            proccess.StartListen();
        }
    }
}
