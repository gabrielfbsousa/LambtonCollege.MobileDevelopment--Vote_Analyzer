
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
    [Activity(Label = "BasicSearchActivity", Icon = "@drawable/logo_transparent")]
    public class BasicSearchActivity : Activity
    {

        public String candidateNumber;
        public String candidateRole;
        public Candidate candidate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.BasicSearchLayout);

            //SetupButtons
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


            // Numbers Pad
            Button button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += delegate {
                candidateNumber += "1";
                TextView text = FindViewById<TextView>(Resource.Id.textView3);
                text.Text = candidateNumber;
            };

            Button button2 = FindViewById<Button>(Resource.Id.button2);
            button2.Click += delegate {
                candidateNumber += "2";
                TextView text = FindViewById<TextView>(Resource.Id.textView3);
                text.Text = candidateNumber;
            };

            Button button3 = FindViewById<Button>(Resource.Id.button3);
            button3.Click += delegate {
                candidateNumber += "3";
                TextView text = FindViewById<TextView>(Resource.Id.textView3);
                text.Text = candidateNumber;
            };

            Button button4 = FindViewById<Button>(Resource.Id.button4);
            button4.Click += delegate {
                candidateNumber += "4";
                TextView text = FindViewById<TextView>(Resource.Id.textView3);
                text.Text = candidateNumber;
            };

            Button button5 = FindViewById<Button>(Resource.Id.button5);
            button5.Click += delegate {
                candidateNumber += "5";
                TextView text = FindViewById<TextView>(Resource.Id.textView3);
                text.Text = candidateNumber;
            };

            Button button6 = FindViewById<Button>(Resource.Id.button6);
            button6.Click += delegate {
                candidateNumber += "6";
                TextView text = FindViewById<TextView>(Resource.Id.textView3);
                text.Text = candidateNumber;
            };

            Button button7 = FindViewById<Button>(Resource.Id.button11);
            button7.Click += delegate {
                candidateNumber += "7";
                TextView text = FindViewById<TextView>(Resource.Id.textView3);
                text.Text = candidateNumber;
            };

            Button button8 = FindViewById<Button>(Resource.Id.button12);
            button8.Click += delegate {
                candidateNumber += "8";
                TextView text = FindViewById<TextView>(Resource.Id.textView3);
                text.Text = candidateNumber;
            };

            Button button9 = FindViewById<Button>(Resource.Id.button13);
            button9.Click += delegate {
                candidateNumber += "9";
                TextView text = FindViewById<TextView>(Resource.Id.textView3);
                text.Text = candidateNumber;
            };

            Button button0 = FindViewById<Button>(Resource.Id.button14);
            button0.Click += delegate {
                candidateNumber += "0";
                TextView text = FindViewById<TextView>(Resource.Id.textView3);
                text.Text = candidateNumber;
            };


            // Operations Buttons
            Button buttonNull = FindViewById<Button>(Resource.Id.button8);
            buttonNull.Click += delegate {
                candidateNumber = "";
                candidateNumber += "-----";
                TextView text = FindViewById<TextView>(Resource.Id.textView3);
                text.Text = candidateNumber;
            };

            Button buttonCorrect = FindViewById<Button>(Resource.Id.button9);
            buttonNull.Click += delegate {
                candidateNumber = "";
                TextView text = FindViewById<TextView>(Resource.Id.textView3);
                text.Text = candidateNumber;
            };

            Button buttonConfirm = FindViewById<Button>(Resource.Id.button10);
            buttonConfirm.Click += delegate {
                //Find candidate based on the number and the role
                 findCandidateRole();

                //pass the parameter to the Summary Activity 
                Intent intent = new Intent(this, typeof(CandidateSummaryActivity));
                intent.PutExtra("CandidateNumber", candidateNumber);
                intent.PutExtra("CandidateRole", candidateRole);
                StartActivity(intent);
                //StartActivity(typeof(CandidateSummaryActivity));
            };

        }

        public void findCandidateRole(){
            

            RadioGroup group = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            RadioButton radioButton = FindViewById<RadioButton>(group.CheckedRadioButtonId);

            if(radioButton.Text.Equals("Deputy")){
                candidateRole = "Deputy"; 
            } else if(radioButton.Text.Equals("Senator")){
                candidateRole = "Senator";
            } else {
                candidateRole = "President";
            }

        }
    }
}
