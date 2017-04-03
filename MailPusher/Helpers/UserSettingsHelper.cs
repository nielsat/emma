using MailPusher.Common.Helpers;
using MailPusher.Models;
using MailPusher.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailPusher.Helpers
{
    public class UserSettingsHelper
    {
        public UserSettings GetSettings(string userId)
        {
            UserSettingsRepo repo = new UserSettingsRepo();
            return Map(repo.GetUserSettings(userId));
        }

        public UserSettings Map(MailPusher.Repository.Models.UserSettings settings)
        {
            if (settings == null)
            {
                return null;
            }
            return new UserSettings()
            {
                ID = settings.ID,
                Language = settings.Language,
                UserId = settings.UserId
            };
        }

        public UserSettings CreateUserSettigns(string userId, string language)
        {
            UserSettingsRepo repo = new UserSettingsRepo();
            return Map(repo.Create(userId, language));
        }

        public string GetUserLanguage(string userId)
        {
            string result = string.Empty;
            UserSettings settings = GetSettings(userId);
            if (settings == null)
            {
                result = AppSettingsHelper.GetValueFromAppSettings(Common.Enums.AppSettingsKey.defaultUserLanguage);
                CreateUserSettigns(userId, result);
            }
            else
            {
                result = settings.Language;
            }
            return result;
        }

        public UserSettings Update(string userId, string language)
        {
            UserSettingsRepo repo = new UserSettingsRepo();
            return Map(repo.Update(userId, language));
        }
    }
}