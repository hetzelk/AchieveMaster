using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace AchieveMaster.Models
{
    public class CategoriesList
    {
        public IEnumerable<SelectListItem> AllCategories { get; set; }
        
        public string selectedCategory { get; set; }
    }
}