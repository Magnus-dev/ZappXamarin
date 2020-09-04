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
                this.ApiProcessing();
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
            string command = "select id, name, address, postcode, city, appointmentTime, startTime, endTime, _id from appointment where _id = '" + _id + "';";
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
        private JsonValue downloadData(string table)
        {
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            try
            {
                //Download data from the API from table {table}
                byte[] myDataBuffer = webClient.DownloadData(Constant.HomeUrl +table+ Constant.ApiTokenString);
                string download = Encoding.ASCII.GetString(myDataBuffer);
                JsonValue value = JsonValue.Parse(download);
                JsonValue values = value["entries"];
                return values;
            }
            catch (WebException)
            {
                return null;
                //Doe vooralsnog niks, straks wellicht een boolean terug.
                // geven of e.e.a. gelukt is of niet
            }
        }
        private void ApiProcessing()
        {
            var conn = getDatabase();
            JsonValue valuesAppointment = downloadData(Constant.GetAppointmentUrl);
            foreach (JsonObject result in valuesAppointment)
            {
                //Console.WriteLine(result.ToString());
                AppointmentRecord record = new AppointmentRecord(result);
                Console.WriteLine(this.writeToTable(record.createRecordString(), conn));
            }
            JsonValue valuesTasks = downloadData(Constant.GetTasksUrl);
            //Console.WriteLine(valuesTasks.ToString());
            foreach (JsonObject result in valuesTasks)
            {
                //Console.WriteLine(result.ToString());
                ToDoesRecord record = new ToDoesRecord(result);
                Console.WriteLine(this.writeToTable(record.createRecordString(), conn));
            }

        }
        public ArrayList showAllData()
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
        public ArrayList showAppointmentTasks(string _id)
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

    }
}