using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db.Tables
{
    public class LinkCategories
    {
        [Key]
        public long LC_Id { get; set; }
        public long Category_Cat_Id { get; set; }
        public long Link_Link_Id { get; set; }
    }
}