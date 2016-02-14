using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace AchieveMaster.Models
{
    public class Request
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string StudentLocation { get; set; }
        [Required]
        public string MeetLocation { get; set; }
        public string Expired { get; set; }
        [Required]
        public string PayRate { get; set; }
        public string Image { get; set; } //must be a URL for now
        public string UserID { get; set; }
    }
}