using MailPusher.Common.Enums;
using MailPusher.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using MailPusher.Common.Models;
using MailPusher.Common.Helpers;
using MailPusher.Common.Models.Reports;
using System.Data.Entity.Core.Objects;

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
                var date = DateTime.UtcNow.AddDays(-30);
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
            publisher.Created = DateTime.UtcNow;
            publisher.Updated = DateTime.UtcNow;
            Publisher result = new Publisher();
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                result = context.Publishers.Add(publisher);
                context.SaveChanges();
            }
            return result;
        }

        public Publisher Update(Publisher publisher)
        {
            Publisher result = new Publisher(); 
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                Publisher dbPublisher = context.Publishers.FirstOrDefault(x => x.ID == publisher.ID);
                if (dbPublisher != null)
                {
                    dbPublisher.Domain = publisher.Domain;
                    if (publisher.Status != dbPublisher.Status)
                    {
                        dbPublisher.StatusChanged = DateTime.UtcNow;
                    }
                    dbPublisher.Status = publisher.Status;
                    dbPublisher.Language = publisher.Language;
                    dbPublisher.NACEID = publisher.NACEID;
                    dbPublisher.Name = publisher.Name;

                    dbPublisher.Updater = publisher.Updater;
                }
                
                context.Entry(dbPublisher).State = EntityState.Modified;
                context.SaveChanges();

                result = context.Publishers.Include(x => x.NACE).FirstOrDefault(x => x.ID == publisher.ID); ;
            }
            return result;
        }

        public bool ChangePublisherStatus(PublisherStatus status, int id, string userId)
        {
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                Publisher result = context.Publishers.FirstOrDefault(x=>x.ID==id);
                if (status != result.Status)
                {
                    result.StatusChanged = DateTime.UtcNow;
                }
                result.Status = status;
                result.UpdaterId = userId;
                result.Updated = DateTime.UtcNow;
                context.Entry(result).State = EntityState.Modified;
                context.SaveChanges();
            }
            return true;
        }

        public bool Delete(int publisherID)
        {
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                Publisher dbPublisher = new Publisher() { ID = publisherID };
                context.Publishers.Attach(dbPublisher);
                context.Publishers.Remove(dbPublisher);
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
                var query = context.Publishers.Include(x => x.NACE);
                if (!string.IsNullOrEmpty(countryCode))
                {
                    query = query.Where(x => x.Language == countryCode);
                }
                result = query.FirstOrDefault(x => x.Status == status);
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

                if (!string.IsNullOrEmpty(countryCode))
                {
                    query = query.Where(x => x.Language == countryCode);
                }

                result = query.FirstOrDefault(x => x.Status == status);
            }
            return result;
        }

        public Publisher UpdateLastReceivedEmailDate(int publisherID)
        {
            Publisher result = new Publisher();
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                result = context.Publishers.FirstOrDefault(x => x.ID == publisherID);
                result.LastReceivedEmail = DateTime.UtcNow;
                context.Entry(result).State = EntityState.Modified;
                context.SaveChanges();
            }
            return result;
        }

        public List<PublisherStats> GetStats(List<int> publisherIds)
        {
            List<PublisherStats> result = new List<PublisherStats>();
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                var resultQuery = context.Emails.Where(x => publisherIds.Contains(x.PublisherID)).GroupBy(x => x.PublisherID).Select(group => new PublisherStats()
                {
                    PublisherId = group.Key,
                    ReceivedEmails = group.Count()
                });
                result.AddRange(resultQuery);
            }
            return result;
        }

        public Metrics GetMetrics()
        {
            Metrics result = new Metrics();
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                result.TotalConfirmedSubscriptions = context.Publishers.Where(x => x.Status == PublisherStatus.Confirmed).Count();
                result.TotalSubscriptions = context.Publishers.Where(x => (x.Status & (PublisherStatus.Confirmed | PublisherStatus.Subscribed)) > 0).Count();

                DateRange lastMonthRange = DateRangeHelper.GetLastMonth();
                result.ConfirmedSubscriptionsLastMonth = context.Publishers.Where(x => x.StatusChanged!=null && x.StatusChanged >= lastMonthRange.Start.Date && x.StatusChanged <= lastMonthRange.End.Date && x.Status == PublisherStatus.Confirmed).Count();

                DateRange lastWeekRange = DateRangeHelper.GetLastWeek();
                result.ConfirmedSubscriptionsLastMonth = context.Publishers.Where(x => x.StatusChanged != null && x.StatusChanged >= lastWeekRange.Start.Date && x.StatusChanged <= lastWeekRange.End.Date && x.Status == PublisherStatus.Confirmed).Count();

                DateRange currentMonthRange = DateRangeHelper.GetCurrentMonth();
                result.ConfirmedSubscriptionsLastMonth = context.Publishers.Where(x => x.StatusChanged != null && x.StatusChanged >= currentMonthRange.Start.Date && x.Status == PublisherStatus.Confirmed).Count();

                DateRange currentWeekRange = DateRangeHelper.GetCurrentWeek();
                result.ConfirmedSubscriptionsThisWeek = context.Publishers.Where(x => x.StatusChanged != null && x.StatusChanged >= currentWeekRange.Start.Date && x.Status == PublisherStatus.Confirmed).Count();
            }
            return result;
        }

        public List<TimeChartPoint> GetConfirmedSubscriptionsByDate()
        {
            List<TimeChartPoint> result = new List<TimeChartPoint>();
            using (MailPusherDBContext context = new MailPusherDBContext())
            {
                var query = context.Publishers.Where(x => x.StatusChanged.HasValue && x.Status == PublisherStatus.Confirmed).OrderBy(x=>x.StatusChanged).GroupBy(x => DbFunctions.TruncateTime(x.StatusChanged.Value)).Select(group => new TimeChartPoint()
                {
                    Date = group.Key.Value,
                    Value = group.Count()
                });
                result = query.ToList(); 
            }
            return result;
        }
    }
}
