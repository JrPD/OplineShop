namespace OnlineShop.Models.Db.Tables
{

	public class ProductCounter
	{
		public long Pr_Id { get; set; }

        public int Pr_Count { get; set; }

        public virtual Product Product { get; set; }
    }

}