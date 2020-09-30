using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ZAPP.Fragments
{
    public class AppointmentAddressFragment : Android.Support.V4.App.Fragment
    {
        string _id;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.AppointmentAdress, container, false);
            _id = Arguments.GetString("ID");
            _database db = new _database(Activity);
            AppointmentRecord record = db.getAppointmentFromTable(_id);
            TextView Address = view.FindViewById<TextView>(Resource.Id.Address);
            Address.Text = record.address;
            TextView City = view.FindViewById<TextView>(Resource.Id.City);
            City.Text = record.postcode + "   " + record.city;
            TextView Phonenumber = view.FindViewById<TextView>(Resource.Id.Phonenumber);
            Phonenumber.Text = "Telefoonnummer:  " + record.phoneNumber;
            TextView NoticeTitle = view.FindViewById<TextView>(Resource.Id.NoticeTitle);
            NoticeTitle.Text = "Beheerder Notice:  ";
            TextView Notice = view.FindViewById<TextView>(Resource.Id.Notice);
            Notice.Text = record.notice;

            return view;
        }
    }
}