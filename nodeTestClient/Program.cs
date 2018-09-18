using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace nodeTestClient
{
    public class Program
    {
        private static readonly HttpClient client = new HttpClient();
        public static void Main(string[] args)
        {
            Test();
        }

        public static async void Test()
        {
            var request = new req { id = 0, token = 15 };

            string test = new JavaScriptSerializer().Serialize(request);

            var content = new StringContent(test, Encoding.UTF8, "application/json");
            
            var response = client.PostAsync(@"http://localhost:8081/test3", content).Result;
            

            var responseString = response.Content.ReadAsStringAsync().Result;


            res temp = new JavaScriptSerializer().Deserialize<res>(responseString);

        }



    }

    public struct req
    {
        public int id;
        public int token;
    }

    public struct res
    {
        public string status;
        public string message;
    }
}
