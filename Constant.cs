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
        public static string EdUrl = "http://192.168.1.21:8080/api";
        public static string HomeUrl = "http://192.168.178.19:8080/api";
        public static string AuthUser = "/cockpit/authUser";
        public static string ApiTokenString = "?token=";
        public static string SaveAppointmentUrl = "/collections/save/ZappAppointment";
        public static string GetAppointmentUrl = "/collections/get/ZappAppointment";
        public static string SaveTasksUrl = "/collections/save/ZappTasks";
        public static string GetTasksUrl = "/collections/get/ZappTasks";
    }
}