using OnlineShop.Models.Db.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.ManageShopModels
{
	/// <summary>
	/// View Model for Product DB Object
	/// </summary>
	public class ProductView
	{
		public const byte MaxNameLength = 200;

		public int PrCatId {get;set;}

		private Category category;
		private Description description;
		private List<Image> images;
		private string name;
		private double price;
		private int count;
		
		/// <summary>
		/// Count of some Product
		/// </summary>
		public int Count
		{
			get
			{
				return count;
			}
			set
			{
				count = value;
			}
		}

		/// <summary>
		/// Price of our Product
		/// </summary>
		public double Price
		{
			get
			{
				return price;
			}
			set
			{
				price = value;
			}
		}

		/// <summary>
		/// Product Name
		/// </summary>
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				//CustomContract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(value));
				//CustomContract.Requires<ArgumentOutOfRangeException>
				//    (value.Length <= 200);
				name = value;
			}
		}

		/// <summary>
		/// Is this Product available now
		/// </summary>
		public bool IsAvailable { get; set; }
		//todo ??? чот мені здаєтся тут повинно бути CategoryView, чи не так йопта?
		public Category Category
		{
			get
			{
				return category ?? new Category();
			}
			set
			{
				category = value;
			}
		}
		
		public Description Description
		{//todo ??? може йолки тут таки достати дескрипшон з нашого файла на диску?
			get { return description ?? null; }
			set
			{
				description = value;
			}
		}

		public List<Image> Images
		{//todo ??? і тут тоже вже фотки а не клас?
			get
			{
				return images ?? new List<Image>();
			}
		}

		public bool AddImage(Image img, bool clearCollection = false)
		{
			if (clearCollection)
				images.Clear();
			images.Add(img);
			return true;
		}

		public bool AddImages(List<Image> img, bool clearCollection = false)
		{
			if (clearCollection)
				images.Clear();
			images.AddRange(img);
			return true;
		}

	}
}