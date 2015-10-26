using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.ManageShopModels.Managers
{
    /// <summary>
    /// Manager for using with View, here we had connecting with CategoryView for set valid values
    /// </summary>
    public class CategoryManager : IValidatableObject
    {
        private CategoryView categoryView;
        /// <summary>
        /// Category tmp Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Category tmp Level
        /// </summary>
        public byte Level { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}