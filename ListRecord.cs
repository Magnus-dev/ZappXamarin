﻿using System;
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
    class ListRecord
    {
        public string id;
        public string name;
        public string address;
        public string postcode;
        public string city;
        public string appointmentTime;
        public string _id;
        public ListRecord(AppointmentRecord record)
        {
            this.id = record.id.ToString();
            this.name = record.name;
            this.address = record.address;
            this.postcode = record.postcode;
            this.city = record.city;
            this.appointmentTime = record.appointmentTime;
            this._id = record._id;
        }
        
    }
}