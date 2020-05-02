
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

namespace VoteAnalyzer
{
    [Activity(Label = "MainLayoutActivity", Icon = "@drawable/logo_transparent")]
    public class MainLayoutActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MainLayout);

            ImageButton HomeButton = FindViewById<ImageButton>(Resource.Id.imageButton1);
            HomeButton.Click += delegate {
                StartActivity(typeof(MainLayoutActivity));
            };

            ImageButton SearchButton = FindViewById<ImageButton>(Resource.Id.imageButton2);
            SearchButton.Click += delegate {
                StartActivity(typeof(BasicSearchActivity));
            };

            ImageButton SimulationButton = FindViewById<ImageButton>(Resource.Id.imageButton3);
            SimulationButton.Click += delegate {
                StartActivity(typeof(ElectionSimulationActivity));
            };

            ImageButton RankingButton = FindViewById<ImageButton>(Resource.Id.imageButton4);
            RankingButton.Click += delegate {
                StartActivity(typeof(RankingActivity));
            };


            // Create your application here
        }
    }
}
