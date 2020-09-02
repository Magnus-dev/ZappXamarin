using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public string switchCompleted()
        {
            int status;
            if (this.Completed == 1)
            {
                status = 0;
            }
            else
            {
                status = 1;
            }

            string record = "UPDATE todoes SET completed = " + status + " WHERE _id = '" + this._id + "';";
            return record;
        }
    }
}