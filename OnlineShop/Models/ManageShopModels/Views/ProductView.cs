using OnlineShop.Models.Db.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.ManageShopModels
{
    public class ProductView
    {
        public const byte MaxNameLength = 200;
        public const string Incorrect = "Was sent an incorrect {0} => {1}";

        private Category category;
        private Description description;
        private List<Image> images;
        private string name;
        private double price;
        private int count;

        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                Contract.Requires<ArgumentException>(value > 0,
                    string.Format(Incorrect, "count", value));
            }
        }

        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                Contract.Requires<ArgumentException>(value > 0.0,
                    string.Format(Incorrect, "price", value));
                price = value;
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
                Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(value));
                Contract.Requires<ArgumentOutOfRangeException>
                    (value.Length <= 200);
                name = value;
            }
        }

        public bool IsAvailable { get; set; }

        public Category Category
        {
            get
            {
                return category ?? new Category();
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                category = value;
            }
        }

        public Description Description
        {
            get
            {
                return description ?? new Description();
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                description = value;
            }
        }

        public List<Image> Images
        {
            get
            {
                return images ?? new List<Image>();
            }
        }

        public bool AddImage(Image img, bool clearCollection = false)
        {
            Contract.Requires<ArgumentNullException>(img != null);
            if (clearCollection)
                images.Clear();
            images.Add(img);
            return true;
        }

        public bool AddImages(List<Image> img, bool clearCollection = false)
        {
            Contract.Requires<ArgumentNullException>(img != null);
            Contract.Requires<ArgumentNullException>(img.Count != 0);
            if (clearCollection)
                images.Clear();
            images.AddRange(img);
            return true;
        }

    }
}