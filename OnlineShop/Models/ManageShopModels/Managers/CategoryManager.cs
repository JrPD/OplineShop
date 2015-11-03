﻿using OnlineShop.Mappers;
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
    public class CategoryManager 
    {
        /// <summary>
        /// Try to get Id Categroy from  his Name(frequency using for search parent category name)
        /// </summary>
        /// <param name="parentName">name of parent or current category</param>
        /// <returns></returns>
        public long GetIdFromName(string parentName)
        {
            if (parentName == null ||
                parentName.Length == 0)
                return DefParentId;
            try
            {
                return MvcApplication.ContextRepository.Select<Category>()
                    .FirstOrDefault(c => c.Cat_Name == parentName).Cat_Id;
            }
            catch(Exception)
            {
                return DefParentId;
            }
        }

        /// <summary>
        /// Search name of parent Category
        /// </summary>
        /// <param name="parentId">id of parent Category</param>
        /// <returns>Name of parent Category</returns>
        public string GetParentName(long parentId)
        {
            if(parentId == DefParentId)
            {
                return null;
            }
            else
            {
               var parCategory = MvcApplication.ContextRepository.Select<Category>()
                    .FirstOrDefault(c => c.Cat_Id == parentId);
                if (parCategory != null)
                    return parCategory.Cat_Name;
                else
                    return null;
            }
        }

        /// <summary>
        /// Here we can get category for edit by his name
        /// </summary>
        /// <param name="catName">name of searched category</param>
        /// <returns></returns>
        public CategoryView GetCategoryById(long catId)
        {
            var dbCategory = MvcApplication.ContextRepository.Select<Category>()
                .FirstOrDefault(c => c.Cat_Id == catId);
            if (dbCategory != null && dbCategory.Cat_Id > 0)
            {
                return (CategoryView)MvcApplication.Mapper.Map(dbCategory,
                        typeof(Category), typeof(CategoryView));
            }
            else
                throw new Exception(string.Format(Res.IncorrectInput, "Category Id", catId));
        }

        /// <summary>
        /// Return current level using for it parent Id
        /// </summary>
        /// <param name="parentId">parent id for searching current level</param>
        /// <returns>current level</returns>
        public byte GetCurrentLevelFromParentId(long parentId)
        {
            if (parentId == DefParentId)
                return 1;
            var parCategory = MvcApplication.ContextRepository.Select<Category>().
                FirstOrDefault(c => c.Cat_Id == parentId);
            if (parCategory == null)
                return 1;
            else
                return parCategory.Cat_Level++;
        }

        /// <summary>
        /// This will use when we want to get all categories from Level 1
        /// </summary>
        public const int DefParentId = -1;

        /// <summary>
        /// Remove Category from DB searching this Category by his Id
        /// </summary>
        /// <param name="id">Id for Category with we want delete</param>
        public void RemoveCategoryById(long id)
        {
            MvcApplication.ContextRepository.Delete<Category>(
                MvcApplication.ContextRepository.Select<Category>().FirstOrDefault(
                    c => c.Cat_Id == id), true);                                             
        }

        /// <summary>
        /// Return all categories from DB
        /// </summary>
        public List<CategoryView> GetAllCategories(long parentId)
        {
            var resViewCat = new List<CategoryView>();//collection witch will be return
            if (parentId == DefParentId)
            {
                var allCatForLevel = MvcApplication.ContextRepository.Select<Category>()
                    .Where(c=>c.Cat_Level==1);//all cateogires from DB for 1 level
                foreach (var dbCategory in allCatForLevel)//mapping
                {
                    var viewCat = (CategoryView)MvcApplication.Mapper.Map(dbCategory,
                        typeof(Category), typeof(CategoryView));
                    viewCat.ParentName = string.Empty;
                    resViewCat.Add(viewCat);
                }
            }
            else
            {
                var allCatForParentId = MvcApplication.ContextRepository.Select<Category>()
                    .Where(c=>c.Cat_Parent_Cat_Id == parentId);//all child categories for parentId with was choose by use
                foreach (var category in allCatForParentId)//mapping
                {
                    var viewCat = (CategoryView)MvcApplication.Mapper.Map(category,
                       typeof(Category), typeof(CategoryView));
                    try
                    {
                        viewCat.ParentName = MvcApplication.ContextRepository.
                             Select<Category>().FirstOrDefault(c => c.Cat_Id ==
                                  Convert.ToInt64(viewCat.ParentName)).Cat_Name;
                    }
                    catch (Exception)
                    {
                        //todo тут повинен бути якийсь логер або ще щось щоб потім знати про такий баг бо при норм даних сюди не попаде, по ідеї
                        viewCat.ParentName = string.Empty;
                    }
                    resViewCat.Add(viewCat);
                }
            }
            return resViewCat;
        }
    }
}