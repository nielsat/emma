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
    }
}
