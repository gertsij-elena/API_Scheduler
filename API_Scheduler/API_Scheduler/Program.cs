using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace API_Scheduler
{
    class Program
    {

        private enum Api { Get, Post, Put, Delete };
        static void Main(string[] arg)
        {
            foreach (string t in arg)
            {
                Task<HttpResponseMessage> task = MakeRequest(t);

                task.Wait();

                HttpResponseMessage response = task.Result;
                string responseContent = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine("Argument:{0}", t);
                Console.WriteLine("Response_content:{0}", responseContent);
            }

            Console.ReadLine();
        }
        private static async Task<HttpResponseMessage> MakeRequest(string apiCall)
        {

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://httpbin.org/");

            var responseMessage = new HttpResponseMessage();
            var apiString = "";
            switch (apiCall)
            {
                case "GetItem":
                    {

                        var userId = "5";

                        apiString = "get?show_env=" + userId;

                        responseMessage = await CallAPI(apiString,Api.Get);

                        break;
                    }

                case "PostItem":
                    {
                        apiString = "forms/post";

                        responseMessage = await CallAPI(apiString,Api.Post);

                        break;
                    }

                default:
                    {
                        break;
                    }
            }
            try
            {
               
                if (responseMessage.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return responseMessage;
        }
        static async Task<HttpResponseMessage> CallAPI(string apiString,Api api)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://httpbin.org/");           

            switch (api)
            {
                case Api.Get:
                    {
                        HttpResponseMessage response1 = await client.GetAsync(apiString);
                        return response1;
                    }

                case Api.Post:
                    {
                        FormUrlEncodedContent requestContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("data", "post_data") });
                        HttpResponseMessage response2 = await client.PostAsync(apiString, requestContent);
                        return response2;
                    }

                case Api.Put:
                    {
                        FormUrlEncodedContent requestContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("data", "put_data") });
                        HttpResponseMessage response3 = await client.PutAsync(apiString, requestContent);
                        return response3;
                    }

                case Api.Delete:
                    {
                        HttpResponseMessage response4 = await client.DeleteAsync(apiString);
                        return response4;
                    }

                default:
                    {
                        HttpResponseMessage response5 = await client.GetAsync(apiString);
                        return response5;
                    }
            }
        }      
    }
}

