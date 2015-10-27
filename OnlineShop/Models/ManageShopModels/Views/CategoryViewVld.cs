using OnlineShop.Models.Db.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.ManageShopModels.Views
{
    /// <summary>
    /// View Model for Category DB Object with Contract Validation
    /// </summary>
    public class CategoryViewVld
    {
        public const byte MinLevel = 1;
        public const byte MaxLevel = 5;
        public const byte MaxNameLength= 200;
        public const string NameNotFound = "Parent category name was Not Found!!!";
        public const string IncorrectId = "Was sent an incorrect id => {0}";
        public readonly string OutOfRange;

        private byte level = 0;
        private string name = string.Empty;
        private long? parent_id;
        private List<Product> products = new List<Product>();

        public CategoryViewVld()
        {
            OutOfRange = "You can't set smaller that " + MinLevel 
                + "or higher that " + MaxLevel;
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
                //Contract.Requires<ArgumentException>(value.Length > 0,
                //    string.Format(NameCantBeNull, value));
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
                    NameNotFound);
                return name;
            }
            set
            {
                if (value != null && value != string.Empty)
                {
                    var category = MvcApplication.ContextRepository
                        .Select<Category>().FirstOrDefault(c => c.Cat_Name == value);
                    Contract.Requires<ArgumentException>(category != null,
                        string.Format(NameNotFound, value));
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
                    string.Format(IncorrectId, value));
                Contract.Requires<ArgumentException>(!MvcApplication.ContextRepository
                    .Select<Category>().Any(c => c.Cat_Id == value),
                    string.Format(IncorrectId, value));
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