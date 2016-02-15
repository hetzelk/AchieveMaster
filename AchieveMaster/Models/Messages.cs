using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AchieveMaster.Models
{
    public class Messages
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string FirstPerson { get; set; }
        public string SecondPerson { get; set; }
        public string FirstDiscontinued { get; set; }
        public string SecondDiscontinued { get; set; }
        public string Conversation { get; set; }
        public string Expired { get; set; }
        public bool NewMessage { get; set; }
        public bool blocked { get; set; }
        public string FirstPersonName { get; set; }
        public string SecondPersonName { get; set; }
        public string UserID { get; internal set; }
    }
}