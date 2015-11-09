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
    public class CategoryManager 
    {
        /// <summary>
        /// Try to get Id Categroy from  his Name(frequency using for search parent category name)
        /// </summary>
        /// <param name="parentName">name of parent or current category</param>
        /// <returns></returns>
        public long GetIdFromName(string name)
        {
            if (name == null ||
                name.Length == 0)
                return DefParentId;
            try
            {
                return MvcApplication.ContextRepository.Select<Category>()
                    .FirstOrDefault(c => c.Cat_Name == name).Cat_Id;
            }
            catch(Exception)
            {
                return DefParentId;
            }
        }

        /// <summary>
        /// Chekc if next level is last for possibility to add categories
        /// </summary>
        /// <param name="parentId">parent category Id</param>
        /// <returns>Is next is level last</returns>
        public bool IsNextLastLevel(long parentId)
        {
            var category = MvcApplication.ContextRepository.Select<Category>().FirstOrDefault(c => c.Cat_Id == parentId);
            if (category != null && category.Cat_Level + 1 == CategoryView.MaxLevel)
                return true;
            return false;
        }
        /// <summary>
        /// Return parent name from current category
        /// </summary>
        /// <param name="id">Id from current category</param>
        /// <returns>Parent Name or NULL if there no parent category</returns>
        public string GetParentName(long id)
        {
            var category = GetCategoryById(id);
            if (category != null)
            {
                if (category.ParentName != null)
                    return category.ParentName;
                else if (category.ParentId != CategoryManager.DefParentId)
                    return GetNameFromId(category.ParentId);
            }
            return null;
        }

        /// <summary>
        /// Search name of Category by his id
        /// </summary>
        /// <param name="id">id of parent Category</param>
        /// <returns>Name of parent Category</returns>
        public string GetNameFromId(long id)
        {
            if(id == DefParentId)
            {
                return null;
            }
            else
            {
               var category = MvcApplication.ContextRepository.Select<Category>()
                    .FirstOrDefault(c => c.Cat_Id == id);
                if (category != null)
                    return category.Cat_Name;
                else
                    return null;
            }
        }

        /// <summary>
        /// Using for get all parents from our positions in categories
        /// </summary>
        /// <param name="parentName">parent Name of out positions</param>
        /// <returns></returns>
        public Dictionary<string,long> GetAllParentCategories(string parentName)
        {
            var res = new Dictionary<string, long>();
            var category = MvcApplication.ContextRepository.Select<Category>().FirstOrDefault(c => c.Cat_Name == parentName);
            if(category != null)
            {
                res.Add(category.Cat_Name, category.Cat_Id);
                if (category.Cat_Parent_Cat_Id != DefParentId)
                {
                    var parCategory = MvcApplication.ContextRepository.Select<Category>().FirstOrDefault(c => c.Cat_Id == category.Cat_Parent_Cat_Id);
                    if (parCategory != null)
                    {
                        var tmpRes = GetAllParentCategories(parCategory.Cat_Name);
                        foreach (var item in tmpRes)
                            res.Add(item.Key, item.Value);
                    }
                }
                return res;
            }
            return null;
        }

        /// <summary>
        /// Save Selected Image
        /// </summary>
        /// <param name="model">Our model from View</param>
        public void SaveNewImage(CategoryView model)
        {
            if (model.ImgFile != null && model.ImgFile.ContentLength > 0)
            {
                MvcApplication.ContextRepository.Insert<Image>
                    ((Image)MvcApplication.Mapper.Map(model, 
                        typeof(CategoryView), typeof(Image)),true);
            }
        }

        /// <summary>
        /// Return Id of parent category from current
        /// </summary>
        /// <param name="id">Id of current Category</param>
        /// <returns>Default Parent Id if parent was not found or his Id</returns>
        public long GetParentId(long id)
        {
            var category = GetCategoryById(id);
            if (category != null)
            {
                if (category.ParentId != 0 ||
                    category.ParentId != DefParentId)
                    return category.ParentId;
            }
            return DefParentId;
        }

        /// <summary>
        /// Prepare new model for creating category view
        /// </summary>
        /// <param name="parentName">Parent Name of Category in what we want to create new</param>
        /// <returns>CategoryView model prepared for enter all needed data</returns>
        public CategoryView CreateNewModel(string parentName)
        {
            var parId = GetIdFromName(parentName);
            var model = new CategoryView()
            {
                ParentId = parId,
                ParentName = parentName,
                Level = GetCurrentLevelFromParentId(parId),
                HasSubCategories = false
            };
            return model;
        }

        /// <summary>
        /// Update category in DB with your changed model from View
        /// </summary>
        /// <param name="model">CategoryView model with was changed by user</param>
        public void UpdateCategory(CategoryView model)
        {
            //todo save links
            if (model.ImagePath != null && model.ImagePath.Length != 0 && model.ImageId == null)
            {//TODO need to fix replacing of picture
                model.ImageId = MvcApplication.ContextRepository.Select<Image>()
                    .FirstOrDefault(i => i.Img_Path == model.ImagePath).Img_Id;
            }
            MvcApplication.ContextRepository.Update<Category>((Category)
                    MvcApplication.Mapper.Map(model,
                    typeof(CategoryView), typeof(Category)), true);
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
                var catLinks = MvcApplication.ContextRepository.Select<LinkCategories>()
                    .Where(cl => cl.Category_Cat_Id == dbCategory.Cat_Id);
                var catView = (CategoryView)MvcApplication.Mapper.Map(dbCategory,
                        typeof(Category), typeof(CategoryView));
                foreach (var catLink in catLinks)
                {
                    var link = MvcApplication.ContextRepository.Select<Link>()
                               .FirstOrDefault(l => l.Link_Id == catLink.Link_Link_Id);
                    if (link != null)
                    {
                        var properties = MvcApplication.ContextRepository.Select<Property>()
                            .Where(p=>p.Prop_Link_Id == link.Link_Id);
                        var propView = new List<PropertyView>();
                        foreach (var prop in properties)
                        {
                            propView.Add((PropertyView)MvcApplication
                                .Mapper.Map(prop,typeof(Property),typeof(PropertyView)));
                        }
                        catView.Properties.Add((LinkView)MvcApplication
                                .Mapper.Map(link, typeof(Link), typeof(LinkView)), propView);
                    }
                }
                return catView;
            }
            else
                return null;
                //throw new Exception(string.Format(Res.IncorrectInput, "Category Id", catId));
        }

        /// <summary>
        /// This function save our new category into DB
        /// </summary>
        /// <param name="model">Category with will be saved</param>
        public void SaveNewCategory(CategoryView model)
        {
            var parCat = MvcApplication.ContextRepository.Select<Category>()
                .FirstOrDefault(c => c.Cat_Id == model.ParentId);
            if(parCat != null)
            {
                parCat.Cat_HasChild = true;
                MvcApplication.ContextRepository.Update<Category>(parCat, false);
                model.Level = parCat.Cat_Level;
                model.Level++;
            }
            if(model.ImagePath != null && model.ImagePath.Length != 0 && model.ImageId == null)
            {
                model.ImageId = MvcApplication.ContextRepository.Select<Image>()
                    .FirstOrDefault(i => i.Img_Path == model.ImagePath).Img_Id;
            }
            if (model.Properties != null && model.Properties.Count > 0)
            {//todo check if we not use some of old links
                foreach (var item in model.Properties)
                {
                    //todo check saving for boolean state
                    //MvcApplication.ContextRepository.Insert<Link>((Link)MvcApplication
                    //            .Mapper.Map(item.Key, typeof(LinkView), typeof(Link)), true);
                    foreach (var property in item.Value)
                    {
                        property.Link_Id = item.Key.Id;
                        //todo check saving for boolean state
                        //MvcApplication.ContextRepository
                        //    .Insert<Property>(property, true);
                    }
                }
            }
            MvcApplication.ContextRepository.Insert<Category>((Category)
                  MvcApplication.Mapper.Map(model,
                  typeof(CategoryView), typeof(Category)), true);
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
            var category = MvcApplication.ContextRepository.Select<Category>()
               .FirstOrDefault(c => c.Cat_Id == id);
            if (category != null)
            {
                var parCat = MvcApplication.ContextRepository.Select<Category>()
                    .FirstOrDefault(c => c.Cat_Id == category.Cat_Parent_Cat_Id);
                if (parCat != null)
                {
                    parCat.Cat_HasChild = false;
                    MvcApplication.ContextRepository.Update<Category>(parCat, false);
                }
                MvcApplication.ContextRepository.Delete<Category>(category,true);
            }
                                               
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