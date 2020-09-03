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
        public void SetStartTime(Context context, string StartTime)
        {
            _database db = new _database(context);


            string query = "UPDATE todoes SET startTime = " + StartTime + " WHERE _id = '" + this._id + "';";
            Console.WriteLine(query);
            db.writeToTable(query, db.getDatabase());

        }
        public void SetEndTime(Context context, string EndTime)
        {
            _database db = new _database(context);


            string query = "UPDATE todoes SET endTime = " + EndTime + " WHERE _id = '" + this._id + "';";
            Console.WriteLine(query);
            db.writeToTable(query, db.getDatabase());

        }
    }
}