using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MVPMixTweets.WindowsPhone.Resources;
using MyStocks.Forms.ViewModels;

namespace MVPMixTweets.WindowsPhone
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();
      viewModel = new TwitterViewModel();
      // Sample code to localize the ApplicationBar
      //BuildLocalizedApplicationBar();
    }
    private TwitterViewModel viewModel;
    private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
    {
      Progress.Visibility = System.Windows.Visibility.Visible;

      await viewModel.LoadTweetsCommand(TextBoxSearch.Text);
      Tweets.ItemsSource = viewModel.Tweets;

      Progress.Visibility = System.Windows.Visibility.Collapsed;
    }

    private void Tweets_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if(Tweets.SelectedItem == null)
        return;

      viewModel.Speak(Tweets.SelectedIndex);
    }

    // Sample code for building a localized ApplicationBar
    //private void BuildLocalizedApplicationBar()
    //{
    //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
    //    ApplicationBar = new ApplicationBar();

    //    // Create a new button and set the text value to the localized string from AppResources.
    //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
    //    appBarButton.Text = AppResources.AppBarButtonText;
    //    ApplicationBar.Buttons.Add(appBarButton);

    //    // Create a new menu item with the localized string from AppResources.
    //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
    //    ApplicationBar.MenuItems.Add(appBarMenuItem);
    //}
  }
}