using System;
using System.Net;
using System.Windows;
using Windows.ApplicationModel.Resources;

namespace IronCow.Resources
{
    public class StringsProvider
    {
        private static ResourceLoader _resources = null;

        public static string GetString(string key)
        {
            if (_resources == null)
            {
                _resources = new ResourceLoader("IronCow/Strings");
            }

            return _resources.GetString(key);
        } 
    }
}
