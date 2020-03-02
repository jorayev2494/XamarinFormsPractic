﻿using JsonServer.MVVM.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;                   // Get Ipv4
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Linq;

namespace JsonServer.Services.RestServer
{
    public class RestApi
    {

        public static string URL = App.URL + "/api";          // api - prefix

        static private HttpClient GetClient()
        {
            // GetHostName();
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            // client.BaseAddress = new Uri($"{App.URL}/api");
            return client;
        }

        #region Get PC Ip
        private static void GetHostName()
        {
            var hostName = Dns.GetHostName();        // Retrive the Name of HOST
            var s = NetworkInterface.GetAllNetworkInterfaces();

            // Dns.
            List<string> ips = new List<string>();
            // var addresses = Dns.GetHostAddresses("IPv4");
            var myIp = "";  //  Dns.GetHostByName(hostName);

            //foreach (var ip in Dns.GetHostEntry().AddressList)
            //{
            //    //if (ip.AddressFamily == AddressFamily.InterNetwork)
            //    //{
            //    //    myIp = ip.ToString();
            //    //}

            //    ips.Add(ip.ToString());

            //}



            foreach (var ip in NetworkInterface.GetAllNetworkInterfaces())
            {
                //if (ip.AddressFamily == AddressFamily.InterNetwork)
                //{
                //    myIp = ip.ToString();
                //}

                ips.Add(ip.Name);

            }

            var method = GetLocalV4Addresses();



            foreach (var item in method)
            {
                ips.Add($">> {item}");
            }

            App.Current.MainPage.DisplayAlert("Your Ip", $"Ip Server: {myIp}", "Ok");
            // URL = $"{myIp}:8080/api";
        }

        private static IEnumerable<IPAddress> GetLocalV4Addresses()
        {
            return from iface in NetworkInterface.GetAllNetworkInterfaces()
                   where iface.OperationalStatus == OperationalStatus.Up
                   from address in iface.GetIPProperties().UnicastAddresses
                   where address.Address.AddressFamily == AddressFamily.InterNetwork
                   select address.Address;
        }
        #endregion

        /// <summary>
        /// GET
        /// </summary>
        /// <typeparam name="T">Type Model</typeparam>
        /// <param name="url">Server url</param>
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
        /// <typeparam name="T">Type Model</typeparam>
        /// <param name="url">Server url</param>
        /// <param name="model">Model</param>
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
        /// <typeparam name="T">Type Model</typeparam>
        /// <param name="url">Server url</param>
        /// <param name="model">Model</param>
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
        /// <typeparam name="T">Type Model</typeparam>
        /// <param name="url">Server url</param>
        /// <param name="model">Model</param>
        /// <returns></returns>
        public static async Task<Boolean> DELETE<T>(string url, T model) where T : IModel
        {
            HttpClient httpClient = GetClient();
            HttpResponseMessage httpResponse = await httpClient.DeleteAsync(URL + url + "/" + model.Id);

            string content = await httpResponse.Content.ReadAsStringAsync();

            MobileResult mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);

            return mobileResult.Status == 200;
        }

        #region Form Data // data-form
        /// <summary>
        /// POST FORM DATA
        /// </summary>
        /// <typeparam name="T">Type Model</typeparam>
        /// <param name="url">Server url</param>
        /// <param name="formDataContent">MultipartFormDataContent</param>
        /// <returns></returns>
        public static async Task<T> POST_FORM_DATA<T>(string url, MultipartFormDataContent formDataContent)
        {
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage httpResponse = await httpClient.PostAsync(URL + url, formDataContent);

            string content = await httpResponse.Content.ReadAsStringAsync();
            MobileResult mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);

            return JsonConvert.DeserializeObject<T>(mobileResult.Data.ToString());
        }


        /// <summary>
        /// PUT FORM DATA
        /// </summary>
        /// <typeparam name="T">Type Model</typeparam>
        /// <param name="url">Server url</param>
        /// <param name="formDataContent">MultipartFormDataContent</param>
        /// <returns></returns>
        public static async Task<T> PUT_FORM_DATA<T>(string url, T model, MultipartFormDataContent formDataContent) where T : IModel
        {
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage httpResponse = await httpClient.PutAsync(URL + url + "/" + model.Id, formDataContent);

            string content = await httpResponse.Content.ReadAsStringAsync();
            MobileResult mobileResult = JsonConvert.DeserializeObject<MobileResult>(content);

            return JsonConvert.DeserializeObject<T>(mobileResult.Data.ToString());
        }
        #endregion
    }
}
