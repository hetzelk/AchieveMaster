using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AchieveMaster.Models
{
    public class AchieveMasterDB : DbContext
    {
        public AchieveMasterDB() : base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<AchieveMaster.Models.Request> Requests { get; set; }
        public System.Data.Entity.DbSet<AchieveMaster.Models.Messages> Messages { get; set; }

        public System.Data.Entity.DbSet<AchieveMaster.Models.Categories> Categories { get; set; }
    }
}