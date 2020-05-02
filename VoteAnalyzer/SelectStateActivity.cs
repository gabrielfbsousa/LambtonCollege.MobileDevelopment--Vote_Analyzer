
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace VoteAnalyzer
{
    [Activity(Label = "SelectStateActivity", Icon = "@drawable/logo_transparent")]
    public class SelectStateActivity : Activity
    {

        public Spinner spinner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectStateLayout);

            //Obter o estado selecionado
            /*
            var items = new List<string>() { "one", "two", "three" }; //Colocar estados
            var adapter = new ArrayAdapter<string>(this, Resource.Id.spinner1, items);
            spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            spinner.Adapter = adapter;
            */

            // Create your application here
            Button button = FindViewById<Button>(Resource.Id.button1);

            button.Click += delegate {
                StartActivity(typeof(MainLayoutActivity));
            };
        }
    }
}
