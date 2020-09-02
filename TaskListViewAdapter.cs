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
    [Activity(Label = "TaskListViewAdapter")]
    class TaskListViewAdapter : BaseAdapter<TaskRecord>
    {
        List<TaskRecord> items;
        Activity context;
        // Context context;
        public TaskListViewAdapter(Activity context, List<TaskRecord> items) : base()
        {
            this.context = context;
            this.items = items;
        }
        public override TaskRecord this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            var view = convertView;


            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.TaskListRow, null);
            }
            view.FindViewById<TextView>(Resource.Id.Task1).Text = item.Description;
            if(item.Completed ==1)
            {
                view.FindViewById<TextView>(Resource.Id.Task2).Text = " Completed";
            }
            else
            {
                view.FindViewById<TextView>(Resource.Id.Task2).Text = "NOT";
            }
           
            //view.FindViewById<TextView>(Resource.Id.Text3).Text = item.address + ", " + item.postcode + " " + item.city;
            return view;
        }


    }

    class TaskListViewAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}