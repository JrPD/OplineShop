using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.ManageShopModels.Views
{
    public class CategoryViewSmpl
    {
        [Display(Name = "Level")]
        public byte Level { get; set; }
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        [Display(Name = "Parent Category Name")]
        public string ParentName { get; set; }

        public string ImagePath { get; set; }

        public bool HasSubCategories { get; set;}
    }
}