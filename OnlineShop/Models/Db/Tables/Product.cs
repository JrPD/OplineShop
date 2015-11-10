using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db.Tables
{

	public class Product
	{
		public long Pr_Id { get; set; }

		public long Pr_Cat_Id { get; set; }

        public long? Pr_Descr_Id { get; set; }

        public string Pr_Name { get; set; }

		public double Pr_Price { get; set; }

		public virtual  Category Category { get; set; }
		
		public bool Pr_IsAvailable { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }

		public virtual Description Description { get; set; }

        public virtual ICollection<Link> Links { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

        public int Pr_Count { get; set; }

        public Product()
		{
			Images = new HashSet<Image>();    
            Carts = new HashSet<Cart>();
            Links = new HashSet<Link>();
            Properties = new HashSet<Property>();
		}
	}

}