using OnlineShop.Models.Db.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OnlineShop.Models.ManageShopModels.Views
{
    public class LinkView : IComparable<LinkView>, IValidatableObject
    {
        public const byte MaxNameLength = 200;
        public const byte MinNameLength = 3;

        [Required]
        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Res),
            ErrorMessageResourceName = "FieldCantBeNull")]
        [Display(Name = "Ім'я групи властивостей")]
        [StringLength(MaxNameLength,
            MinimumLength = MinNameLength,
            ErrorMessageResourceType = typeof(Res),
            ErrorMessageResourceName = "IncorrectLengthAttributeDefault")]
        public string Name { get; set; }

        public bool IsChanged { get; set; }

        public bool IsNew { get; set; }

        public bool Checked { get; set; }

        public int CompareTo(LinkView other)
        {
            return this.Name.CompareTo(other.Name);
        }

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

                var sameNameLinks = App.Rep.Select<Link>().Where(p => p.Link_Name.ToLower() == Name.ToLower());
                if (sameNameLinks != null && sameNameLinks.Count() > 0)
                {
                    yield return new ValidationResult(
                        string.Format(Res.SameLinkGroupName), new[] { "Name" });
                }
            }
        }
    }
}