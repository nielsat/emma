using MailPusher.Models;
using MailPusher.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailPusher.Helpers
{
    public class NACEHelper
    {
        public List<NACE> GetAll()
        {
            NACERepo repo = new NACERepo();
            return Map(repo.GetAllRecords());
        }

        public List<NACE> Map(List<Repository.Models.NACE> naces)
        {
            List<NACE> result = new List<NACE>();
            foreach (var item in naces)
            {
                result.Add(Map(item));
            }
            return result;
        }

        public NACE Map(Repository.Models.NACE nace)
        {
            return new NACE()
            {
                ID = nace.ID,
                Description = nace.Description
            };
        }
    }
}