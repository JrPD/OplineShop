using OnlineShop.Mappers;
using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.ManageShopModels.Managers
{
	/// <summary>
	/// Manager for using with View, here we had connecting with CategoryView for set valid values
	/// </summary>
	public static class CategoryManager 
	{
		/// <summary>
		/// Try to get Id Categroy from  his Name(frequency using for search parent category name)
		/// </summary>
		/// <param name="parentName">name of parent or current category</param>
		/// <returns></returns>
		public static long GetIdFromName(string name)
		{
			if (name == null ||
				name.Length == 0)
				return Convert.ToInt64(Res.DefaultParentCategoryId);
			try
			{
				return App.Rep.Select<Category>()
					.FirstOrDefault(c => c.Cat_Name == name).Cat_Id;
			}
			catch(Exception)
			{
				return Convert.ToInt64(Res.DefaultParentCategoryId);
			}
		}

		/// <summary>
		/// Chekc if next level is last for possibility to add categories
		/// </summary>
		/// <param name="parentId">parent category Id</param>
		/// <returns>Is next is level last</returns>
		public static bool IsNextLastLevel(long parentId)
		{
			var category = App.Rep.Select<Category>().FirstOrDefault(c => c.Cat_Id == parentId);
			if (category != null && category.Cat_Level + 1 == CategoryView.MaxLevel)
				return true;
			return false;
		}
		/// <summary>
		/// Return parent name from current category
		/// </summary>
		/// <param name="id">Id from current category</param>
		/// <returns>Parent Name or NULL if there no parent category</returns>
		public static string GetParentName(long id)
		{
			var category = GetCategoryById(id);
			if (category != null)
			{
				if (category.ParentName != null)
					return category.ParentName;
				else if (category.ParentId != Convert.ToInt64(Res.DefaultParentCategoryId))
					return GetNameFromId(category.ParentId);
			}
			return null;
		}

		/// <summary>
		/// Search name of Category by his id
		/// </summary>
		/// <param name="id">id of parent Category</param>
		/// <returns>Name of parent Category</returns>
		public static string GetNameFromId(long id)
		{
			if(id == Convert.ToInt64(Res.DefaultParentCategoryId))
			{
				return null;
			}
			else
			{
			   var category = App.Rep.Select<Category>()
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
		public static Dictionary<string, long> GetAllParentCategories(string parentName)
		{
			var res = new Dictionary<string, long>();
			var category = App.Rep.Select<Category>().FirstOrDefault(c => c.Cat_Name == parentName);
			if(category != null)
			{
				res.Add(category.Cat_Name, category.Cat_Id);
				if (category.Cat_Parent_Cat_Id != Convert.ToInt64(Res.DefaultParentCategoryId))
				{
					var parCategory = App.Rep.Select<Category>().FirstOrDefault(c => c.Cat_Id == category.Cat_Parent_Cat_Id);
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
		public static void SaveNewImage(CategoryView model)
		{
			if (model.ImgFile != null && model.ImgFile.ContentLength > 0)
			{
                var path = Res.ImagesDirectory
                       + Res.CategoryImagesDirectory;
                var fileName = Guid.NewGuid().ToString()
                       + model.ImgFile.FileName;
                model.ImagePath = path + fileName;

                byte[] data;
                using (Stream inputStream = model.ImgFile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data = memoryStream.ToArray();
                }
                if(!ImageManager.ImageManager.UploadFile(data, fileName, path))
                {
                    model.ImagePath = null;
                    model.ImageId = null;
                    model.ImgFile = null;
                    return;
                }
                App.Rep.Insert<Image>
					((Image)App.Mapper.Map(model, 
						typeof(CategoryView), typeof(Image)),true);
			}
		}

		/// <summary>
		/// Return Id of parent category from current
		/// </summary>
		/// <param name="id">Id of current Category</param>
		/// <returns>Default Parent Id if parent was not found or his Id</returns>
		public static long GetParentId(long id)
		{
			var category = GetCategoryById(id);
			if (category != null)
			{
				if (category.ParentId != 0 ||
					category.ParentId != Convert.ToInt64(Res.DefaultParentCategoryId))
					return category.ParentId;
			}
			return Convert.ToInt64(Res.DefaultParentCategoryId);
		}

		/// <summary>
		/// Prepare new model for creating category view
		/// </summary>
		/// <param name="parentName">Parent Name of Category in what we want to create new</param>
		/// <returns>CategoryView model prepared for enter all needed data</returns>
		public static CategoryView CreateNewModel(string parentName)
		{
			var parId = GetIdFromName(parentName);
			var model = new CategoryView()
			{
				ParentId = parId,
				ParentName = parentName,
				Level = GetCurrentLevelFromParentId(parId),
				HasSubCategories = false
			};
            App.Mapper.MapLinksForCategory(ref model);
			return model;
		}

		/// <summary>
		/// Update category in DB with your changed model from View
		/// </summary>
		/// <param name="model">CategoryView model with was changed by user</param>
		public static void UpdateCategory(CategoryView model)
		{
			//todo save links
			if (model.ImagePath != null && model.ImagePath.Length != 0)
			{
				var img =  App.Rep.Select<Image>()
					.FirstOrDefault(i => i.Img_Path == model.ImagePath);
				if (img != null)
				{
					model.ImageId = img.Img_Id;
				}
				else
				{
					model.ImageId = null;
				}
			}
            SaveProperties(model);
			App.Rep.Update<Category>((Category)
					App.Mapper.Map(model, typeof(CategoryView), typeof(Category)), true);
		}

		/// <summary>
		/// Here we can get category for edit by his name
		/// </summary>
		/// <param name="catName">name of searched category</param>
		/// <returns></returns>
		public static CategoryView GetCategoryById(long catId)
		{
			var dbCategory = App.Rep.Select<Category>()
				.FirstOrDefault(c => c.Cat_Id == catId);
			if (dbCategory != null && dbCategory.Cat_Id > 0)
			{                                                             
				var catView = (CategoryView)App.Mapper.Map(dbCategory,
						typeof(Category), typeof(CategoryView));
                App.Mapper.MapLinksForCategory(ref catView);
				return catView;
			}
			else
				return null;
		}

		/// <summary>
		/// This function save our new category into DB
		/// </summary>
		/// <param name="model">Category with will be saved</param>
		public static void SaveNewCategory(CategoryView model)
		{
			var parCat = App.Rep.Select<Category>()
				.FirstOrDefault(c => c.Cat_Id == model.ParentId);
			if(parCat != null)
			{
				parCat.Cat_HasChild = true;
				App.Rep.Update<Category>(parCat, false);
				model.Level = parCat.Cat_Level;
				model.Level++;
			}
			if(model.ImagePath != null && model.ImagePath.Length != 0)
			{
				model.ImageId = App.Rep.Select<Image>()
					.FirstOrDefault(i => i.Img_Path == model.ImagePath).Img_Id;
			}
            var cat = App.Rep.Insert<Category>((Category)
				  App.Mapper.Map(model,
				  typeof(CategoryView), typeof(Category)), true);
            if (cat != null)
                model.Id = cat.Cat_Id;
            SaveProperties(model);
		}

        /// <summary>
        /// Save All checked Category links from Add or Edit Forms by Category Id
        /// </summary>                     
        /// <param name="model">CategoryView model with links and Category Id</param>
        private static void SaveProperties(CategoryView model)
        {
            if (model.Id != 0)
            {
                foreach (var link in model.Properties)
                {
                    if (!link.IsNew && !link.Checked)
                    {
                        var linkCat = App.Rep.Select<LinkCategories>()
                          .FirstOrDefault(lc => lc.Category_Cat_Id == model.Id
                          && lc.Link_Link_Id == link.Id);
                        if (linkCat != null)
                            App.Rep.Delete<LinkCategories>(linkCat, false);
                    }
                    else if (link.Checked && link.IsNew)
                    {
                        App.Rep.Insert<LinkCategories>(new LinkCategories()
                        {
                            Category_Cat_Id = model.Id,
                            Link_Link_Id = link.Id
                        }, false);
                    }
                }
                App.Rep.Save();
            }
        }

        /// <summary>
        /// Same value as Res.DefaultParentCategoryId need to be used in Controller as argument
        /// </summary>
        public  const long DefaultParentCategoryId = -1;

		/// <summary>
		/// Return current level using for it parent Id
		/// </summary>
		/// <param name="parentId">parent id for searching current level</param>
		/// <returns>current level</returns>
		public static byte GetCurrentLevelFromParentId(long parentId)
		{
			if (parentId == Convert.ToInt64(Res.DefaultParentCategoryId))
				return 1;
			var parCategory = App.Rep.Select<Category>().
				FirstOrDefault(c => c.Cat_Id == parentId);
			if (parCategory == null)
				return 1;
			else
				return parCategory.Cat_Level++;
		}                                   

		/// <summary>
		/// Remove Category from DB searching this Category by his Id
		/// </summary>
		/// <param name="id">Id for Category with we want delete</param>
		public static void RemoveCategoryById(long id)
		{
			var category = App.Rep.Select<Category>()
			   .FirstOrDefault(c => c.Cat_Id == id);
			if (category != null)
			{
				RemoveChildCategory(category);

				var parCat = App.Rep.Select<Category>()
					.FirstOrDefault(c => c.Cat_Id == category.Cat_Parent_Cat_Id);
				if (parCat != null)
				{
					parCat.Cat_HasChild = false;
					App.Rep.Update<Category>(parCat, false);
				}                                                                     
			}
		}

		/// <summary>
		/// Remove child Categories for current Category, using before remove this Category
		/// </summary>
		/// <param name="category">Current category with need to clear all child's</param>
		private static void RemoveChildCategory(Category category)
		{
			if(category.Cat_HasChild)
			{
				var childCat = App.Rep.Select<Category>()
					.Where(c => c.Cat_Parent_Cat_Id == category.Cat_Id);
				if(childCat != null)
				foreach (var child in childCat.ToList())
				{
					RemoveChildCategory(child);
				}
			}
			else
			{
				RemoveAllProducts(category.Cat_Id);
			}
			App.Rep.Delete<Category>(category, true);
		}

		/// <summary>
		/// Move all Products from current Category into Default with Id -2
		/// </summary>
		/// <param name="cat_Id">Id of current category</param>
		private static void RemoveAllProducts(long cat_Id)
		{
			var products = App.Rep.Select<Product>()
				.Where(p => p.Pr_Cat_Id == cat_Id).ToList();
			if (products.Count != 0)
			{
				foreach (var product in products)
				{
					product.Pr_Cat_Id = Convert.ToInt64(Res.DefaultCategoryForProductsId);
					product.Category = App.Rep.Select<Category>()
						.FirstOrDefault(c => c.Cat_Id == Convert.ToInt64(Res.DefaultCategoryForProductsId));
					App.Rep.Update<Product>(product, false);
				}
				App.Rep.Save();
			}
		}
		/// <summary>
		/// Return all categories from DB
		/// </summary>
		public static List<CategoryView> GetAllCategories(long parentId)
		{
			var resViewCat = new List<CategoryView>();//collection witch will be return
			if (parentId == Convert.ToInt64(Res.DefaultParentCategoryId))
			{
				var allCatForLevel = App.Rep.Select<Category>()
					.Where(c=>c.Cat_Level==1);//all cateogires from DB for 1 level
				foreach (var dbCategory in allCatForLevel)//mapping
				{
					var viewCat = (CategoryView)App.Mapper.Map(dbCategory,
						typeof(Category), typeof(CategoryView));
					viewCat.ParentName = string.Empty;
					App.Mapper.MapImageForCategory(ref viewCat);
					resViewCat.Add(viewCat);
				}
			}
			else
			{
				var allCatForParentId = App.Rep.Select<Category>()
					.Where(c=>c.Cat_Parent_Cat_Id == parentId);//all child categories for parentId with was choose by use
				foreach (var category in allCatForParentId)//mapping
				{
					var viewCat = (CategoryView)App.Mapper.Map(category,
					   typeof(Category), typeof(CategoryView));
					try
					{
						viewCat.ParentName = App.Rep.
							 Select<Category>().FirstOrDefault(c => c.Cat_Id ==
								  Convert.ToInt64(viewCat.ParentName)).Cat_Name;
					}
					catch (Exception)
					{
						//todo тут повинен бути якийсь логер або ще щось щоб потім знати про такий баг бо при норм даних сюди не попаде, по ідеї
						viewCat.ParentName = string.Empty;
					}
					App.Mapper.MapImageForCategory(ref viewCat);
					resViewCat.Add(viewCat);
				}
			}
			return resViewCat;
		}        
	}
}