using OnlineShop.Models.Db.Tables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Models.ManageShopModels.Views
{
    /// <summary>
    /// View Model for Product DB Object
    /// </summary>
    public class ProductView : IValidatableObject
    {
        public const byte MaxNameLength = 200;
        public const byte MinCount = 0;
        public const double MinPrice = 0.01;
        public const byte MaxCount = 255;
        public const double MaxPrice = 999999.99;

        [Display(Name = "Категорія")]
        public long SelectedCategoryId { get; set; }

        public List<SelectListItem> CategoryList { get; set; }

        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Res),
            ErrorMessageResourceName = "FieldCantBeNull")]
        [Display(Name = "Кількість")]
        public int Count { get; set; }

        [Required(ErrorMessageResourceType = typeof(Res),
            ErrorMessageResourceName = "FieldCantBeNull")]
        [Display(Name = "Назва продукту")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Res),
            ErrorMessageResourceName = "FieldCantBeNull")]
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

        public ProductView()
        {
            CategoryList = new List<SelectListItem>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enumerable.Range(MinCount, MaxCount).Contains(Count))
                yield return new ValidationResult(string.Format(Res.IncorrectInput,
                    "Кількість", Count.ToString()),
                    new[] { "Count" });

            if (Price < MinPrice || Price > MaxPrice)
                yield return new ValidationResult(string.Format(Res.IncorrectInput,
                    "Ціна", Price.ToString()),
                    new[] { "Price" });

            if (Name.Length > MaxNameLength)
                yield return new ValidationResult(
                    string.Format(Res.IncorrectLength, MaxNameLength, Name.Length), new[] { "Name" });
            Category cat = App.Rep.Select<Category>()
                      .FirstOrDefault(c => c.Cat_Id == SelectedCategoryId);

            if (cat != null)
                Category = cat;
            if (Category != null && Category.Cat_Id != 0)
            {
                if (Category.Cat_HasChild)
                {
                    yield return new ValidationResult(Res.NotLastLevelCategory, new[] { "Category" });
                }
            }
            else
            {
                yield return new ValidationResult(Res.NotAvailableCategory, new[] { "Category" });
            }

            var sameNameProducts = App.Rep.Select<Product>().Where(p => p.Pr_Name.ToLower() == Name.ToLower());
            if (sameNameProducts != null && sameNameProducts.Count() > 0)
            {
                yield return new ValidationResult(
                    string.Format(Res.SameProductName), new[] { "Name" });
            }
        }
    }
}