using IronCow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MilkmanMX.Controls
{
    public partial class TaskItemControl : UserControl
    {
        public TaskItemControl()
        {
            InitializeComponent();

            this.Loaded += TaskItemControl_Loaded;
        }

        private bool DarkThemeUsed()
        {
            return Visibility.Visible == (Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"];
        }

        private void TaskItemControl_Loaded(object sender, RoutedEventArgs e)
        {
            Task target = (Task)this.DataContext;

            if (this.DarkThemeUsed() == true)
            {
                this.stkDark.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.stkLight.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                this.stkLight.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.stkDark.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }
    }
}
