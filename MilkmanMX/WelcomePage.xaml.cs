using IronCow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MilkmanMX.Common;

namespace MilkmanMX
{
    public partial class WelcomePage : Page
    {
        #region Construction and Navigation

        public WelcomePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Frame.CanGoBack == true)
                Frame.BackStack.RemoveAt(0);
        }

        #endregion

        #region Event Handlers

        private void stkSignIn_Tapped(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AuthorizationPage));
        }

        private void stkRegister_Tapped(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(Strings.RegistrationDialog.Replace("\\n", "\n"), Strings.RegistrationDialogTitle, MessageBoxButton.OK);

            //WebBrowserTask webBrowserTask = new WebBrowserTask();

            //webBrowserTask.Uri = new Uri("http://www.rememberthemilk.com/signup/");
            //webBrowserTask.Show();
        }

        private void stkAbout_Tapped(object sender, RoutedEventArgs e)
        {
            //SmartDispatcher.BeginInvoke(() =>
            //{
            //    NavigationService.Navigate(new Uri("/YourLastAboutDialog;component/AboutPage.xaml", UriKind.Relative));
            //});
        }

        //private void mnuAbout_Click(object sender, EventArgs e)
        //{
        //    SmartDispatcher.BeginInvoke(() =>
        //    {
        //        NavigationService.Navigate(new Uri("/YourLastAboutDialog;component/AboutPage.xaml", UriKind.Relative));
        //    });
        //}

        //private void mnuFeedback_Click(object sender, EventArgs e)
        //{
        //    EmailComposeTask emailComposeTask = new EmailComposeTask();

        //    emailComposeTask.To = "feedback@mbmccormick.com";
        //    emailComposeTask.Subject = "Milkman Feedback";
        //    emailComposeTask.Body = "Version " + App.ExtendedVersionNumber + " (" + App.PlatformVersionNumber + ")\n\n";
        //    emailComposeTask.Show();
        //}

        //private async void mnuDonate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var productList = await CurrentApp.LoadListingInformationAsync();
        //        var product = productList.ProductListings.FirstOrDefault(p => p.Value.ProductType == ProductType.Consumable);
        //        var receipt = await CurrentApp.RequestProductPurchaseAsync(product.Value.ProductId, true);

        //        if (CurrentApp.LicenseInformation.ProductLicenses[product.Value.ProductId].IsActive)
        //        {
        //            CurrentApp.ReportProductFulfillment(product.Value.ProductId);

        //            MessageBox.Show(Strings.DonateDialog, Strings.DonateDialogTitle, MessageBoxButton.OK);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // do nothing
        //    }
        //}

        #endregion
    }
}