using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text.Style;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ZAPP.Fragments
{
    public class AppointmentTasksFragment : Android.Support.V4.App.Fragment
    {
        _database db;
        ArrayList result;
        List<TaskRecord> records;
        string _id;
        ListView listView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.AppointmentTasks, container, false);
            _id = Arguments.GetString("ID");
            Console.WriteLine(_id);
            //db = new _database(Activity);
            db = new _database(Activity);

            result = (ArrayList)db.ShowAppointmentTasks(_id);
            records = new List<TaskRecord>();
            foreach (ToDoesRecord value in result)
            {

                Console.WriteLine(value._id);
                TaskRecord row = new TaskRecord(value);
                records.Add(row);
            }
            //ListAdapter = new TaskListViewAdapter(Activity, records);

            listView = view.FindViewById<ListView>(Resource.Id.TasksOverview);
            listView.Adapter = new TaskListViewAdapter(Activity, records);
            listView.ItemClick += OnListItemClick;
            return view;
        }
        
        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
        }
        protected void OnListItemClick(object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
        {
            var t = records[e.Position];
            t.switchCompleted(Activity);
            //StartActivityForResult(intent, 0);
            ListView listView = sender as ListView;
            var itemView = listView.GetChildAt(e.Position - listView.FirstVisiblePosition);

            ImageView taskCheck = itemView.FindViewById<ImageView>(Resource.Id.Completed);
            if (t.Completed == 1)
            {
                taskCheck.Visibility = ViewStates.Visible;
            }
            else
            {
                taskCheck.Visibility = ViewStates.Invisible;
            }
        }
        //public override void OnActivityCreated(Bundle savedInstanceState)
        //{
        //    base.OnActivityCreated(savedInstanceState);
        //    _id = Arguments.GetString("ID");
        //    Console.WriteLine(_id);
        //    //db = new _database(Activity);
        //    db = new _database(Activity);

        //    result = (ArrayList)db.ShowAppointmentTasks(_id);
        //    records = new List<TaskRecord>();
        //    foreach (ToDoesRecord value in result)
        //    {

        //        Console.WriteLine(value._id);
        //        TaskRecord row = new TaskRecord(value);
        //        records.Add(row);
        //    }
        //    //ListAdapter = new TaskListViewAdapter(Activity, records);

        //    listView = GetView(Resource.Id.TasksOverview);
        //    listView.Adapter = new TaskListViewAdapter(this, records);
        //    listView.ItemClick += OnListItemClick;
        //}
        //protected void OnListItemClick(ListView listView,View v, int position, long id)
        //{
        //    base.OnListItemClick(listView, v, position, id);
        //    var t = records[position];
        //    t.switchCompleted(Activity);
        //    //StartActivityForResult(intent, 0);
        //    //ListView listView = sender as ListView;
        //    var itemView = listView.GetChildAt(position - listView.FirstVisiblePosition);

        //    ImageView taskCheck = itemView.FindViewById<ImageView>(Resource.Id.Completed);
        //    if (t.Completed == 1)
        //    {
        //        taskCheck.Visibility = ViewStates.Visible;
        //    }
        //    else
        //    {
        //        taskCheck.Visibility = ViewStates.Invisible;
        //    }
        //}

    }
}