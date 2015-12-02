using OnlineShop.Models.Db.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.ManageShopModels.Views
{
    /// <summary>
    /// View Model for Product DB Object
    /// </summary>
    public class ProductView : IValidatableObject
    {
        public const byte MaxNameLength = 200;
        public const byte MinCountOrPrice = 0;
        public const byte MaxCount = 255;
        public const int MaxPrice = 999999;

        [Display(Name = "Категорія")]
        public long CatId { get; set; }

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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enumerable.Range(MinCountOrPrice, MaxCount).Contains(Count))
                yield return new ValidationResult(string.Format(Res.IncorrectInput,
                    "Кількість", Count.ToString()),
                    new[] { "Count" });
            if (!Enumerable.Range(MinCountOrPrice, MaxPrice).Contains(Convert.ToInt32(Price + .99)))
                yield return new ValidationResult(string.Format(Res.IncorrectInput,
                    "Ціна", Price.ToString()),
                    new[] { "Price" });
            if (Name.Length > MaxNameLength)
                yield return new ValidationResult(
                    string.Format(Res.IncorrectLength, MaxNameLength, Name.Length), new[] { "Name" });
            Category cat = App.Rep.Select<Category>()
                      .FirstOrDefault(c => c.Cat_Id == CatId);
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
        }
    }
}