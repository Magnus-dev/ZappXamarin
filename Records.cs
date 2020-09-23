using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Javax.Security.Auth;
using Mono.Data.Sqlite;

namespace ZAPP
{
    class Records
    {
    }
    public class AppointmentRecord
    {
        public int id;
        public string name;
        public string address;
        public string postcode;
        public string city;
        public string appointmentTime;
        public string startTime;
        public string endTime;
        public string _id;



        public AppointmentRecord(JsonValue record)
        {
            this.name = (string)record["Name"];
            this.address = (string)record["Address"];
            this.postcode = (string)record["Postcode"];
            this.city = (string)record["City"];
            this.appointmentTime = (string)record["AppointmentTime"];
            this._id = (string)record["_id"];
            this.startTime = null;
            this.endTime = null;
        }
        public AppointmentRecord(SqliteDataReader record)
        {
            this.id = (int)(Int64)record["id"];
            this.name = (string)record["name"];
            this.address = (string)record["address"];
            this.postcode = (string)record["postcode"];
            this.city = (string)record["city"];
            this.appointmentTime = (string)record["appointmentTime"];
            this._id = (string)record["_id"];
            this.startTime = (string)record["startTime"];
            this.endTime = (string)record["endTime"];
        }
        public string createRecordString()
        {
            string record = "insert into appointment (name, address, postcode, city, appointmentTime, startTime, endTime, _id) " +
                "           Values('" + this.name + "', '" + this.address + "', '" + this.postcode + "', '" + this.city + "', '" + this.appointmentTime + "', '" + this.startTime + "', '" + this.endTime + "', '" + this._id + "');";
            return record;
        }
        public string updateRecordString()
        {
            string record = "update appointment SET name = '" + this.name + "', address= '" + this.address + "', postcode='" + this.postcode + "' , city = '" + this.city + "', appointmentTime='" + this.appointmentTime + "' , startTime ='" + this.startTime + "' , endTime= '" + this.endTime + "' WHERE _id = '" + this._id + "';";
            return record;
        }
        public static string getRecords()
        {
            string record = "select id, name, address, postcode, city, appointmentTime, startTime, endTime, _id from appointment;";
            return record;
        }
        public void SetStartTime(_database db, string now)
        {
            string query = "UPDATE appointment SET startTime = '" +now + "' WHERE _id = '" + this._id + "';";
            Console.WriteLine(query);
            db.writeToTable(query);
            this.startTime = now;

        }
        public void SetEndTime(_database db, string now)
        {
            string query = "UPDATE appointment SET endTime = '" + now + "' WHERE _id = '" + this._id + "';";
            Console.WriteLine(query);
            db.writeToTable(query);
            this.endTime = now;

        }

    }
    public class ToDoesRecord
    {
        public int id;
        public string description;
        public int completed;
        public string appointmentId;
        public string _id;




        public ToDoesRecord(JsonValue record)
        {
            this.description = (string)record["Description"];
            this.completed = 0;
            this._id = (string)record["_id"];

            JsonObject Appointment = (JsonObject)record["Appointment"];
            this.appointmentId = (string)Appointment["_id"];
        }
        public ToDoesRecord(SqliteDataReader record)
        {
            this.id = (int)(Int64)record["id"];
            this.description = (string)record["description"];
            this.completed = (int)record["completed"];
            this._id = (string)record["_id"];
            this.appointmentId = (string)record["appointmentId"];
        }
        public string createRecordString()
        {
            string record = "insert into todoes (description, completed, appointmentId, _id) " +
                "           Values('" + this.description + "', '" + this.completed + "', '" + this.appointmentId + "', '" + this._id + "');";
            return record;
        }
        public string updateRecordString()
        {
            string record = "update todoes SET description = '" + this.description + "', appointmentId= '" + this.appointmentId + "', _id='" + this._id + "' WHERE _id = '" + this._id + "';";
            return record;
        }
        //public string createManyRecordsString(JsonArray records)
        //{

        //    string result = "insert into data (code, description) Values";
        //    foreach (JsonValue record in records)
        //    {
        //        result += "(" + record["code"] + ", " + record["description"] + "), ";
        //    }
        //    result += ";";

        //    return result;
        //}
        public static string getRecords(string _id)
        {
            string record = "select id,description, completed, appointmentId, _id from todoes where appointmentId = '"+ _id +"';";
            return record;
        }
        public static string getRecords()
        {
            string record = "select id,description, completed, appointmentId, _id from todoes;";
            return record;
        }

        public string switchCompleted()
        {
            int status; 
            if(this.completed == 1)
            {
                status = 0;
            }
            else
            {
                status = 1;
            }

            string record = "UPDATE todoes SET completed = "+status +" WHERE _id = '"+this._id+ "';";
            return record;
        }

    }
    public class UserRecord
    {
        public int id;
        public string username;
        public string password;



        public UserRecord(JsonValue record)
        {
            this.username = (string)record["username"];
            this.password = (string)record["password"];

        }
        public UserRecord(SqliteDataReader record)
        {
            this.id = (int)(Int64)record["id"];
            this.username = (string)record["username"];
            this.password = (string)record["password"];
        }
        public string createRecordString()
        {
            string record = "insert into appointment (username, password) " +
                "           Values('" + this.username + "', '" + this.password + "');";
            return record;
        }
        /* public string createManyRecordsString(JsonArray records)
         {

             string result = "insert into data (code, description) Values";
             foreach (JsonValue record in records)
             {
                 result += "(" + record["code"] + ", " + record["description"] + "), ";
             }
             result += ";";

             return result;
         }*/
        public static string getRecords()
        {
            string record = "select id, username, password;";
            return record;
        }


    }

}