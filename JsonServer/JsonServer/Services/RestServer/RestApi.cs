using JsonServer.MVVM.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JsonServer.Services.RestServer
{
    public class RestApi
    {

        public const string URL = "http://192.168.1.108:8080/api";

        static private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        static public async Task<IEnumerable<T>> GET<T>(string url)
        {
            HttpClient httpClient = GetClient();
            string httpResponseString = await httpClient.GetStringAsync(URL + url);
            MobileResult mobileResult = JsonConvert.DeserializeObject<MobileResult>(httpResponseString);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(mobileResult.Data.ToString());
        }

        /// <summary>
        /// POST
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<T> POST<T>(string url, T model)
        {
            HttpClient httpClient = GetClient();

            HttpResponseMessage httpResponse = await httpClient.PostAsync(URL + url,
                new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8,
                "application/json")
            );

            string content = await httpResponse.Content.ReadAsStringAsync();
            MobileResult mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);

            // if (mobileResult.Status == 200 && mobileResult.Message == "success")
            return JsonConvert.DeserializeObject<T>(mobileResult.Data.ToString());
        }

        /// <summary>
        /// PUT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<T> PUT<T>(string url, T model) where T : IModel
        {
            HttpClient httpClient = GetClient();

            HttpResponseMessage httpResponse = await httpClient.PutAsync(URL + url + "/" + model.Id,
                new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8,
                "application/json")
            );

            string content = await httpResponse.Content.ReadAsStringAsync();
            MobileResult mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);

            // if (mobileResult.Status == 200 && mobileResult.Message == "success")
            return JsonConvert.DeserializeObject<T>(mobileResult.Data.ToString());
        }

        /// <summary>
        /// DELETE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static async Task<Boolean> DELETE<T>(string url, T model) where T : IModel
        {
            HttpClient httpClient = GetClient();
            HttpResponseMessage httpResponse = await httpClient.DeleteAsync(URL + url + "/" + model.Id);

            string content = await httpResponse.Content.ReadAsStringAsync();

            MobileResult mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);

            return mobileResult.Status == 200;
        }

    }
}
