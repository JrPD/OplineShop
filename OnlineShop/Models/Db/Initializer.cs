using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.Db
{
	public class Initializer : CreateDatabaseIfNotExists<DbContext>
	{
		protected override void Seed(DbContext context)
		{
			//todo тут дефолтні дані до бази
		}
	}
}