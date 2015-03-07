using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using MyStocks.Forms.ViewModels;
using Refractored.Xam.TTS;

namespace MVPMixTweets.Droid
{
  [Activity(Label = "MVPMix Tweets", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : ActionBarActivity
  {

    TwitterViewModel viewModel;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);
      var buttonGet = FindViewById<Button>(Resource.Id.MyButton);
      var search = FindViewById<EditText>(Resource.Id.editText1);
      var progress = FindViewById<ProgressBar>(Resource.Id.progressBar1);
      progress.Indeterminate = true;
      progress.Visibility = ViewStates.Gone;

      var list = FindViewById<ListView>(Resource.Id.listView1);
      viewModel = new TwitterViewModel();
      buttonGet.Click += async (sender, args) =>
        {
          progress.Visibility = ViewStates.Visible;
          buttonGet.Enabled = false;

          await viewModel.LoadTweetsCommand(search.Text);

          list.Adapter = new TweetAdapter(this, viewModel);
          buttonGet.Enabled = true;
          progress.Visibility = ViewStates.Gone;
        };

      list.ItemClick += (sender, args) =>
        {
          viewModel.Speak(args.Position);
        };
    }
  }
}

