﻿using MailPusher.Common.Enums;
using MailPusher.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Repository.Repositories
{
    public class EmailRepo
    {
        public Email GetFirstPublisherEmail(Common.Enums.PublisherStatus status)
        {
            Email result = new Email();
            using (var context = new MailPusherDBContext())
            {
                var publisher = context.Publishers.FirstOrDefault(x => x.Status == status);
                if (publisher != null)
                {
                    result = context.Emails.OrderBy(x=>x.ID).FirstOrDefault(x => x.PublisherID == publisher.ID);
                }
            }
            return result;
        }
        public Email AddEmail(Common.Models.Email newEmail)
        {
            Email result = new Email();
            Email email = new Email()
            {
                PublisherID = newEmail.PublisherID,
                ReceivedGMT = newEmail.ReceivedGMT,
                SenderAddress = newEmail.SenderAddress,
                SenderName = newEmail.SenderName,
                SubjectLine = newEmail.SubjectLine,
                Copy = newEmail.Copy
            };
            using (var context = new MailPusherDBContext())
            {
                result = context.Emails.Add(email);
                context.SaveChanges();

                context.EmailsRawData.Add(new EmailRawData()
                {
                    HTML = newEmail.HTML,
                    HTMLText = newEmail.HTMLText,
                    Text = newEmail.Text,
                    EmailID = result.ID
                });

                foreach (var header in newEmail.EmailHeaders)
                {
                    context.EmailRawHeaders.Add(new EmailRawHeader()
                    {
                        HeaderName = header.HeaderName,
                        HeaderValue = header.HeaderValue,
                        EmailID = result.ID
                    });
                }
                context.SaveChanges();
            }
            return result;
        }

        public List<Email> GetPublisherEmails(int start, int length, string searchCriteria, int publisherID, DateTime? from, DateTime? to)
        {
            List<Email> result = new List<Email>();
            using (var context = new MailPusherDBContext())
            {
                var query = AddFilters(context, searchCriteria, publisherID, from, to);
                result = query.OrderBy(x => x.ID).Skip(start).Take(length).ToList();
            }
            return result;
        }

        protected IQueryable<Email> AddFilters(MailPusherDBContext context, string searchCriteria, int publisherID, DateTime? from, DateTime? to) {
            var query = context.Emails.Where(x => x.PublisherID == publisherID);
            if (!string.IsNullOrEmpty(searchCriteria))
            {
                query = query.Where(x => x.SubjectLine.Contains(searchCriteria));
            }
            if (from.HasValue)
            {
                query = query.Where(x => x.ReceivedGMT > from.Value);
            }
            if (to.HasValue)
            {
                query = query.Where(x => x.ReceivedGMT < to.Value);
            }
            return query;
        }

        public int GetTotalRecords(int publisherID)
        {
            int result = 0;
            using (var context = new MailPusherDBContext())
            {
                result = context.Emails.Where(x=>x.PublisherID==publisherID).Count();
            }
            return result;
        }

        public int GetTotalFilteredRecords(string searchText,int publisherID, DateTime? from, DateTime? to)
        {
            int result = 0;
            using (var context = new MailPusherDBContext())
            {
                var query = AddFilters(context, searchText, publisherID, from, to);
                result = query.Count();
            }
            return result;
        }

        public EmailRawData GetEmailBody(int emailId)
        {
            EmailRawData result = new EmailRawData();
            using (var context = new MailPusherDBContext())
            {
                result = context.EmailsRawData.FirstOrDefault(x => x.EmailID == emailId);
            }
            return result;
        }

        public Email GetEmailByPublisherCountryAndStatus(string countryCode, PublisherStatus status)
        {
            Email result = new Email();
            using (var context = new MailPusherDBContext())
            {
                result = (from p in context.Publishers
                          join e in context.Emails on p.ID equals e.PublisherID
                          where p.Status == status && p.Language == countryCode
                          select e).FirstOrDefault();
            }
            return result;
        }
        public Email GetNextEmailByPublisherCountryAndStatus(string countryCode, PublisherStatus status, int publisherId)
        {
            Email result = new Email();
            using (var context = new MailPusherDBContext())
            {
                result = (from p in context.Publishers
                          join e in context.Emails on p.ID equals e.PublisherID
                          where p.Status == status && p.Language == countryCode && p.ID>publisherId
                          select e).FirstOrDefault();
            }
            return result;
        }

        public Email GetPrevEmailByPublisherCountryAndStatus(string countryCode, PublisherStatus status, int publisherId)
        {
            Email result = new Email();
            using (var context = new MailPusherDBContext())
            {
                result = (from p in context.Publishers
                          join e in context.Emails on p.ID equals e.PublisherID
                          where p.Status == status && p.Language == countryCode && p.ID<publisherId
                          select e).FirstOrDefault();
            }
            return result;
        }
    }
}
