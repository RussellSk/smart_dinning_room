using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Entities;

namespace WpfApp1
{
    class APIClient
    {
        private readonly HttpClient client = new HttpClient();
        private readonly Configurations configurations = Configurations.Instance;

        public APIClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(10);
        }

        public async Task<String> GET(String url)
        {
            string responseBody = "";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            } catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<String> POST(String url, String data)
        {
            try
            {
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                String respone = await result.Content.ReadAsStringAsync();
                return respone;
            } catch (Exception)
            {
                return "";
            }
        }

        public List<LeftMenuItem> LoadLeftMenu()
        {
            List <LeftMenuItem> items = new List<LeftMenuItem>();
            try
            {
                var t = Task.Run(() => this.GET(configurations.data["API"]["kitchen"] + "api/category"));
                t.Wait();

                JObject leftMenuJson = JObject.Parse(t.Result);
                List<JToken> results = leftMenuJson["data"].Children().ToList();
                
                foreach (JToken result in results)
                {
                    LeftMenuItem leftMenuItem = result.ToObject<LeftMenuItem>();
                    items.Add(leftMenuItem);
                }
            } catch (Exception)
            {
                return items;
            }

            return items;
        }

        public List<MenuItems> getFullMenuItems()
        {
            List<MenuItems> menuItems = new List<MenuItems>();
            try
            {
                var t = Task.Run(() => this.GET(configurations.data["API"]["kitchen"] + "api/product"));
                t.Wait();

                JObject leftMenuJson = JObject.Parse(t.Result);
                List<JToken> results = leftMenuJson["data"].Children().ToList();

                foreach (JToken result in results)
                {
                    MenuItems menuItem = result.ToObject<MenuItems>();
                    menuItems.Add(menuItem);
                }

                return menuItems;

            } catch (Exception)
            {
                return menuItems;
            }
        }

        public List<MenuItems> getMenuItemsByCategory(string categoryId)
        {
            List<MenuItems> menuItems = new List<MenuItems>();
            try
            {
                var t = Task.Run(() => this.GET(configurations.data["API"]["kitchen"] + "api/product?category_id=" + categoryId));
                t.Wait();

                JObject leftMenuJson = JObject.Parse(t.Result);
                List<JToken> results = leftMenuJson["data"].Children().ToList();

                foreach (JToken result in results)
                {
                    MenuItems menuItem = result.ToObject<MenuItems>();
                    menuItems.Add(menuItem);
                }

                return menuItems;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return menuItems;
            }
        }

        public VerificationResponse verificationPost(OrderItems orderItems)
        {
            String jsonOrder = JsonConvert.SerializeObject(orderItems);
            Console.WriteLine(jsonOrder);

            VerificationResponse response = new VerificationResponse();
            try
            {
                var t = Task.Run(() => this.POST(configurations.data["API"]["backend"] + "api/order", jsonOrder));
                t.Wait();
                response = JsonConvert.DeserializeObject<VerificationResponse>(t.Result);
                return response;
            } catch (Exception)
            {
                return response;
            }
        }
    }
}
