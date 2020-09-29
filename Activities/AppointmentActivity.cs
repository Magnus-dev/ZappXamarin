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

        //Fragments
        AppointmentTasksFragment TasksFragment = new AppointmentTasksFragment();
        AppointmentAddressFragment AddressFragment = new AppointmentAddressFragment();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Appointment);
            ConnectViews();
            
            //Button RegisterButton = FindViewById<Button>(Resource.Id.registerTime);

            //RegisterButton.Click += delegate
            //{
            //    RegisterButtonClicked();
            //};
        }
        void ConnectViews()
        {
            pagertabstrip = (PagerTabStrip)FindViewById(Resource.Id.TabStrip);
            viewPager = (ViewPager)FindViewById(Resource.Id.viewPager);
            viewPager.OffscreenPageLimit = 1;
            viewPager.BeginFakeDrag();

            SetupViewPager();
        }

        private void SetupViewPager()
        {
            AppointmentsViewPagerAdapter adapter = new AppointmentsViewPagerAdapter(SupportFragmentManager);
            adapter.AddFragment(TasksFragment, "Tasks");
            adapter.AddFragment(AddressFragment, "Address");
            viewPager.Adapter = adapter;
        }
    }
}