using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db
{
	
	public class Image
	{
		[Key]
		public long Im_Id { get; set; }
		public string Im_Path { get; set; }
	}


}