using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Security;
using System.Threading.Tasks;

namespace ZAPP
{
    [Activity(Label = "Detail")]
    public class Detail : Activity
    {
        LinearLayout layout;
        ListView listView;
        List<TaskRecord> records;
        ArrayList result;
        View v;
        _database db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            db = new _database(this);
            var _id = Intent.GetStringExtra("ID");
            result = (ArrayList)db.ShowAppointmentTasks(_id);
            records = new List<TaskRecord>();
            foreach (ToDoesRecord value in result)
            {
                Console.WriteLine(value._id);
                TaskRecord row = new TaskRecord(value);
                records.Add(row);
            }
            SetContentView(Resource.Layout.Detail);
            listView = FindViewById<ListView>(Resource.Id.TasksOverview);
            listView.Adapter = new TaskListViewAdapter(this, records);
            listView.ItemClick += OnListItemClick;
            Button RegisterButton = FindViewById<Button>(Resource.Id.registerTime);

            RegisterButton.Click += delegate
            {
                RegisterButtonClicked();
            };
        }
        protected void OnListItemClick(object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
        {
            var t = records[e.Position];
            //var intent = new Intent(this, typeof(Detail));
            //intent.PutExtra("ID", t.AppointmentId);
            t.switchCompleted(this);
            //StartActivityForResult(intent, 0);
            
                
            ListView listView = sender as ListView;
            var itemView = listView.GetChildAt(e.Position - listView.FirstVisiblePosition);

            ImageView taskCheck = itemView.FindViewById<ImageView>(Resource.Id.Completed);
            if(t.Completed == 1)
            {
                taskCheck.Visibility = ViewStates.Visible;
            }
            else
            {
                taskCheck.Visibility = ViewStates.Invisible;
            }
        }
        protected void RegisterButtonClicked()
        {
            var _id = Intent.GetStringExtra("ID");
            AppointmentRecord record = db.getAppointmentFromTable(_id);
            string now = DateTime.Now.ToString();
            //Console.WriteLine(record.startTime.Length);
            if (record.startTime.Length == 0)
            {                
                record.SetStartTime(db, now);
            }
            else
            {
                if (record.endTime.Length == 0)
                {
                    record.SetEndTime(db, now);
                }

            }
            TextView warning = FindViewById<TextView>(Resource.Id.Warning);
            warning.Visibility = ViewStates.Invisible;
            
        }
       
    }
   
}