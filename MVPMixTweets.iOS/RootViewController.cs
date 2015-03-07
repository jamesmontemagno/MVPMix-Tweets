using System;
using System.Drawing;

using Foundation;
using UIKit;
using MyStocks.Forms.ViewModels;
using SDWebImage;



namespace MVPMixTweets.iOS
{
  public partial class RootViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
  {
    public RootViewController(IntPtr handle)
      : base(handle)
    {
    }

    public override void DidReceiveMemoryWarning()
    {
      // Releases the view if it doesn't have a superview.
      base.DidReceiveMemoryWarning();

      // Release any cached data, images, etc that aren't in use.
    }

    #region View lifecycle
    private UIActivityIndicatorView activityIndicator;
    private TwitterViewModel viewModel = new TwitterViewModel();
    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      TableViewTweets.WeakDataSource = this;
      TableViewTweets.WeakDelegate = this;
      activityIndicator = new UIActivityIndicatorView(new CoreGraphics.CGRect(0, 0, 20, 20));
      activityIndicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White;
      activityIndicator.HidesWhenStopped = true;
      NavigationItem.LeftBarButtonItem = new UIBarButtonItem(activityIndicator);
    }


    #endregion

    async partial void ButtonGetTweets_TouchUpInside(UIButton sender)
    {
      activityIndicator.StartAnimating();
      ButtonGetTweets.Enabled = false;

      TextFieldSearch.ResignFirstResponder();
      await viewModel.LoadTweetsCommand(TextFieldSearch.Text);
      TableViewTweets.ReloadData();


      ButtonGetTweets.Enabled = true;
      activityIndicator.StopAnimating();
    }

    public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
    {
      var cell = tableView.DequeueReusableCell("tweet", indexPath);

      var tweet = viewModel.Tweets[indexPath.Row];
      cell.TextLabel.Text = tweet.DisplayName;
      cell.DetailTextLabel.Text = tweet.Text;
      cell.ImageView.SetImage(
          url: new NSUrl(tweet.Image)
        );
      return cell;
    }

    public nint RowsInSection(UITableView tableView, nint section)
    {
      return viewModel.Tweets.Count;
    }

    public void RowSelected(UITableView tableView, NSIndexPath indexPath)
    {
      viewModel.Speak(indexPath.Row);
    }
    
  }
}