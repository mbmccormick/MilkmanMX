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
using MilkmanMX.Helpers;
using IronCow.Resources;
using MilkmanMX.Common;

namespace MilkmanMX
{
    public sealed partial class MainPage : Page
    {
        public static bool sReload = true;

        #region Task Lists Property

        public ObservableCollection<TaskList> TaskLists
        {
            get { return (ObservableCollection<TaskList>)GetValue(TaskListsProperty); }
            set { SetValue(TaskListsProperty, value); }
        }

        public static readonly DependencyProperty TaskListsProperty =
               DependencyProperty.Register("TaskLists", typeof(ObservableCollection<TaskList>), typeof(MainPage),
                   new PropertyMetadata(new ObservableCollection<TaskList>()));

        #endregion

        #region Construction and Navigation

        public MainPage()
        {
            this.InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            SyncData();
        }

        #endregion

        #region Loading Data

        private void SyncData()
        {
            if (sReload)
            {
                if (!string.IsNullOrEmpty(App.RtmClient.AuthToken))
                {
                    App.RtmClient.SyncEverything(() =>
                    {
                        LoadData();
                    });
                }
                else
                {
                    Login();
                }
            }

            sReload = false;
        }

        private void LoadData()
        {
            LoadDataInBackground();
        }

        private void LoadDataInBackground()
        {
            SmartDispatcher.BeginInvoke(() =>
            {
                if (App.RtmClient.TaskLists != null)
                {
                    var tempTaskLists = new SortableObservableCollection<TaskList>();

                    foreach (TaskList l in App.RtmClient.TaskLists)
                    {
                        if (l.Name.ToLower() == StringsProvider.GetString("AllTasksLower"))
                            tempTaskLists.Insert(0, l);
                        else
                            tempTaskLists.Add(l);
                    }

                    // insert the nearby list placeholder
                    TaskList nearby = new TaskList(StringsProvider.GetString("Nearby"));
                    tempTaskLists.Insert(1, nearby);

                    TaskLists = tempTaskLists;
                }
            });
        }

        public void Login()
        {
            this.Frame.Navigate(typeof(AuthorizationPage));
        }

        #endregion

        #region Event Handling

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        #endregion
    }
}
