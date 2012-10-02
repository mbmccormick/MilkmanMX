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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MilkmanMX
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SyncData();
        }

        #region Loading Data

        private void SyncData()
        {
            if (!string.IsNullOrEmpty(App.RtmClient.AuthToken))
            {
                LoadData();
            }
            else
            {
                Login();
            }
        }

        private void LoadData()
        {
            LoadDataInBackground();
        }

        private void LoadDataInBackground()
        {
            //if (App.RtmClient.TaskLists != null)
            //{
            //    var tempTaskLists = new ObservableCollection<TaskList>();

            //    foreach (TaskList l in App.RtmClient.TaskLists)
            //    {
            //        tempTaskLists.Add(l);
            //    }
            //}
        }

        public void Login()
        {
            this.Frame.Navigate(typeof(AuthorizationPage));
        }

        #endregion

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }
    }
}
