using System;
using System.Collections;
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
    [Activity(Label = "Detail")]
    public class Detail : Activity
    {
        LinearLayout layout;
        ListView listView;
        List<TaskRecord> records;
        ArrayList result;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _database db = new _database(this);
            var _id = Intent.GetStringExtra("ID");
            result = (ArrayList)db.showAppointmentTasks(_id);
            records = new List<TaskRecord>();
            foreach (ToDoesRecord value in result)
            {
                TaskRecord row = new TaskRecord(value);
                records.Add(row);
            }
            SetContentView(Resource.Layout.Detail);
            listView = FindViewById<ListView>(Resource.Id.TasksOverview);
            listView.Adapter = new TaskListViewAdapter(this, records);
            listView.ItemClick += OnListItemClick;

            

            //SetContentView(Resource.Layout.Detail);
            //layout = FindViewById<LinearLayout>(Resource.Id.Details);
            //layout.FindViewById<TextView>(Resource.Id.TextD1).Text = id;
            //layout.FindViewById<TextView>(Resource.Id.TextD2).Text = code;
            //layout.FindViewById<TextView>(Resource.Id.TextD3).Text = description;
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
        protected void OnListItemClick(object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
        {

        }
    }
   
}