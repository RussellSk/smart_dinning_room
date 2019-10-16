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

        public APIClient()
        {
            client.BaseAddress = new Uri("http://shop.podrom.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
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

        public List<LeftMenuItem> LoadLeftMenu()
        {
            List<LeftMenuItem> items = new List<LeftMenuItem>();
            try
            {
                var t = Task.Run(() => this.GET("api/category"));
                t.Wait();

                JObject leftMenuJson = JObject.Parse(t.Result);
                List<JToken> results = leftMenuJson["data"].Children().ToList();
                
                foreach (JToken result in results)
                {
                    LeftMenuItem leftMenuItem = result.ToObject<LeftMenuItem>();
                    items.Add(leftMenuItem);
                }
            } catch (Exception ex)
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
                var t = Task.Run(() => this.GET("api/product"));
                t.Wait();

                JObject leftMenuJson = JObject.Parse(t.Result);
                List<JToken> results = leftMenuJson["data"].Children().ToList();

                foreach (JToken result in results)
                {
                    MenuItems menuItem = result.ToObject<MenuItems>();
                    menuItems.Add(menuItem);
                }

                return menuItems;

            } catch (Exception ex)
            {
                return menuItems;
            }
        }

        public List<MenuItems> getMenuItemsByCategory(string categoryId)
        {
            List<MenuItems> menuItems = new List<MenuItems>();
            try
            {
                var t = Task.Run(() => this.GET("api/product?category_id=" + categoryId));
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
                return menuItems;
            }
        }
    }
}
