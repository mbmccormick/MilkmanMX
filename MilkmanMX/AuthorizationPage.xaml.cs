using IronCow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MilkmanMX.Helpers;

namespace MilkmanMX
{
    public sealed partial class AuthorizationPage : MilkmanMX.Common.LayoutAwarePage
    {
        #region Construction and Navigation

        private string Frob { get; set; }

        public AuthorizationPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            StartAuth();
        }

        #endregion

        #region Loading Data

        private void StartAuth()
        {
            App.RtmClient.GetFrob((string frob) =>
            {
                Frob = frob;
                string url = App.RtmClient.GetAuthenticationUrl(frob, AuthenticationPermissions.Delete);

                SmartDispatcher.BeginInvoke(() =>
                {
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
                App.RtmClient.GetToken(Frob, (string token, User user) =>
                {
                    // create timeline
                    App.RtmClient.GetOrStartTimeline((int timeline) =>
                    {
                        SmartDispatcher.BeginInvoke(() =>
                        {
                            if (this.Frame.CanGoBack)
                            {
                                this.Frame.Navigate(typeof(MainPage));
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

        #endregion
    }
}
