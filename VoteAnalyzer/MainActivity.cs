using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace VoteAnalyzer
{
    [Activity(Label = "VoteAnalyzer", MainLauncher = true, Icon = "@drawable/logo_transparent")]
    public class MainActivity : Activity 
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);



            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate { 
                StartActivity(typeof(SelectStateActivity));
            };


        }
    }
}

