
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
    [Activity(Label = "CandidateSummaryActivity", Icon = "@drawable/logo_transparent")]
    public class CandidateSummaryActivity : Activity
    {
        private string candidateNumber;
        private string candidateRole;
        private Candidate candidate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CandidateSummaryLayout);

            candidateNumber = Intent.GetStringExtra("CandidateNumber");
            candidateRole = Intent.GetStringExtra("CandidateRole");

            findCandidate();
        }

        public async void findCandidate()
        {

            //Find information
            BasicSearchService service = new BasicSearchService();
            candidate = await service.FindCandidateFullInfo(candidateNumber, candidateRole);

            //Erase after done


            //Set information into layout
            TextView textName = FindViewById<TextView>(Resource.Id.textView2);
            textName.Text = candidate.Name;

            TextView textParty = FindViewById<TextView>(Resource.Id.textView4);
            textParty.Text = candidate.Party;

            TextView textRole = FindViewById<TextView>(Resource.Id.textView6);
            textRole.Text = candidate.Role;

            //Set information into corruption and votes boxes
            TextView textVote1 = FindViewById<TextView>(Resource.Id.textView10);
            textVote1.Text = candidate.CandidateVotes[0];

            TextView textVote2 = FindViewById<TextView>(Resource.Id.textView12);
            textVote2.Text = candidate.CandidateVotes[1];


            if(candidate.CorruptionCases.Any()){
                TextView textCorruption1 = FindViewById<TextView>(Resource.Id.textView9);
                textCorruption1.Text = candidate.CorruptionCases[0];
            } else {
                TextView textCorruption1 = FindViewById<TextView>(Resource.Id.textView9);
                textCorruption1.Text = "Nothing found against";
            }


            //Setting up SeekBar and Number of Rating
            SeekBar seekBar = FindViewById<SeekBar>(Resource.Id.seekBar1);
            seekBar.Progress = candidate.Rating;

            TextView seekBarNumber = FindViewById<TextView>(Resource.Id.textView17);
            seekBarNumber.Text = "Rate: " + candidate.Rating;
        }
    }
}
