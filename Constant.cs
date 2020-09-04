using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ZAPP
{
    static class Constant
    {
        public static string EdUrl = "http://192.168.1.21:8080/api/collections";
        public static string HomeUrl = "http://192.168.178.19:8080/api/collections";
        public static string ApiToken = "011c00c3da03302a6c353ae054176b";
        public static string ApiTokenString = "?token="+ApiToken;
        public static string SaveAppointmentUrl = "/save/ZappAppointment";
        public static string GetAppointmentUrl = "/get/ZappAppointment";
        public static string SaveTasksUrl = "/save/ZappTasks";
        public static string GetTasksUrl = "/get/ZappTasks";
    }
}