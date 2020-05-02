
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
    [Activity(Label = "RankingActivity", Icon = "@drawable/logo_transparent")]
    public class RankingActivity : Activity
    {
        private string candidateRole;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RankingLayout);

            //Setup Buttons
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
            Button goButton = FindViewById<Button>(Resource.Id.button1);
            goButton.Click += delegate {
                populate();
            };
        }

        public async void populate(){
            RadioGroup group = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            RadioButton radioButton = FindViewById<RadioButton>(group.CheckedRadioButtonId);
            RankingService service = new RankingService();
            List<Candidate> candidates = null;

            if (radioButton.Text.Equals("Deputy"))
            {
                candidateRole = "Deputy";
                candidates = await service.FindDeputyRanking();
            }
            else if (radioButton.Text.Equals("Senator"))
            {
                candidateRole = "Senator";
                candidates = await service.FindSenatorRanking();
            }
            else
            {
                candidateRole = "President";
                //candidates = await service.FindPresidentRanking();
            }

                TextView firstPlaceName = FindViewById<TextView>(Resource.Id.textView3);
                firstPlaceName.Text = candidates[0].Name;

                TextView firstPlaceRating = FindViewById<TextView>(Resource.Id.textView4);
            firstPlaceRating.Text = "Rate: " +candidates[0].Rating + "";

                TextView secondPlaceName = FindViewById<TextView>(Resource.Id.textView5);
                secondPlaceName.Text = candidates[1].Name;

            TextView secondPlaceRating = FindViewById<TextView>(Resource.Id.textView6);
            secondPlaceRating.Text = "Rate: " + candidates[1].Rating + "";

                TextView thirdPlaceName = FindViewById<TextView>(Resource.Id.textView7);
            thirdPlaceName.Text = candidates[2].Name;

            TextView thirdPlaceRating = FindViewById<TextView>(Resource.Id.textView8);
            thirdPlaceRating.Text = "Rate: " + candidates[2].Rating + "";

            TextView fourthPlaceName = FindViewById<TextView>(Resource.Id.textView9);
            fourthPlaceName.Text = candidates[3].Name;

            TextView fourthPlaceRating = FindViewById<TextView>(Resource.Id.textView10);
            fourthPlaceRating.Text = "Rate: " + candidates[3].Rating + "";

            TextView fifthPlaceName = FindViewById<TextView>(Resource.Id.textView11);
            fifthPlaceName.Text = candidates[4].Name;

            TextView fifthPlaceRating = FindViewById<TextView>(Resource.Id.textView12);
            fifthPlaceRating.Text = "Rate: " + candidates[4].Rating + "";

            TextView sixthPlaceName = FindViewById<TextView>(Resource.Id.textView13);
            sixthPlaceName.Text = candidates[5].Name;

            TextView sixthPlaceRating = FindViewById<TextView>(Resource.Id.textView14);
            sixthPlaceRating.Text ="Rate: " + candidates[5].Rating + "";



        }
    }
}
