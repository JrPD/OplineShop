﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models.ManageShopModels.Views
{
    public class LinkView : IComparable<LinkView>, IValidatableObject
    {
        //todo Запитати про булеві поля і CompareTo
        public const byte MaxNameLength = 200;

        public long Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Res),
            ErrorMessageResourceName = "NameCantBeNull")]
        [Display(Name = "Ім'я групи властивостей")]
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
            if (Name.Length > MaxNameLength)
                yield return new ValidationResult(
                    string.Format(Res.IncorrectLength, MaxNameLength, Name.Length), new[] { "Name" });
        }
    }
}