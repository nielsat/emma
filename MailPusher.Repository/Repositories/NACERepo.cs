using MailPusher.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Repository.Repositories
{
    public class NACERepo
    {
        public List<NACE> GetAllRecords()
        {
            List<NACE> result = new List<NACE>();
            using (var context = new MailPusherDBContext())
            {
                result = context.NACEs.ToList();
            }
            return result;
        }

        public List<NACE> GetPublisherNaces()
        {
            List<NACE> result = new List<NACE>();
            using (var context = new MailPusherDBContext())
            {
                var query = (from nace in context.NACEs
                             join publisher in context.Publishers
                             on nace.ID equals publisher.NACEID
                             group nace by new { nace.ID, nace.Description }
                             into gr
                             select new
                             {
                                 ID = gr.Key.ID,
                                 Description = gr.Key.Description
                             }).OrderBy(x=>x.Description);
                foreach (var item in query)
                {
                    result.Add(new NACE()
                    {
                        ID = item.ID,
                        Description = item.Description
                    });
                }
            }
            return result;
        }
    }
}
