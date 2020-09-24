﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
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

            
            
           
            var intent = new Intent(this, typeof(Home));
            StartActivityForResult(intent, 0);
        }
    }
}