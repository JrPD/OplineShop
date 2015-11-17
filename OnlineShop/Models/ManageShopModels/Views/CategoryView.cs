using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.ManageShopModels.Views
{
    public class CategoryView : IValidatableObject
    {
        public byte Level { get; set; }
        [Required(ErrorMessageResourceType = typeof(Res),
            ErrorMessageResourceName = "FieldCantBeNull")]
        [Display(Name = "Ім'я категорії")]
        public string Name { get; set; }
        public string ParentName { get; set; }
        public long ParentId { get; set; }
        [Display(Name = "Зображення категорії")]
        public HttpPostedFileBase ImgFile { get; set; }
        public string ImagePath { get; set; }
        public long? ImageId { get; set; }
        public bool HasSubCategories { get; set; }
        public long Id { get; set; }
        public List<LinkView> Properties { get; set; }


        public const byte MinLevel = 1;
        public const byte MaxLevel = 3;
        public const byte MaxNameLength = 200;


        public CategoryView()
        {
            Properties = new List<LinkView>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (!Enumerable.Range(MinLevel, MaxLevel).Contains(Level))
                yield return new ValidationResult(
                    string.Format(Res.IncorrectLevel,Level.ToString()),
                    new[] { "Level" });

            if (Name.Length > MaxNameLength)
                yield return new ValidationResult(
                    string.Format(Res.IncorrectLength, MaxNameLength, Name.Length), new[] { "Name" });
            if (ParentId != CategoryManager.DefaultParentCategoryId)
            {
                if (ParentId <= 0)
                {
                    yield return new ValidationResult(
                        string.Format(Res.IncorrectInput, "parent id", ParentId));
                }
				else if (!App.Rep.Select<Category>()
                        .Any(c => c.Cat_Id == ParentId))
                {
                    yield return new ValidationResult(
                string.Format(Res.IncorrectInput, "Parent Id", ParentId));
                }
            }
            else if (ParentName != null && ParentName.Length != 0)
            {
				var category = App.Rep.Select<Category>()
                    .FirstOrDefault(c => c.Cat_Name == ParentName);
                if (category == null)
                {
                    yield return new ValidationResult(
                        string.Format(Res.CatView_NameNotFound, ParentName));

                }
                else
                {
                    ParentId = category.Cat_Id;
                }
            }
        }
    }
}