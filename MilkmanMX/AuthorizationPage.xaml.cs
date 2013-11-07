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
    public partial class AuthorizationPage : Page
    {
        #region Construction and Navigation

        private string Frob { get; set; }

        public AuthorizationPage()
        {
            InitializeComponent();

            //App.UnhandledExceptionHandled += new EventHandler<ApplicationUnhandledExceptionEventArgs>(App_UnhandledExceptionHandled);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            StartAuth();
            //MessageBox.Show(Strings.AuthorizationDialog.Replace("\\n", "\n"), Strings.AuthorizationDialogTitle, MessageBoxButton.OK);
        }

        //private void App_UnhandledExceptionHandled(object sender, ApplicationUnhandledExceptionEventArgs e)
        //{
        //    Dispatcher.BeginInvoke(() =>
        //    {
        //        GlobalLoading.Instance.IsLoading = false;
        //    });
        //}

        private void StartAuth()
        {
            //GlobalLoading.Instance.IsLoadingText(Strings.Loading);

            App.RtmClient.GetFrob((string frob) =>
            {
                Frob = frob;
                string url = App.RtmClient.GetAuthenticationUrl(frob, AuthenticationPermissions.Delete);

                SmartDispatcher.BeginInvoke(() =>
                {
                    //GlobalLoading.Instance.IsLoading = false;
                    webAuthorization.Navigate(new Uri(url));
                });
            });
        }

        #endregion

        #region Event Handling

        private void btnComplete_Click(object sender, RoutedEventArgs e)
        {
            // only do something if Frob is present
            if (!string.IsNullOrEmpty(Frob))
            {
                //GlobalLoading.Instance.IsLoadingText(Strings.Authorizing);

                App.RtmClient.GetToken(Frob, (string token, User user) =>
                {
                    // create timeline
                    App.RtmClient.GetOrStartTimeline((int timeline) =>
                    {
                        App.SaveData();
                        
                        SmartDispatcher.BeginInvoke(() =>
                        {
                            //GlobalLoading.Instance.IsLoading = false;

                            if (Frame.CanGoBack)
                            {
                                MainPage.sReload = true;

                                Frame.BackStack.RemoveAt(0);
                                Frame.Navigate(typeof(MainPage), "IsFirstRun=true");
                            }
                        });
                    });
                });
            }
        }

        private void btnRetry_Click(object sender, RoutedEventArgs e)
        {
            StartAuth();
        }

        private void webAuthorization_FrameNavigationStarting(object sender, WebViewNavigationStartingEventArgs e)
        {
            //GlobalLoading.Instance.IsLoadingText(Strings.Loading);
        }

        private void webAuthorization_FrameNavigationCompleted(object sender, WebViewNavigationCompletedEventArgs e)
        {
            //GlobalLoading.Instance.IsLoading = false;

            if (this.webAuthorization.DocumentTitle.Contains("Remember The Milk - Application successfully authorized"))
            {
                btnComplete.IsEnabled = true;
            }
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