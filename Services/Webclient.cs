using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Security;

namespace ZAPP.Services
{
    static class Webclient
    {
        //EDUCOM URL
        //public static string Url = "http://192.168.1.21:8080/api";
        // HOME URL
        public static string Url = "http://192.168.178.19:8080/api";
        public static string AuthUser = "/cockpit/authUser";
        public static string ApiTokenString = "?token=";
        public static string loginApiKey = "4cc34a901a055d1580f5ef92e99ce6";
        public static string SaveAppointmentUrl = "/collections/save/ZappAppointment";
        public static string GetAppointmentUrl = "/collections/get/ZappAppointment";
        public static string SaveTasksUrl = "/collections/save/ZappTasks";
        public static string GetTasksUrl = "/collections/get/ZappTasks";
        public static JsonValue DownloadData(string table, string apiKey)
        {
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            try
            {
                Console.WriteLine(apiKey);
                //Download data from the API from table {table}
                byte[] myDataBuffer = webClient.DownloadData(Url + table + ApiTokenString + apiKey);
                string download = Encoding.ASCII.GetString(myDataBuffer);
                JsonValue value = JsonValue.Parse(download);
                JsonValue values = value["entries"];
                Console.WriteLine(values.ToString());
                return values;
            }
            catch (WebException)
            {
                Console.WriteLine("Could not connect to WebAPI. Please check Connection");
                return null;

                //Doe vooralsnog niks, straks wellicht een boolean terug.
                // geven of e.e.a. gelukt is of niet
            }
        }
        public static void UploadStartTime(string _id, string now, string apiKey)
        {

            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("Content-Type", "application/json");
            try
            {
                Uri url = new Uri(Url + SaveAppointmentUrl + ApiTokenString + apiKey);
                    string content = "{  \"data\" :{ \"_id\":\"" + _id + "\", \"StartTime\": \"" + now + "\"}}";
                    webClient.UploadStringAsync(url, "POST", content);
            }
            catch (WebException)
            {

            }

        }
        public static void UploadEndTime(string _id, string now, string apiKey)
        {

            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("Content-Type", "application/json");
            try
            {
                Uri url = new Uri(Url + SaveAppointmentUrl + ApiTokenString+apiKey);
                string content = "{  \"data\" :{ \"_id\":\"" + _id + "\", \"EndTime\": \"" + now + "\"}}";
                //Console.WriteLine(content);
                webClient.UploadStringAsync(url, "POST", content);
            }
            catch (WebException)
            {
                Console.WriteLine("Could not connect to WebAPI. Please check Connection");
            }

        }
        public static string LoginRequest(string email, string password)
        {
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("Content-Type", "application/json");
            try
            {
                Uri url = new Uri(Url + AuthUser + ApiTokenString + loginApiKey);
                string content = "{ \"user\":\"" + email + "\", \"password\": \"" + password + "\"}";
                string message = webClient.UploadString(url, "POST", content);
                Console.WriteLine(message);

                JsonValue value = JsonValue.Parse(message);
                string apiKey = value["api_key"];
                Console.WriteLine(apiKey);
                
                Console.WriteLine("STOP");
                return apiKey;

            }
            catch (WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Wrong Email or Password");
                    return null;
                }   
                else
                {
                    Console.WriteLine("Could not connect to WebAPI. Please check Connection");
                    return null;
                }
                //Console.WriteLine("Could not connect to WebAPI. Please check Connection");

            }
        }
        //public static void UploadStringCallbackLogin(Object sender, UploadStringCompletedEventArgs e)
        //{   
        //    if(e.Error == null) { 
        //        string result = e.Result;
        //        JsonValue value = JsonValue.Parse(result);
        //        string apiKey = value["api_key"];
        //        Console.WriteLine(apiKey);
        //        //string reply = e.Result.ToString();
        //        //Console.WriteLine(reply);
            
        //        Console.WriteLine("STOP");
        //    }
        //    else
        //    {
        //        //StartActivity(typeof(LoginActivity));
                
        //    }
        //    //return apiKey;
        //}
    }
}