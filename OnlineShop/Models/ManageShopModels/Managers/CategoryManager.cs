using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.ManageShopModels.Managers
{
    public class CategoryManager : IValidatableObject
    {
        private CategoryView categoryView;

        public string Name { get; set; }
        public byte Level { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}