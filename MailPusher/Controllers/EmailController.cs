using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using MailPusher.Common.Enums;
using MailPusher.Common.Models;
using MailPusher.Helpers;
using Microsoft.AspNet.Identity;
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
            List<SortColumn> sorting = GetSortColumns(request);
            
            EmailHelper helper = new EmailHelper();

            var filteredData = helper.GetEmails(request.Start, request.Length, request.Search.Value, publisherID, from, to, sorting);

            var response = DataTablesResponse.Create(request, 
                helper.GetTotalRecords(publisherID), 
                helper.GetTotalFilteredRecords(request.Search.Value, publisherID, from, to), 
                filteredData);

            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        private List<SortColumn> GetSortColumns(IDataTablesRequest request)
        {
            List<SortColumn> sorting = new List<SortColumn>();
            foreach (var column in request.Columns)
            {
                if (column.Sort != null)
                {
                    if (column.Field == "receivedGMT" || column.Field == "subjectLine" || column.Field == "shortReceivedGMT")
                    {
                        sorting.Add(new SortColumn()
                        {
                            SortColumnName = column.Field == "receivedGMT" || column.Field == "shortReceivedGMT" ? SortColumnName.ReceivedGMT : SortColumnName.SubjectLine,
                            SortingOrder = column.Sort.Direction == SortDirection.Ascending ? SortingOrder.Ascending : SortingOrder.Descending
                        });
                    }
                }
            }
            return sorting;
        }

        public ActionResult GetEmailBody(int emailID)
        {
            EmailHelper helper = new EmailHelper();
            return Content(helper.GetEmailBody(emailID));
        }

        public ActionResult GetFirstPublisherEmail(string countryCode, PublisherStatus status)
        {
            //TODO move this to user settings, don't make during search!!
            UserSettingsHelper usHelper = new UserSettingsHelper();
            usHelper.Update(User.Identity.GetUserId(), countryCode);
            //TODO move this to user settings, don't make during search!!
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

        public ActionResult GetPublisherEmailsTotal(int publisherId)
        {
            EmailHelper helper = new EmailHelper();
            return Json(helper.GetTotalRecords(publisherId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Latest()
        {
            return View();
        }

        public ActionResult GetLatest(IDataTablesRequest request)
        {
            List<SortColumn> sorting = GetSortColumns(request);
            EmailHelper helper = new EmailHelper();

            var filteredData = helper.GetEmails(request.Start, request.Length, request.Search.Value, 0, string.Empty, string.Empty, sorting, true);

            var response = DataTablesResponse.Create(request,
                helper.GetTotalRecords(0),
                helper.GetTotalFilteredRecords(request.Search.Value, 0, string.Empty, string.Empty),
                filteredData);

            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }
    }
}