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
    class TaskRecord
    {
        public string Description;
        public int Completed;
        public string _id;
        public string AppointmentId;
        public TaskRecord(ToDoesRecord record)
        {
            this._id = record._id;
            this.Description = record.description;
            this.Completed = record.completed;
            this.AppointmentId = record.appointmentId;

        }
    }
}