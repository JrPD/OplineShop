using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.ManageShopModels.Views
{
    public class PropertyView
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long Link_Id { get; set; }

        public bool IsChanged { get; set; }

        public bool IsNew { get; set; }
    }
}