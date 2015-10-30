using OnlineShop.Models.Db.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using System.Linq;
using System.Net;
using System.Web;

namespace OnlineShop.Models.ManageShopModels.Views
{
    /// <summary>
    /// View Model for Category DB Object with Contract Validation
    /// </summary>
    public class CategoryViewVld
    {
        public const byte MinLevel = 1;
        public const byte MaxLevel = 3;
        public const byte MaxNameLength= 200;
        public readonly string OutOfRange;

        private byte level = 0;
        private bool hasChild = false;
        private string name = string.Empty;
        private long? parent_id;
        private List<Product> products = new List<Product>();
        private string imagePath;

        public CategoryViewVld()
        {
            OutOfRange = "You can't set smaller that " + MinLevel 
                + "or higher that " + MaxLevel;
        }

        public bool HasChild
        {
            get
            {
                return hasChild;
            }
            set
            {
                hasChild = value;
            }
        }

        public string ImagePath
        {
            get
            {
                return imagePath ?? string.Empty;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null 
                    && value.Length != 0);
                var file = new FileInfo(HttpContext.Current.Server.MapPath(value));
                Contract.Requires<ArgumentException>(file.Exists,
                    string.Format(Res.IncorrectInput, "Path to file", value));
                imagePath = value;
            }
        }


        public byte Level
        {
            get
            {
                return level;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(
                    Enumerable.Range(MinLevel, MaxLevel).Contains(value),
                    OutOfRange);
                level = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                Contract.Requires<ArgumentException>(value.Length > 0,
                    Res.NameCantBeNull);
                Contract.Requires<ArgumentOutOfRangeException>(
                    value.Length>MaxNameLength);
                name = value;
            }
        }

        public string ParentName
        {
            get
            {
                var name = MvcApplication.ContextRepository
                    .Select<Category>().Where(c => c.Cat_Id == parent_id)
                    .Select(c => c.Cat_Name).First();
                Contract.Requires<NullReferenceException>(!string.IsNullOrEmpty(name),
                    Res.CatView_NameNotFound);
                return name;
            }
            set
            {
                if (value != null && value != string.Empty)
                {
                    var category = MvcApplication.ContextRepository
                        .Select<Category>().FirstOrDefault(c => c.Cat_Name == value);
                    Contract.Requires<ArgumentException>(category != null,
                        string.Format(Res.CatView_NameNotFound, value));
                    parent_id = category.Cat_Id;
                }
                else
                    parent_id = null;
            }
        }

        public long ParentId
        {
            get
            {
                return parent_id ?? -1;
            }
            set
            {
                Contract.Requires<ArgumentException>(value <= 0,
                    string.Format(Res.IncorrectInput,"parent id", value));
                Contract.Requires<ArgumentException>(!MvcApplication.ContextRepository
                    .Select<Category>().Any(c => c.Cat_Id == value),
                    string.Format(Res.IncorrectInput,"parent id", value));
                parent_id = value;
            }
        }

        public List<Product> Products
        {
            get
            {
                return products ?? new List<Product>();
            }
        }

        public bool AddProducts(Product pr, bool clearCollection = false)
        {
            Contract.Requires<ArgumentNullException>(pr != null);
            if (clearCollection)
                products.Clear();
            products.Add(pr);
            return true;
        }

        public bool AddProducts(List<Product> pr, bool clearCollection = false)
        {
            Contract.Requires<ArgumentNullException>(pr != null);
            Contract.Requires<ArgumentNullException>(pr.Count != 0);
            if (clearCollection)
                products.Clear();
            products.AddRange(pr);
            return true;
        }
    }
}