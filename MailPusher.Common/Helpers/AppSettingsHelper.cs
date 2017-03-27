using MailPusher.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailPusher.Common.Helpers
{
    public class AppSettingsHelper
    {
        public static string GetValueFromAppSettings(AppSettingsKey key)
        {
            if (System.Configuration.ConfigurationManager.AppSettings[key.ToString()] == null)
            {
                return string.Empty;
            }
            return System.Configuration.ConfigurationManager.AppSettings[key.ToString()].ToString();
        }
    }
}