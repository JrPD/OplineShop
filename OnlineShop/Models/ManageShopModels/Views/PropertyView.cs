using OnlineShop.Models.Db.Tables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnlineShop.Models.ManageShopModels.Views
{
    public class PropertyView : IValidatableObject
    {
        public const byte MaxNameLength = 200;
        public const byte MinNameLength = 3;

        [Required]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Res),
            ErrorMessageResourceName = "FieldCantBeNull")]
        [Display(Name = "Ім'я властивості")]
        [StringLength(MaxNameLength,
            MinimumLength = MinNameLength,
            ErrorMessageResourceType = typeof(Res),
            ErrorMessageResourceName = "IncorrectLengthAttributeDefault")]
        public string Name { get; set; }

        [Display(Name = "Група властивостей")]
        public long LinkId { get; set; }

        public bool IsChanged { get; set; }

        public bool IsNew { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsNew)
            {
                if (Name.Length > MaxNameLength)
                    yield return new ValidationResult(
                        string.Format(Res.IncorrectLength, MaxNameLength, Name.Length), new[] { "Name" });

                if (Name.Length < MinNameLength)
                    yield return new ValidationResult(
                        string.Format(Res.IncorrectMinLength, MinNameLength, Name.Length), new[] { "Name" });

                var sameNameProperties = App.Rep.Select<Property>().Where(p => p.Prop_Name.ToLower() == Name.ToLower());
                if (sameNameProperties != null && sameNameProperties.Count() > 0)
                {
                    yield return new ValidationResult(
                        string.Format(Res.SameLinkPropName), new[] { "Name" });
                }

                var link = App.Rep.Select<Link>().FirstOrDefault(l => l.Link_Id == LinkId);
                if (link == null)
                    yield return new ValidationResult(string.Format(Res.IncorrectLinkId));
            }
        }
    }
}