using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AchieveMaster.Models
{
    public class Request
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string StudentLocation { get; set; }
        public string MeetLocation { get; set; }
        public string Expired { get; set; }
        public string PayRate { get; set; }
        public string Image { get; set; } //must be a URL for now
        public string UserID { get; set; }
    }
}