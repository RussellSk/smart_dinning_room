using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;
using System.Diagnostics;
using System.IO;


namespace WpfApp1
{
    class Configurations
    {
        private FileIniDataParser parser = new FileIniDataParser();
        public IniData data;

        Configurations() 
        {
            try
            {
                string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                data = parser.ReadFile(folder + @"\configuration.ini");
            } catch (Exception ex)
            {
                Console.WriteLine("Configuration Error: " + ex.Message);
            }
        }

        private static Configurations instance = null;
        public static Configurations Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Configurations();
                }
                return instance;
            }
            set { }
        }


    }
}
