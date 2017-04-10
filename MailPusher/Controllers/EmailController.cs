﻿using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MailPusher.Common.Enums;
using MailPusher.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MailPusher.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index(int id)
        {
            EmailHelper helper = new EmailHelper();
            return View(helper.GetEmail(id));
        }

        public ActionResult Get(IDataTablesRequest request, int publisherID, string from, string to)
        {
            EmailHelper helper = new EmailHelper();

            var filteredData = helper.GetEmails(request.Start, request.Length, request.Search.Value, publisherID, from, to);

            var response = DataTablesResponse.Create(request, 
                helper.GetTotalRecords(publisherID), 
                helper.GetTotalFilteredRecords(request.Search.Value, publisherID, from, to), 
                filteredData);

            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmailBody(int emailID)
        {
            EmailHelper helper = new EmailHelper();
            return Content(helper.GetEmailBody(emailID));
        }

        public ActionResult GetFirstPublisherEmail(string countryCode, PublisherStatus status)
        {
            EmailHelper helper = new EmailHelper();
            return Json(helper.GetEmailByPublisherCountryAndStatus(countryCode, status),JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNextPublisherEmail(string countryCode, PublisherStatus status, int publisherId)
        {
            EmailHelper helper = new EmailHelper();
            return Json(helper.GetNextEmailByPublisherCountryAndStatus(countryCode, status, publisherId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPrevPublisherEmail(string countryCode, PublisherStatus status, int publisherId)
        {
            EmailHelper helper = new EmailHelper();
            return Json(helper.GetPrevEmailByPublisherCountryAndStatus(countryCode, status, publisherId), JsonRequestBehavior.AllowGet);
        }
    }
}