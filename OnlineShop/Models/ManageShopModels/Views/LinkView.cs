using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.ManageShopModels.Views
{
    public class LinkView : IComparable<LinkView> 
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsChanged { get; set; }

        public bool IsNew { get; set; }

        public bool Checked { get; set; }       

        public int CompareTo(LinkView other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}