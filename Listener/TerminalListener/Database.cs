using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace TerminalListener
{
    class Database
    {
        private MySqlConnection connection = null;

        ~Database()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        public bool init()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode node;

            try
            {
                string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                doc.Load(folder + @"\database.xml");
                node = doc.DocumentElement.SelectSingleNode("/settings/main-mysql-server");

                String address, port, database, username, password;
                address = node.Attributes["address"]?.InnerText.Trim();
                port = node.Attributes["port"]?.InnerText.Trim();
                database = node.Attributes["database"]?.InnerText.Trim();
                username = node.Attributes["username"]?.InnerText.Trim();
                password = node.Attributes["password"]?.InnerText.Trim();
                
                connection = new MySqlConnection(String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode=none", address, port, username, password, database));
                connection.Open();
                Console.WriteLine("Connected to MySQL");

            }
            catch (Exception e)
            {
                Console.WriteLine("\nDatabase init failed:");
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }


        public List<DeviceStruct> Get_Devices()
        {
            List<DeviceStruct> devices = new List<DeviceStruct>();
            try
            {
                string sql = "SELECT * FROM devices";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    devices.Add(new DeviceStruct
                    {
                        name = dataReader["name"].ToString(),
                        address = dataReader["ip"].ToString(),
                        login = dataReader["login"].ToString(),
                        password = dataReader["password"].ToString(),
                        port = dataReader["port"].ToString(),
                    });
                }

                dataReader.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Database = " + e.Message);
            }

            return devices;
        }

       
    }
}
