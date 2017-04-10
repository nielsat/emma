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
    public class PublisherHelper
    {
        public int GetTotalRecords() {
            PublisherRepo repo = new PublisherRepo();
            return repo.GetTotalRecords();
        }

        public int GetTotalFilteredRecords(string searchCriteria, PublisherStatus statuses, bool isPotentiallyCancelled) {
            PublisherRepo repo = new PublisherRepo();
            return repo.GetTotalFilteredRecords(searchCriteria, statuses, isPotentiallyCancelled);
        }

        public List<Publisher> GetPublishers(int start, int length, string searchCriteria, PublisherStatus publisherStatuses, bool isPotentiallyCancelled)
        {
            PublisherRepo repo = new PublisherRepo();
            return Map(repo.GetPuglishers(start, length, searchCriteria, publisherStatuses, isPotentiallyCancelled));
        }

        public List<Publisher> GetPublishersWithStats(int start, int length, string searchCriteria, PublisherStatus publisherStatuses, bool isPotentiallyCancelled)
        {
            PublisherRepo repo = new PublisherRepo();
            List<Publisher> publishers = Map(repo.GetPuglishers(start, length, searchCriteria, publisherStatuses, isPotentiallyCancelled));
            var publishersStats = repo.GetStats(publishers.Select(x => x.ID).ToList());
            foreach (var publisher in publishers)
            {
                var currentPublisherStats = publishersStats.FirstOrDefault(x => x.PublisherId == publisher.ID);
                publisher.ReceivedEmails = currentPublisherStats == null ? 0 : currentPublisherStats.ReceivedEmails;
            }
            return publishers;
        }

        public Publisher GetPublisher(int publisherId) {
            PublisherRepo repo = new PublisherRepo();
            return Map(repo.GetPublisher(publisherId));
        }

        public Publisher GetFirstPublisherByStatus(PublisherStatus publisherStatus)
        {
            PublisherRepo repo = new PublisherRepo();
            return Map(repo.GetFirstPublisherByStatus(publisherStatus));
        }

        public Publisher GetFirstPublisherByCountryAndStatus(string countryCode, PublisherStatus publisherStatus)
        {
            PublisherRepo repo = new PublisherRepo();
            return Map(repo.GetFirstPublisherByCountryAndStatus(countryCode, publisherStatus), countryCode); ;
        }

        public Publisher GetPublisherByCountryAndStatus(string countryCode, PublisherStatus status, Direction direction, int publisherId)
        {
            PublisherRepo repo = new PublisherRepo();
            return Map(repo.GetPublisherByCountryAndStatus(countryCode, status, direction, publisherId), countryCode);
        }

        public Publisher AddPublisher(Publisher newPublisher)
        {
            PublisherRepo repo = new PublisherRepo();
            Repository.Models.Publisher dbPublisher = repo.Create(Map(newPublisher));
            return Map(dbPublisher);
        }

        public Publisher Update(Publisher publisher)
        {
            PublisherRepo repo = new PublisherRepo();
            Repository.Models.Publisher dbPublisher = repo.Update(Map(publisher));
            return Map(dbPublisher);
        }

        public List<Publisher> Map(List<Repository.Models.Publisher> sourcePublishers)
        {
            List<Publisher> result = new List<Publisher>();
            foreach(var publisher in sourcePublishers)
            {
                result.Add(Map(publisher));
            }
            return result;
        }

        public bool ChangePublisherStatus(PublisherStatus status, int id, string userId)
        {
            PublisherRepo repo = new PublisherRepo();
            return repo.ChangePublisherStatus(status, id, userId);
        }

        public Repository.Models.Publisher Map(Publisher publisher)
        {
            return new Repository.Models.Publisher()
            {
                Domain = publisher.Domain,
                ID = publisher.ID,
                Language = publisher.Language,
                NACEID = publisher.NACEID,
                Name = publisher.Name,
                Status = publisher.Status,
                CreatorId = publisher.CreatorId,
                UpdaterId=publisher.UpdaterId
            };
        }

        public bool Delete(int publisherID)
        {
            PublisherRepo repo = new PublisherRepo();
            return repo.Delete(publisherID);
        }

        public Publisher Map(Repository.Models.Publisher publisher, string language ="")
        {
            if (publisher == null)
            {
                return new Publisher()
                {
                    Language = language
                };
            }
            return new Publisher()
            {
                Domain = publisher.Domain,
                ID = publisher.ID,
                Language = publisher.Language,
                NACEID = publisher.NACEID,
                Name = publisher.Name,
                Status = publisher.Status,
                Category = publisher.NACE==null? string.Empty: publisher.NACE.Description,
                CreatorId = publisher.CreatorId,
                UpdaterId = publisher.UpdaterId,
                LastReceivedEmail = publisher.LastReceivedEmail.HasValue? FormatHelper.ConvertDateToString(publisher.LastReceivedEmail.Value) :"",
                FormatedStatus = FormatHelper.GetFormtedStatus(publisher.Status, publisher.StatusChanged)
            };
        }
    }
}