using IronCow;
using IronCow.Rest;
using MilkmanMX.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MilkmanMX
{
    sealed partial class App : Application
    {
        private static readonly string RtmApiKey = "09b03090fc9303804aedd945872fdefc";
        private static readonly string RtmSharedKey = "d2ffaf49356b07f9";

        public static Rtm RtmClient;
        public static Response ListsResponse;
        public static Response TasksResponse;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        public static void LoadData()
        {
            string RtmAuthToken = IsolatedStorageHelper.GetObject<string>("RtmAuthToken");
            int? Timeline = IsolatedStorageHelper.GetObject<int?>("RtmTimeline");
            ListsResponse = IsolatedStorageHelper.GetObject<Response>("ListsResponse");
            TasksResponse = IsolatedStorageHelper.GetObject<Response>("TasksResponse");

            if (!string.IsNullOrEmpty(RtmAuthToken))
            {
                RtmClient = new Rtm(RtmApiKey, RtmSharedKey, RtmAuthToken);
            }
            else
            {
                RtmClient = new Rtm(RtmApiKey, RtmSharedKey);
            }

            RtmClient.Client.UseHttps = true;

            if (Timeline.HasValue)
            {
                RtmClient.CurrentTimeline = Timeline.Value;
            }

            RtmClient.Resources = App.Current.Resources;
        }

        public static void SaveData()
        {
            IsolatedStorageHelper.SaveObject<string>("RtmAuthToken", RtmClient.AuthToken);
            IsolatedStorageHelper.SaveObject<int?>("RtmTimeline", RtmClient.CurrentTimeline);
            IsolatedStorageHelper.SaveObject<Response>("ListsResponse", ListsResponse);
            IsolatedStorageHelper.SaveObject<Response>("TasksResponse", TasksResponse);
        }

        public static void DeleteData()
        {
            IsolatedStorageHelper.DeleteObject("RtmAuthToken");
            IsolatedStorageHelper.DeleteObject("ListsResponse");
            IsolatedStorageHelper.DeleteObject("TasksResponse");
            IsolatedStorageHelper.DeleteObject("RtmTimeline");

            RtmClient = new Rtm(RtmApiKey, RtmSharedKey);
            ListsResponse = null;
            TasksResponse = null;

            RtmClient.Resources = App.Current.Resources;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    SmartDispatcher.Initialize(rootFrame.Dispatcher);

                    LoadData();
                }

                Window.Current.Content = rootFrame;
            }

            SmartDispatcher.Initialize(rootFrame.Dispatcher);

            LoadData();

            if (rootFrame.Content == null)
            {
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            Window.Current.Activate();
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}
