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
    [Activity(Label = "Home")]
    public class Home : Activity
    {
        ListView listView;
        List<ListRecord> records;
        ArrayList result;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _database db = new _database(this);
            result = (ArrayList)db.ShowAllAppointmentData();
            records = new List<ListRecord>();
            foreach (AppointmentRecord value in result)
            {
                ListRecord row = new ListRecord(value);
                if (row.endTime == "")
                {
                    records.Add(row);
                }
                
            }
            SetContentView(Resource.Layout.Home);
            listView = FindViewById<ListView>(Resource.Id.Overview);
            listView.Adapter = new HomeListViewAdapter(this, records);
            listView.ItemClick += OnListItemClick;
        }
        protected void OnListItemClick(object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
        {
            var t = records[e.Position];
            var intent = new Intent(this, typeof(Activities.AppointmentActivity));
            intent.PutExtra("ID", t._id);

            StartActivityForResult(intent, 0);
        }
    }
}