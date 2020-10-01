using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Transitions;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;

namespace ZAPP
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        LinearLayout layout;
        //View v;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            layout = FindViewById<LinearLayout>(Resource.Id.LoginLayout);
            layout.FindViewById<ImageView>(Resource.Id.LogoImg);
            Button LoginButton = layout.FindViewById<Button>(Resource.Id.LoginButton);
            EditText email = FindViewById<EditText>(Resource.Id.Email);
            EditText password = FindViewById<EditText>(Resource.Id.Password);
            
            LoginButton.Click += delegate
            {
                LoginButtonClicked( email.Text, password.Text);
            };
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
        
        protected void LoginButtonClicked(string email, string password)
        {
            string answer = Services.Webclient.LoginRequest(email, password);
            if(answer == null)
            {
                TextView warning = FindViewById<TextView>(Resource.Id.Warning);
                warning.Text = "Wrong Password or Username. Please try again.";
                warning.Visibility = ViewStates.Visible ;
                Toast.MakeText(this, "Please try again", ToastLength.Long).Show();
            }
            //if(answer == "not_found")
            //{
            //    Toast toast = new Toast(this);
                
            //}
            if(answer != null)
            {
                _database db = new _database(this);
                db.SetApiKey(answer);
                var intent = new Intent(this, typeof(SplashActivity));
                StartActivityForResult(intent, 0);
            }
            

        }
    }
}