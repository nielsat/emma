using MailPusher.Common.Enums;
using MailPusher.Common.Helpers;
using MailPusher.Models;
using MailPusher.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailPusher.Helpers
{
    public class EmailHelper
    {
        public Email GetEmail(int emailID)
        {
            EmailRepo repo = new EmailRepo();
            return Map(repo.Get(emailID));
        }
        public List<Email> GetEmails(int start, int length, string searchCriteria, int publisherID, string from, string to, List<Common.Models.SortColumn> sorting, bool addPublisherName = false)
        {
            EmailRepo repo = new EmailRepo();
            var result = Map(repo.GetPublisherEmails(start, length, searchCriteria, publisherID, GetDate(from), GetDate(to), sorting));
            if (addPublisherName)
            {
                var publisherIds = result.Select(x => x.PublisherID).ToList();
                PublisherHelper helper = new PublisherHelper();
                var publishers = helper.GetPublishersName(publisherIds);
                foreach (var item in result)
                {
                    var emailPublisher = publishers.FirstOrDefault(x => x.ID == item.PublisherID);
                    if (emailPublisher != null)
                    {
                        item.PublisherName = emailPublisher.Name;
                    }
                }
            }
            return result;
        }
        protected DateTime? GetDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return null;
            }
            return Convert.ToDateTime(date);
        }
        public List<Email> Map(List<Repository.Models.Email> emails)
        {
            List<Email> result = new List<Email>();
            foreach (var item in emails)
            {
                result.Add(Map(item));
            }
            return result;
        }

        public Email GetFirstPublisherEmail(PublisherStatus status)
        {
            EmailRepo repo = new EmailRepo();
            return Map(repo.GetFirstPublisherEmail(status));
        }

        public Email GetFirstPublisherEmail(PublisherStatus status, int publisherId)
        {
            EmailRepo repo = new EmailRepo();
            return Map(repo.GetFirstPublisherEmail(status, publisherId));
        }

        public Email Map(Repository.Models.Email email, string language = "")
        {
            if (email == null) {
                return new Email() {
                    Language = language
                };
            }
            return new Email()
            {
                Copy = email.Copy,
                ID = email.ID,
                PublisherID = email.PublisherID,
                ReceivedGMT = FormatHelper.ConvertDateToString(email.ReceivedGMT),
                ShortReceivedGMT = FormatHelper.ConvertDateToShortFullDateString(email.ReceivedGMT),
                SenderAddress = email.SenderAddress,
                SenderName = email.SenderName,
                SubjectLine = email.SubjectLine,
                Language = language
            };
        }

        public int GetTotalRecords(int publisherID)
        {
            EmailRepo repo = new EmailRepo();
            return repo.GetTotalRecords(publisherID);
        }

        public int GetTotalFilteredRecords(string searchText, int publisherID, string from, string to)
        {
            EmailRepo repo = new EmailRepo();
            return repo.GetTotalFilteredRecords(searchText, publisherID, GetDate(from), GetDate(to));
        }

        public string GetEmailBody(int EmailId)
        {
            EmailRepo repo = new EmailRepo();
            var emailData = repo.GetEmailBody(EmailId);
            if (emailData == null)
            {
                return string.Empty;
            }
            return string.IsNullOrEmpty(emailData.HTML) ? emailData.Text : emailData.HTML;
        }

        public Email GetEmailByPublisherCountryAndStatus(string countryCode, PublisherStatus status)
        {
            EmailRepo repo = new EmailRepo();
            return Map(repo.GetEmailByPublisherCountryAndStatus(countryCode, status), countryCode); ;
        }

        public Email GetNextEmailByPublisherCountryAndStatus(string countryCode, PublisherStatus status, int publisherId)
        {
            EmailRepo repo = new EmailRepo();
            return Map(repo.GetNextEmailByPublisherCountryAndStatus(countryCode, status, publisherId), countryCode);
        }

        public Email GetPrevEmailByPublisherCountryAndStatus(string countryCode, PublisherStatus status, int publisherId)
        {
            EmailRepo repo = new EmailRepo();
            return Map(repo.GetPrevEmailByPublisherCountryAndStatus(countryCode, status, publisherId), countryCode);
        }
    }
}