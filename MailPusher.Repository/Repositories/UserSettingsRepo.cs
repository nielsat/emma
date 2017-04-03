using MailPusher.Repository.Models;
using System.Linq;
using System;
using System.Data.Entity;

namespace MailPusher.Repository.Repositories
{
    public class UserSettingsRepo
    {
        public UserSettings GetUserSettings(string userId)
        {
            UserSettings result = new UserSettings();
            using (var context = new MailPusherDBContext())
            {
                result = context.UserSettings.FirstOrDefault(x => x.UserId == userId);
            }
            return result;
        }

        public UserSettings Create(string userId, string language)
        {
            UserSettings result = new UserSettings()
            {
                Language = language,
                UserId = userId
            };
            using (var context = new MailPusherDBContext())
            {
                result = context.UserSettings.Add(result);
                context.SaveChanges();
            }
            return result;
        }

        public UserSettings Update(string userId, string language)
        {
            UserSettings result = new UserSettings();
            using (var context = new MailPusherDBContext())
            {
                result = context.UserSettings.FirstOrDefault(x => x.UserId == userId);
                if (result.Language != language)
                {
                    result.Language = language;
                    context.Entry(result).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            return result;
        }
    }
}
