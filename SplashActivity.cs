using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace ZAPP
{
    using System.Threading;
    using Android.App;
    using Android.OS;
    using Java.Util;

    [Activity(Label = "@string/app_name", Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _database db = new _database(this);
            db.ApiProcessing();
            //db.showAllData();
            //Thread.Sleep(500);
            if (db.GetApiKey() == null)
            {
                StartActivity(typeof(LoginActivity));
            }
            else
            {
                StartActivity(typeof(Home));
            }
        }
        /* protected override void OnCreate(Bundle savedInstanceState)
         {
             base.OnCreate(savedInstanceState);
             Xamarin.Essentials.Platform.Init(this, savedInstanceState);
             // Set our view from the "main" layout resource
             SetContentView(Resource.Layout.Home);
         }*/
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
