using OnlineShop.Mappers;
using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels.Views;
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
        /// <summary>
        /// Validation Name string for validation message
        /// </summary>
        public const string ValName = "Name";
        /// <summary>
        /// Validation Level string for validation message
        /// </summary>
        public const string ValLevel = "Level";
        /// <summary>
        /// Validation Parent Name string for validation message
        /// </summary>
        public const string ValParName = "ParName";
        /// <summary>
        /// Valid Category model
        /// </summary>
        private CategoryViewVld categoryViewWithValidation;
        /// <summary>
        /// Simple Category model only for view
        /// </summary>
        private CategoryViewSmpl simpleCategoryViews;

        /// <summary>
        /// Return all categories from DB
        /// </summary>
        public List<CategoryViewSmpl> GetAllCategories()
        {
            var resViewCat = new List<CategoryViewSmpl>();//collection witch will be return
            var allCatForLevel = MvcApplication.ContextRepository.Select<Category>();//all cateogires from DB
            foreach (var category in allCatForLevel)//mapping
            {
                var viewCat = (CategoryViewSmpl)MvcApplication.Mapper.Map(category, 
                    typeof(Category), typeof(CategoryViewSmpl));
                try
                {
                    viewCat.ParentName = MvcApplication.ContextRepository.
                         Select<Category>().FirstOrDefault(c => c.Cat_Id ==
                              Convert.ToInt64(viewCat.ParentName)).Cat_Name;
                }
                catch (Exception)
                {
                    viewCat.ParentName = string.Empty;
                }
               
                resViewCat.Add(viewCat);
            }
            return resViewCat;
        }
        

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var error = string.Empty;
            List<object> errors = new List<object>();
            try
            {
                try
                {
                    categoryViewWithValidation.Level = simpleCategoryViews.Level;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    errors.Add(new object[] { ex.Message, ValLevel });
                }
                try
                {
                    categoryViewWithValidation.Name = simpleCategoryViews.Name;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    errors.Add(new object[] { ex.Message, ValName });
                }
                catch(ArgumentException ex)
                {
                    errors.Add(new object[] { ex.Message, ValName });
                }
                try
                {
                    categoryViewWithValidation.ParentName =
                        simpleCategoryViews.ParentName;
                }
                catch (ArgumentException ex)
                {
                    errors.Add(new object[] { ex.Message, ValParName });
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            if (error.Length != 0)
                yield return new ValidationResult(error);
            else
            {
                foreach (object[] er in errors)
                {
                    yield return new ValidationResult(
                        er[0].ToString(), new string[] { er[1].ToString() });
                }
            }
        }
    }
}