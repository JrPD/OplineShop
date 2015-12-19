using OnlineShop.Models.Db.Tables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnlineShop.Models.ManageShopModels.Views
{
    public class PropertyView : IValidatableObject
    {
        //todo Запитати про перевірку Link_id чи необхідна
        public const byte MaxNameLength = 200;

        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Res),
            ErrorMessageResourceName = "FieldCantBeNull")]
        [Display(Name = "Ім'я властивості")]
        public string Name { get; set; }

        [Display(Name = "Група властивостей")]
        public long Link_Id { get; set; }

        public bool IsChanged { get; set; }

        public bool IsNew { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name.Length > MaxNameLength)
                yield return new ValidationResult(
                    string.Format(Res.IncorrectLength, MaxNameLength, Name.Length), new[] { "Name" });

            var sameNameProperties = App.Rep.Select<Property>().Where(p => p.Prop_Name.ToLower() == Name.ToLower());
            if (sameNameProperties != null && sameNameProperties.Count() > 0)
            {
                yield return new ValidationResult(
                    string.Format(Res.SameLinkPropName), new[] { "Name" });
            }
        }
    }
}