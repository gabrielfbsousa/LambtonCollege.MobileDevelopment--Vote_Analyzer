
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
    [Activity(Label = "ElectionSimulationActivity", Icon = "@drawable/logo_transparent")]
    public class ElectionSimulationActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ElectionSimulationLayout);

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
            Button button = FindViewById<Button>(Resource.Id.button1);
            button.Click += delegate
            {
                simulate();
            };
        }

        public async void simulate(){
            BasicSearchService service = new BasicSearchService();

            //President
            TextView numberText = FindViewById<TextView>(Resource.Id.editText1);
            string numberPresident = numberText.Text;
            Candidate president = await service.FindCandidateInformation(numberPresident, "President");

            TextView presidentName = FindViewById<TextView>(Resource.Id.textView2);
            presidentName.Text = president.Name;

            //Senator
            TextView numberSenatorText = FindViewById<TextView>(Resource.Id.editText2);
            string numberSenator = numberSenatorText.Text;
            Candidate senator = await service.FindCandidateInformation(numberSenator, "Senator");

            TextView senatorName = FindViewById<TextView>(Resource.Id.textView4);
            senatorName.Text = senator.Name;

            //Deputy
            TextView numberDeputyText = FindViewById<TextView>(Resource.Id.editText3);
            string numberDeputy = numberDeputyText.Text;
            Candidate deputy = await service.FindCandidateInformation(numberDeputy, "Deputy");

            TextView deputyName = FindViewById<TextView>(Resource.Id.textView6);
            deputyName.Text = deputy.Name;

        }
    }
}
