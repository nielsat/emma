using MailPusher.Common.Enums;
using MailPusher.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace MailPusher.Repository.Repositories
{
    public class PublisherRepo
    {
        public int GetTotalRecords()
        {
            int result = 0;
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                result = context.Publishers.Count();
            }
            return result;
        }

        public int GetTotalFilteredRecords(string searchCriteria, PublisherStatus publisherStatuses, bool isPotentiallyCancelled)
        {
            int result = 0;
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                result = AddFilters(context, searchCriteria, publisherStatuses, isPotentiallyCancelled).Count();
            }
            return result;
        }

        protected IQueryable<Publisher> AddFilters(MailPusherDBContext context, string searchCriteria, PublisherStatus publisherStatuses, bool isPotentiallyCancelled)
        {
            var query = context.Publishers.Select(x => x);
            if (publisherStatuses != 0)
            {
                query = query.Where(x => (x.Status & publisherStatuses) > 0);
            }
            if (!string.IsNullOrEmpty(searchCriteria))
            {
                query = query.Where(x => x.Name.Contains(searchCriteria));
            }
            if (isPotentiallyCancelled)
            {
                var date = DateTime.Now.AddDays(-30);
                query = query.Where(x => x.LastReceivedEmail < date);
            }
            return query;
        }

        public List<Publisher> GetPuglishers(int start, int length, string searchCriteria, PublisherStatus publisherStatuses, bool isPotentiallyCancelled)
        {
            List<Publisher> result = new List<Publisher>();
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                var query = AddFilters(context, searchCriteria,publisherStatuses,isPotentiallyCancelled);
                
                var tmpResult = query.Include(x => x.NACE).OrderBy(x => x.ID).Skip(start).Take(length);
                result = tmpResult.ToList();
            }
            return result;
        }

        public Publisher Create(Publisher publisher)
        {
            publisher.Created = DateTime.Now;
            publisher.Updated = DateTime.Now;
            Publisher result = new Publisher();
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                result = context.Publishers.Add(publisher);
                context.SaveChanges();
            }
            return result;
        }

        public bool ChangePublisherStatus(PublisherStatus status, int id, string userId)
        {
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                Publisher result = context.Publishers.FirstOrDefault(x=>x.ID==id);
                result.Status = status;
                result.UpdaterId = userId;
                result.Updated = DateTime.Now;
                context.Entry(result).State = EntityState.Modified;
                context.SaveChanges();
            }
            return true;
        }

        public Publisher GetPublisher(int publisherId)
        {
            Publisher result = new Publisher();
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                result = context.Publishers.Include(x=>x.NACE).FirstOrDefault(x => x.ID == publisherId);
            }
            return result;
        }

        public Publisher GetFirstPublisherByStatus(PublisherStatus status)
        {
            Publisher result = new Publisher();
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                result = context.Publishers.Include(x => x.NACE).FirstOrDefault(x => x.Status == status);
            }
            return result;
        }

        public Publisher GetFirstPublisherByCountryAndStatus(string countryCode, PublisherStatus status)
        {
            Publisher result = new Publisher();
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                result = context.Publishers.Include(x => x.NACE).FirstOrDefault(x => x.Language == countryCode&& x.Status==status);
            }
            return result;
        }

        public Publisher GetPublisherByCountryAndStatus(string countryCode, PublisherStatus status, Direction direction, int publisherId)
        {
            Publisher result = new Publisher();
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                var query = context.Publishers.Include(x => x.NACE);
                if (direction == Direction.Next)
                {
                    query = query.Where(x => x.ID > publisherId);
                }

                if (direction == Direction.Previous)
                {
                    query = query.Where(x => x.ID < publisherId);
                }
                result = query.FirstOrDefault(x => x.Language == countryCode && x.Status == status);
            }
            return result;
        }

        public Publisher UpdateLastReceivedEmailDate(int publisherID)
        {
            Publisher result = new Publisher();
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                result = context.Publishers.FirstOrDefault(x => x.ID == publisherID);
                result.LastReceivedEmail = DateTime.Now;
                context.Entry(result).State = EntityState.Modified;
                context.SaveChanges();
            }
            return result;
        }
    }
}
