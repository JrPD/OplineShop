using System.ComponentModel.DataAnnotations;
using System.Web;
using OnlineShop.Models.Db.Tables;

namespace OnlineShop.Models.ManageShopModels.Views
{
	/// <summary>
	/// View Model for Product DB Object
	/// </summary>
	public class ProductView
	{
		public const byte MaxNameLength = 200;

		[Display(Name = "Категорія")]
		public long CatId { get; set; }

		public long Id { get; set; }

		[Display(Name = "Кількість")]
		public int Count { get; set; }

		[Required(ErrorMessageResourceType = typeof(Res),
			ErrorMessageResourceName = "NameCantBeNull")]
		[Display(Name = "Назва продукту")]
		public string Name { get; set; }

		[Display(Name = "Ціна")]
		public double Price { get; set; }

		[Display(Name = "Доступність")]
		public bool IsAvailable { get; set; }

		[Display(Name = "Категорія")]
		public Category Category { get; set; }

		[Display(Name = "Зображення категорії")]
		public HttpPostedFileBase[] ImgFiles { get; set; }

		[Display(Name = "Опис продукту")]
		public Description Description { get; set; }

		//private List<Image> images;
		//public bool AddImage(Image img, bool clearCollection = false)
		//{
		//	if (clearCollection)
		//		images.Clear();
		//	images.Add(img);
		//	return true;
		//}

		//public bool AddImages(List<Image> img, bool clearCollection = false)
		//{
		//	if (clearCollection)
		//		images.Clear();
		//	images.AddRange(img);
		//	return true;
		//}
	}
}