using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AchieveMaster.Models
{
    public class Reviews
    {
        public int ID { get; set; }
        public string Rating { get; set; } //0-10
        public string UserID { get; set; }
        public string Review { get; set; }
    }
}