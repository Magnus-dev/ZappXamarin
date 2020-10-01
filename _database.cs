using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Collections;
using System.Data;
using Mono.Data.Sqlite;
using System.Json;



using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


using ArrayList = System.Collections.ArrayList;

namespace ZAPP
{
    public class _database
    {
        private Context context;
        public void createDatabase()
        {
            Resources res = this.context.Resources;
            string app_name =
                res.GetString(Resource.String.app_name);
            string app_version =
                res.GetString(Resource.String.app_version);
            string createUserTableData =
                res.GetString(Resource.String.createUserTableData);
            Console.WriteLine(createUserTableData);
            string createAppointmentTableData =
                res.GetString(Resource.String.createAppointmentTableData);
            Console.WriteLine(createAppointmentTableData);
            string createToDoesTableData =
                res.GetString(Resource.String.createToDoesTableData);
            Console.WriteLine(createToDoesTableData);

            string dbname = "_db_" + app_name + "_" + app_version + ".sqlite";
            Console.WriteLine(dbname);
            string documentsPath = System.Environment.GetFolderPath
                                        (System.Environment.SpecialFolder.Personal);
            string pathToDatabase = Path.Combine(documentsPath, dbname);

            if (!File.Exists(pathToDatabase))
            {
                SqliteConnection.CreateFile(pathToDatabase);
                var connectionString = String.Format("Data Source={0} ; Version = 3;", pathToDatabase);
                using (var conn = new SqliteConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = createUserTableData;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = createAppointmentTableData;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = createToDoesTableData;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                
            }
            


        }
        public SqliteConnection getDatabase()
        {
            Resources res = this.context.Resources;
            string app_name =
                res.GetString(Resource.String.app_name);
            string app_version =
                res.GetString(Resource.String.app_version);


            string dbname = "_db_" + app_name + "_" + app_version + ".sqlite";
            Console.WriteLine(dbname);
            string documentsPath = System.Environment.GetFolderPath
                                        (System.Environment.SpecialFolder.Personal);
            string pathToDatabase = Path.Combine(documentsPath, dbname);

            if (File.Exists(pathToDatabase))
            {
                var connectionString = String.Format("Data Source={0} ; Version = 3;", pathToDatabase);
                var conn = new SqliteConnection(connectionString);
                return conn;

            }
            else
            {
                return null;
            }

        }
        public int writeToTable(string command)
        {
            var conn = getDatabase();
            int result;
            if (conn != null)
            {
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        Console.WriteLine(command);
                        cmd.CommandText = command;
                        cmd.CommandType = CommandType.Text;
                        result = cmd.ExecuteNonQuery();
                    }
                    conn.Close();

                    return result;
                }
            }
            else
            {
                return 0;
            }
        }
        public int writeToTable(string command, SqliteConnection conn)
        {
            
            int result;
            if (conn != null)
            {
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        Console.WriteLine(command);
                        cmd.CommandText = command;
                        cmd.CommandType = CommandType.Text;
                        result = cmd.ExecuteNonQuery();
                    }
                    conn.Close();

                    return result;
                }

            }
            else
            {
                return 0;
            }
        }
        public AppointmentRecord getAppointmentFromTable(string _id)
        {
            SqliteDataReader result;
            var conn = getDatabase();
            string command = AppointmentRecord.getRecord(_id);
            if (conn != null)
            {
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        Console.WriteLine(command);
                        cmd.CommandText = command;
                        cmd.CommandType = CommandType.Text;
                        result = cmd.ExecuteReader();
                    }
                    AppointmentRecord record = new AppointmentRecord(result);
                    conn.Close();
                    //Console.WriteLine(result.ToString());
                    return record;
                }
            }
            else
            {
                return null;
            }
        }
        public _database(Context context)
        {
            this.context = context;
            this.createDatabase();
        }
        // Calls the download method and processes this data to the local SQLite DB
        public void ApiProcessing()
        {
            var conn = getDatabase();
            ArrayList appointment_db = this.ShowAllAppointmentData();

            JsonValue valuesAppointment = Services.Webclient.DownloadData(Services.Webclient.GetAppointmentUrl, this.GetApiKey());

            if (valuesAppointment != null)
            {
                foreach (JsonObject result in valuesAppointment)
                {
                    Console.WriteLine(result.ToString());
                    AppointmentRecord record = new AppointmentRecord(result);
                    if (EntryIsinLocalDB(appointment_db, record))
                    {
                        Console.WriteLine(this.writeToTable(record.updateRecordString(), conn));
                    }
                    else
                    {
                        Console.WriteLine(this.writeToTable(record.createRecordString(), conn));
                    }
                }
                JsonValue valuesTasks = Services.Webclient.DownloadData(Services.Webclient.GetTasksUrl, this.GetApiKey());
                ArrayList tasks_db = this.ShowAppointmentTasks();
                //Console.WriteLine(valuesTasks.ToString());
                if (valuesTasks != null)
                {
                    foreach (JsonObject result in valuesTasks)
                    {
                        //Console.WriteLine(result.ToString());
                        ToDoesRecord record = new ToDoesRecord(result);
                        if (EntryIsinLocalDB(tasks_db, record))
                        {
                            Console.WriteLine(this.writeToTable(record.updateRecordString(), conn));
                        }
                        else
                        {
                            Console.WriteLine(this.writeToTable(record.createRecordString(), conn));
                        }
                    }
                }
            }
        }
        public ArrayList ShowAllAppointmentData()
        {
            ArrayList list = new ArrayList();
            var conn = this.getDatabase();
            string command = AppointmentRecord.getRecords();
            if (conn != null)
            {
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        Console.WriteLine(command);
                        cmd.CommandText = command;
                        cmd.CommandType = CommandType.Text;
                        SqliteDataReader datatable = cmd.ExecuteReader();
                        while (datatable.Read())
                        {
                            list.Add(new AppointmentRecord(datatable));
                        }
                    }
                    conn.Close();
                }
            }
            return list;
        }
        public ArrayList ShowAppointmentTasks(string _id)
        {
            ArrayList list = new ArrayList();
            var conn = this.getDatabase();
            string command = ToDoesRecord.getRecords(_id);
            if (conn != null)
            {
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        Console.WriteLine(command);
                        cmd.CommandText = command;
                        cmd.CommandType = CommandType.Text;
                        SqliteDataReader datatable = cmd.ExecuteReader();
                        while (datatable.Read())
                        {
                            list.Add(new ToDoesRecord(datatable));
                        }
                    }
                    conn.Close();
                }
            }
            return list;
        }
        public ArrayList ShowAppointmentTasks()
        {
            ArrayList list = new ArrayList();
            var conn = this.getDatabase();
            string command = ToDoesRecord.getRecords();
            if (conn != null)
            {
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        Console.WriteLine(command);
                        cmd.CommandText = command;
                        cmd.CommandType = CommandType.Text;
                        SqliteDataReader datatable = cmd.ExecuteReader();
                        while (datatable.Read())
                        {
                            list.Add(new ToDoesRecord(datatable));
                        }
                    }
                    conn.Close();
                }
            }
            return list;
        }
        public bool EntryIsinLocalDB(ArrayList list, AppointmentRecord record)
        {
            foreach(AppointmentRecord listrec in list)
            {
                if(record._id == listrec._id)
                {
                    return true;
                }
            }
            return false;
        }
        public bool EntryIsinLocalDB(ArrayList list, ToDoesRecord record)
        {
            foreach (ToDoesRecord listrec in list)
            {
                if (record._id == listrec._id)
                {
                    return true;
                }
            }
            return false;
        }
        //    public ArrayList showAllTaskData()
        //    {
        //        ArrayList list = new ArrayList();
        //        var conn = this.getDatabase();
        //        string command = ToDoesRecord.getRecords();
        //        if (conn != null)
        //        {
        //            using (conn)
        //            {
        //                conn.Open();
        //                using (var cmd = conn.CreateCommand())
        //                {
        //                    Console.WriteLine(command);
        //                    cmd.CommandText = command;
        //                    cmd.CommandType = CommandType.Text;
        //                    SqliteDataReader datatable = cmd.ExecuteReader();
        //                    while (datatable.Read())
        //                    {
        //                        list.Add(new AppointmentRecord(datatable));
        //                    }
        //                    foreach (AppointmentRecord row in list)
        //                    {
        //                        Console.WriteLine(row.id);
        //                    }

        //                }
        //                conn.Close();
        //                //Console.WriteLine(result.ToString());
        //                //return result;
        //            }
        //        }
        //        return list;
        //        // SqliteDataReader datatable = this.getFromTheTable(command, conn);


        //        //return list;
        //    }
        public string GetApiKey()
        {
            string apiKey = "";
            var conn = this.getDatabase();
            string command = "select apiKey from user where id = 1;"; ;
            try
            {
                if (conn != null)
                {
                    using (conn)
                    {
                        conn.Open();
                        using (var cmd = conn.CreateCommand())
                        {
                            Console.WriteLine(command);
                            cmd.CommandText = command;
                            cmd.CommandType = CommandType.Text;
                            SqliteDataReader datatable = cmd.ExecuteReader();
                            datatable.Read();
                            if (datatable["apiKey"] is System.DBNull)
                            {
                                apiKey = null;
                            }
                            else
                            {
                                apiKey = datatable["apiKey"].ToString();
                                Console.WriteLine(datatable["apiKey"].GetType());
                            }
                        }
                        conn.Close();
                    }
                }
            }
            catch (SqliteException)
            {
                Console.WriteLine("Entry Could not be found");
            }
            return apiKey;
        }
        public int SetApiKey(string apiKey)
        {

            var conn = getDatabase();
            int result;
            string command = "insert into user (apiKey) Values('"+apiKey+"');" ;
            if (conn != null)
            {
                using (conn)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        Console.WriteLine(command);
                        cmd.CommandText = command;
                        cmd.CommandType = CommandType.Text;
                        result = cmd.ExecuteNonQuery();
                    }
                    conn.Close();

                    return result;
                }
            }
            else
            {
                return 0;
            }

        }
    }
}