using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using ZAPP.Fragments;
using Fragment = Android.Support.V4.App.Fragment;

namespace ZAPP.Adapters
{
    class AppointmentsViewPagerAdapter : FragmentPagerAdapter
    {
        List<Fragment> Fragments { get; set; }
        List<string> FragmentNames { get; set; }
        //int Id { get; set; }
        
        //Fragments
        AppointmentTasksFragment TasksFragment = new AppointmentTasksFragment();
        AppointmentAddressFragment AddressFragment = new AppointmentAddressFragment();

        public AppointmentsViewPagerAdapter(Android.Support.V4.App.FragmentManager fragmentManager, string _id): base(fragmentManager)
        {
            Bundle args = new Bundle();
            Fragments = new List<Fragment>();
            FragmentNames = new List<string>();
            args.PutString("ID", _id.ToString());
            TasksFragment.Arguments = args;
            AddressFragment.Arguments = args;
            Fragments.Add(TasksFragment);
            Fragments.Add(AddressFragment);
            FragmentNames.Add("Tasks");
            FragmentNames.Add("Address");
        }


        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return Fragments[position];
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override int Count
        {
            get
            {
                return Fragments.Count;
            }
        }
        public void AddFragment(Fragment fragment, string name)
        {
            Fragments.Add(fragment);
            FragmentNames.Add(name);
        }
        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(FragmentNames[position]);
        }
        //public override View GetView(int position, View convertView, ViewGroup parent)
        //{
        //    var view = convertView;
        //    AppointmentsViewPagerAdapterViewHolder holder = null;

        //    if (view != null)
        //        holder = view.Tag as AppointmentsViewPagerAdapterViewHolder;

        //    if (holder == null)
        //    {
        //        holder = new AppointmentsViewPagerAdapterViewHolder();
        //        var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
        //        //replace with your item and your holder items
        //        //comment back in
        //        //view = inflater.Inflate(Resource.Layout.item, parent, false);
        //        //holder.Title = view.FindViewById<TextView>(Resource.Id.text);
        //        view.Tag = holder;
        //    }


        //    //fill in your items
        //    //holder.Title.Text = "new text here";

        //    return view;
        //}

        //Fill in cound here, currently 0


    }

    class AppointmentsViewPagerAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}