﻿using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace VoteAnalyzer
{
    [Activity(Label = "Adapter", MainLauncher = true, Icon = "@drawable/icon")]
    public class RankingAdapter : BaseAdapter<Candidate>
    {
        private readonly Activity context;
        private List<Candidate> candidates;

        public RankingAdapter(Activity context, List<Candidate> candidates) : base()
        {
            this.context = context;
            this.candidates = candidates;
        }

        public override Candidate this[int position] => throw new NotImplementedException();

        public override int Count => throw new NotImplementedException();

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position];
            return view;
        }




    }
}
