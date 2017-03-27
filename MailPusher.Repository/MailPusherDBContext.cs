using MailPusher.Repository.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailPusher.Repository
{
    public class MailPusherDBContext: IdentityDbContext<AppUser>
    {
        public static MailPusherDBContext Create()
        {
            return new MailPusherDBContext();
        }
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailRawData> EmailsRawData { get; set; }
        public DbSet<EmailRawHeader> EmailRawHeaders { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<NACE> NACEs { get; set; }
    }
}
