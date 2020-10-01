using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System.Collections;
using ZAPP.Adapters;
using ZAPP.Fragments;

namespace ZAPP.Activities
{
    [Activity(Label = "AppointmentActivity")]
    public class AppointmentActivity : AppCompatActivity
    {
        //Views
        ViewPager viewPager;
        PagerTabStrip pagertabstrip;

        //TaskAppointments
        _database db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Appointment);
            ConnectViews();

            Button RegisterButton = FindViewById<Button>(Resource.Id.registerTime);

            RegisterButton.Click += delegate
            {
                RegisterButtonClicked();
            };

            
        }
        void ConnectViews()
        {
            pagertabstrip = (PagerTabStrip)FindViewById(Resource.Id.TabStrip);
            viewPager = (ViewPager)FindViewById(Resource.Id.viewPager);
            viewPager.OffscreenPageLimit = 0;
            //viewPager.BeginFakeDrag();

            SetupViewPager();
        }

        private void SetupViewPager()
        {
            Console.WriteLine(Intent.GetStringExtra("ID"));
            AppointmentsViewPagerAdapter adapter = new AppointmentsViewPagerAdapter(SupportFragmentManager, Intent.GetStringExtra("ID"));
            //adapter.AddFragment(TasksFragment, "Tasks");
            //adapter.AddFragment(AddressFragment, "Address");
            
            viewPager.Adapter = adapter;
        }
        protected void RegisterButtonClicked()
        {
            db = new _database(this);
            string _id = Intent.GetStringExtra("ID");
            Console.WriteLine(_id);
            AppointmentRecord record = db.getAppointmentFromTable(_id);
            string now = DateTime.Now.ToString();
            Console.WriteLine(record.startTime.Length);
            if (record.startTime.Length == 0)
            {
                record.SetStartTime(db, now);
                Services.Webclient.UploadStartTime(_id, now, db.GetApiKey());
                Toast.MakeText(this, "Starttime was Set", ToastLength.Long);
            }
            else
            {
                if (record.endTime.Length == 0)
                {
                    record.SetEndTime(db, now);
                    Services.Webclient.UploadEndTime(_id, now, db.GetApiKey());
                    Toast.MakeText(this, "Endtime was Set", ToastLength.Long);
                    var intent = new Intent(this, typeof(Home));
                    StartActivityForResult(intent, 0);
                }

            }
            //TextView warning = FindViewById<TextView>(Resource.Id.Warning);
            //warning.Visibility = ViewStates.Invisible;

        }
    }
}