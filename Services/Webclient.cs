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

namespace ZAPP.Services
{
    class Webclient
    {
        
        public static JsonValue DownloadData(string table, _database db)
        {
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string apiKey = db.GetApiKey();
            try
            {
                //Download data from the API from table {table}
                byte[] myDataBuffer = webClient.DownloadData(Constant.EdUrl + table + Constant.ApiTokenString + apiKey);
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
        public void  UploadAppointmentTimes(string _id, _database db)
        {

            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("Content-Type", "application/json");
            try
            {
                Uri url = new Uri(Constant.HomeUrl + Constant.SaveAppointmentUrl + Constant.ApiTokenString);
                AppointmentRecord record = db.getAppointmentFromTable(_id);
                string now = DateTime.Now.ToString();
                //Console.WriteLine(record.startTime.Length);
                if (record.startTime.Length == 0)
                {
                    string content = "{  \"data\" :{ \"_id\":\"" + _id + "\", \"StartTime\": \"" + now + "\"}}";
                    //Console.WriteLine(content);
                    string message = webClient.UploadString(url, "POST", content);
                    Console.WriteLine(url);
                    record.SetStartTime(db, now);
                }
                else
                {
                    if (record.endTime.Length == 0)
                    {
                        string content = "{  \"data\" :{ \"_id\":\"" + _id + "\", \"EndTime\": \"" + now + "\"}}";
                        //Console.WriteLine(content);
                        string message = webClient.UploadString(url, "POST", content);
                        Console.WriteLine(url);
                        record.SetEndTime(db, now);
                    }

                }
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
        public JsonValue LoginRequest(string email, string password, _database db)
        {
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string loginApiKey = "4cc34a901a055d1580f5ef92e99ce6";
            webClient.Headers.Add("Content-Type", "application/json");
            try
            {
                Uri url = new Uri(Constant.EdUrl + Constant.AuthUser + Constant.ApiTokenString + loginApiKey);
                string content = "{ \"user\":\"" + email + "\", \"password\": \"" + password + "\"}";
                //Console.WriteLine(content);
                string message = webClient.UploadString(url, "POST", content);
                Console.WriteLine(url);

                return message;
               
                
                //TextView warning = FindViewById<TextView>(Resource.Id.Warning);
                //warning.Visibility = ViewStates.Invisible;
            }
            catch (WebException)
            {
                //Console.WriteLine("Could not connect to WebAPI. Please check Connection");
                //TextView warning = FindViewById<TextView>(Resource.Id.Warning);
                //warning.Visibility = ViewStates.Visible;
                Console.WriteLine("Could not connect to WebAPI. Please check Connection");
                return null;
            }
            
          
        }
    }
}