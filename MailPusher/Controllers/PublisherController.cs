using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MailPusher.Common.Enums;
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
    public class PublisherController : Controller
    {
        // GET: Publisher
        public ActionResult Index()
        {
            var statusList = new List<Tuple<string,int>>();
            foreach (PublisherStatus status in Enum.GetValues(typeof(PublisherStatus)))
            {
                statusList.Add(new Tuple<string, int>(status.ToString(), (int)status));
            }
            ViewBag.StatusList = Newtonsoft.Json.JsonConvert.SerializeObject(statusList);
            return View();
        }

        public ActionResult Get(IDataTablesRequest request, PublisherStatus publisherStatuses, bool isPotentiallyCancelled)
        {
            PublisherHelper helper = new PublisherHelper();
            
            var filteredData = helper.GetPublishersWithStats(request.Start, request.Length, request.Search.Value, publisherStatuses, isPotentiallyCancelled);

            var response = DataTablesResponse.Create(request, helper.GetTotalRecords(), helper.GetTotalFilteredRecords(request.Search.Value, publisherStatuses, isPotentiallyCancelled), filteredData);

            // Easier way is to return a new 'DataTablesJsonResult', which will automatically convert your
            // response to a json-compatible content, so DataTables can read it when received.
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int publisherID)
        {
            PublisherHelper helper = new PublisherHelper();
            
            return View(helper.GetPublisher(publisherID));
        }

        public ActionResult Create()
        {
            InitLists();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Publisher publisher)
        {
            string currentUserId = User.Identity.GetUserId();
            PublisherHelper helper = new PublisherHelper();
            publisher.Status = PublisherStatus.None;
            publisher.CreatorId = currentUserId;
            publisher.UpdaterId = currentUserId;
            try
            {
                helper.AddPublisher(publisher);
            }
            catch (Exception ex)
            {
                throw;
                return View(publisher);
            }
            return Content("ok");
        }

        public ActionResult Edit(int publisherID)
        {
            InitLists();
            PublisherHelper helper = new PublisherHelper();
            return View(helper.GetPublisher(publisherID));
        }

        [HttpPost]
        public ActionResult Edit(Publisher publisher)
        {
            InitLists();
            PublisherHelper helper = new PublisherHelper();
            publisher.UpdaterId = User.Identity.GetUserId();
            helper.Update(publisher);
            return Content("ok");
        }

        [HttpPost]
        public ActionResult ChangePublisherStatus(PublisherStatus status, int id)
        {
            string currentUserId = User.Identity.GetUserId();
            PublisherHelper helper = new PublisherHelper();
            return Json(helper.ChangePublisherStatus(status, id, currentUserId));
        }

        public ActionResult Unconfirmed(int? publisherId)
        {
            EmailHelper helper = new EmailHelper();
            Email result = new Email();
            if (!publisherId.HasValue)
            {
                UserSettingsHelper usHelper = new UserSettingsHelper();
                string userLanguage = usHelper.GetUserLanguage(User.Identity.GetUserId());
                result = helper.GetEmailByPublisherCountryAndStatus(userLanguage, PublisherStatus.Subscribed);
            }
            else
            {
                result = helper.GetFirstPublisherEmail(PublisherStatus.Subscribed, publisherId.Value);
            }
            return View(result);
        }

        private void InitLists() {
            NACEHelper helper = new NACEHelper();
            var naceData = helper.GetAll();
            ViewBag.NACETree = Newtonsoft.Json.JsonConvert.SerializeObject(helper.GenerateTree(naceData));
        }
        [HttpPost]
        public ActionResult Delete(int publisherID)
        {
            PublisherHelper helper = new PublisherHelper();
            helper.Delete(publisherID);
            return Content("ok");
        }
    }
}