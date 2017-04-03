using MailPusher.Common.Enums;
using MailPusher.Common.Helpers;
using MailPusher.Helpers;
using MailPusher.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MailPusher.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        // GET: Subscription
        public ActionResult Index(int? publisherId)
        {
            var publisher = GetPublisher(publisherId);

            PublisherEmail subscription = GetPublisherEmail(publisher);
            return View(subscription);
        }

        protected PublisherEmail GetPublisherEmail(Publisher publisher)
        {
            if (publisher == null)
            {
                return new PublisherEmail();
            }
            string emailTemplate = AppSettingsHelper.GetValueFromAppSettings(AppSettingsKey.emailReceiverTemplate);
            
            return new PublisherEmail()
            {
                Domain = publisher.Domain,
                PublisherId = publisher.ID.ToString(),
                SubscriberEmail = string.Format(emailTemplate, publisher.ID),
                PublisherName = publisher.Name,
                Language = publisher.Language
            };
        }

        protected Publisher GetPublisher(int? publisherId)
        {
            Publisher result = new Publisher();
            PublisherHelper helper = new PublisherHelper();

            if (!publisherId.HasValue)
            {
                UserSettingsHelper usHelper = new UserSettingsHelper();
                string userLanguage = usHelper.GetUserLanguage(User.Identity.GetUserId());
                result = helper.GetFirstPublisherByCountryAndStatus(userLanguage, PublisherStatus.None);
            }
            else {
                result = helper.GetPublisher(publisherId.Value);
            }
            return result;
        }

        public ActionResult GetFirstPublisher(string countryCode, PublisherStatus status)
        {
            //TODO move this to user settings, don't make during search!!
            UserSettingsHelper usHelper = new UserSettingsHelper();
            usHelper.Update(User.Identity.GetUserId(), countryCode);
            //TODO move this to user settings, don't make during search!!
            PublisherHelper helper = new PublisherHelper();
            Publisher publisher = helper.GetFirstPublisherByCountryAndStatus(countryCode, status);
            return Json(GetPublisherEmail(publisher), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNextPublisher(string countryCode,int publisherId, PublisherStatus status)
        {
            PublisherHelper helper = new PublisherHelper();
            Publisher publisher = helper.GetPublisherByCountryAndStatus(countryCode, status, Direction.Next, publisherId);
            return Json(GetPublisherEmail(publisher), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPrevPublisher(string countryCode, int publisherId, PublisherStatus status)
        {
            PublisherHelper helper = new PublisherHelper();
            Publisher publisher = helper.GetPublisherByCountryAndStatus(countryCode, status, Direction.Previous, publisherId);
            return Json(GetPublisherEmail(publisher), JsonRequestBehavior.AllowGet);
        }
    }
}