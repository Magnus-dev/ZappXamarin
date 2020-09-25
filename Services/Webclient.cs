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
        public static string EdUrl = "http://192.168.1.21:8080/api";
        public static string HomeUrl = "http://192.168.178.19:8080/api";
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
                byte[] myDataBuffer = webClient.DownloadData(HomeUrl + table + ApiTokenString + apiKey);
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
        public static void UploadStartTime(string _id, string now)
        {

            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("Content-Type", "application/json");
            try
            {
                Uri url = new Uri(HomeUrl + SaveAppointmentUrl + ApiTokenString);
                    string content = "{  \"data\" :{ \"_id\":\"" + _id + "\", \"StartTime\": \"" + now + "\"}}";
                    //Console.WriteLine(content);
                    string message = webClient.UploadString(url, "POST", content);
                    Console.WriteLine(url);
                //TextView warning = FindViewById<TextView>(Resource.Id.Warning);
                //warning.Visibility = ViewStates.Invisible;
            }
            catch (WebException)
            {
                //Console.WriteLine("Could not connect to WebAPI. Please check Connection");
                //TextView warning = FindViewById<TextView>(Resource.Id.Warning);
                //warning.Visibility = ViewStates.Visible;

            }

        }
        public static void UploadEndTime(string _id, string now)
        {

            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("Content-Type", "application/json");
            try
            {
                Uri url = new Uri(HomeUrl + SaveAppointmentUrl + ApiTokenString);
                string content = "{  \"data\" :{ \"_id\":\"" + _id + "\", \"EndTime\": \"" + now + "\"}}";
                //Console.WriteLine(content);
                string message = webClient.UploadString(url, "POST", content);
                Console.WriteLine(url);
                //TextView warning = FindViewById<TextView>(Resource.Id.Warning);
                //warning.Visibility = ViewStates.Invisible;
            }
            catch (WebException)
            {
                Console.WriteLine("Could not connect to WebAPI. Please check Connection");
                //TextView warning = FindViewById<TextView>(Resource.Id.Warning);
                //warning.Visibility = ViewStates.Visible;

            }

        }
        public static string LoginRequest(string email, string password)
        {
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            
            webClient.Headers.Add("Content-Type", "application/json");
            try
            {
                Uri url = new Uri(HomeUrl + AuthUser + ApiTokenString + loginApiKey);
                string content = "{ \"user\":\"" + email + "\", \"password\": \"" + password + "\"}";
                ////Console.WriteLine(content);
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
                //Console.WriteLine("Could not connect to WebAPI. Please check Connection");
                //TextView warning = FindViewById<TextView>(Resource.Id.Warning);
                //warning.Visibility = ViewStates.Visible;
                HttpWebResponse response = (HttpWebResponse)ex.Response;

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Wrong Email or Password");
                    return "unauthorized";
                }   
                else
                {
                    Console.WriteLine("Could not connect to WebAPI. Please check Connection");
                    return "not_found";
                }
                Console.WriteLine("Could not connect to WebAPI. Please check Connection");

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