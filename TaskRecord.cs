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
        public void switchCompleted(Context context)
        {
            _database db = new _database(context);
            int status;
            if (this.Completed == 1)
            {
                //status = 0;
                this.Completed = 0;
            }
            else
            {
                this.Completed = 1;
            }

            string query = "UPDATE todoes SET completed = " + this.Completed + " WHERE _id = '" + this._id + "';";
            Console.WriteLine(query);
            db.writeToTable(query, db.getDatabase());
            
        }
        
    }
}