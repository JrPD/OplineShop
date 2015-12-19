using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels.Views;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Models.ManageShopModels.Managers
{
    /// <summary>
    /// Manager for using with View, here we had connecting with PropertyView and LinkView for set valid values
    /// </summary>
    public static class PropertyManager
    {
        /// <summary>
        /// Return all Links from DB
        /// </summary>
        public static List<LinkView> GetAllLinksProperties()
        {
            List<LinkView> resListLinkView = new List<LinkView>();//collection
            List<Link> linksProperties = App.Rep.Select<Link>().ToList();

            foreach (var linkProperties in linksProperties)//mapping
            {
                var viewLink = (LinkView)App.Mapper.Map(linkProperties,
                   typeof(Link), typeof(LinkView));
                resListLinkView.Add(viewLink);
            }
            return resListLinkView;
        }

        /// <summary>
        /// Add new link to DB
        /// </summary>
        /// <param name="link">link which will be added</param>
        public static void AddNewLink(LinkView link)
        {
            var mapLink = (Link)App.Mapper.Map(link, typeof(LinkView), typeof(Link));
            App.Rep.Insert<Link>(mapLink, true);
        }

        /// <summary>
        /// Retur all Properties of current link
        /// </summary>
        /// <param name="link_Id">Id of current link</param>
        public static List<PropertyView> GetProperties(long link_Id)
        {
            List<PropertyView> resListPropertiesView = new List<PropertyView>();
            List<Property> properties = App.Rep.Select<Property>()
                .Where(p => p.Prop_Link_Id == link_Id).ToList();
            foreach (var property in properties)//mapping
            {
                var viewProperty = (PropertyView)App.Mapper.Map(property,
                   typeof(Property), typeof(PropertyView));
                resListPropertiesView.Add(viewProperty);
            }
            return resListPropertiesView;
        }

        /// <summary>
        /// Retur name of current Link
        /// </summary>
        /// <param name="link_Id">Id of current link</param>
        public static string GetLinkName(long link_Id)
        {
            string linkName = App.Rep.Select<Link>()
                .FirstOrDefault(l => l.Link_Id == link_Id).Link_Name;
            return linkName;
        }

        /// <summary>
        /// Retur id of current Link
        /// </summary>
        /// <param name="link_Id">Name of current link</param>
        public static long GetLinkId(string linkName)
        {
            long link_id = App.Rep
                .Select<Link>().FirstOrDefault(l => l.Link_Name == linkName).Link_Id;
            return link_id;
        }

        /// <summary>
        /// Retur id of current Link
        /// </summary>
        /// <param name="link_Id">Id of property which contained in this link</param>
        public static long GetLinkId(long prop_id)
        {
            long link_id = App.Rep.Select<Property>().FirstOrDefault(p => p.Prop_Id == prop_id).Prop_Link_Id;
            return link_id;
        }

        /// <summary>
        /// Prepare new model for creating category view
        /// </summary>
        /// <param name="parentName">Parent Name of Category in what we want to create new</param>
        /// <returns>CategoryView model prepared for enter all needed data</returns>
        public static PropertyView CreateNewPropertyModel(long link_id)
        {
            var model = new PropertyView()
            {
                LinkId = link_id
            };
            return model;
        }

        /// <summary>
        /// Get LinkView from current link
        /// </summary>
        /// <param name="link_Id">Id of current link</param>
        public static LinkView GetLinkView(long link_Id)
        {
            //todo Що робити якщо link null?
            Link link = App.Rep.Select<Link>().FirstOrDefault(l => l.Link_Id == link_Id);
            var linkView = (LinkView)App.Mapper.Map(link,
                        typeof(Link), typeof(LinkView));
            return linkView;
        }

        /// <summary>
        /// Remove current link
        /// </summary>
        /// <param name="link_Id">Id of current link</param>
        public static void RemoveLink(long link_Id)
        {
            Link link = App.Rep.Select<Link>()
               .FirstOrDefault(l => l.Link_Id == link_Id);
            if (link != null)
            {
                App.Rep.Delete<Link>(link, true);
            }
        }

        /// <summary>
        /// Edit current link
        /// </summary>
        /// <param name="link">Current link</param>
        public static void UpdateLink(LinkView link)
        {
            App.Rep.Update<Link>((Link)
                    App.Mapper.Map(link, typeof(LinkView), typeof(Link)), true);
        }

        /// <summary>
        /// Add new property to DB
        /// </summary>
        /// <param name="property">Property which will be added</param>
        public static void AddNewProperty(PropertyView property)
        {
            var mapProperty = (Property)App.Mapper.Map(property, typeof(PropertyView), typeof(Property));
            App.Rep.Insert<Property>(mapProperty, true);
        }

        /// <summary>
        /// Remove current Property
        /// </summary>
        /// <param name="prop_id">Id of current link</param>
        public static void RemoveProperty(long prop_id)
        {
            Property property = App.Rep.Select<Property>()
               .FirstOrDefault(p => p.Prop_Id == prop_id);
            if (property != null)
            {
                App.Rep.Delete<Property>(property, true);
            }
        }

        /// <summary>
        /// Get PropertyView from current property
        /// </summary>
        /// <param name="prop_id">Id of current link</param>
        public static PropertyView GetPropertyView(long prop_Id)
        {
            Property property = App.Rep.Select<Property>().FirstOrDefault(p => p.Prop_Id == prop_Id);
            var propertyView = (PropertyView)App.Mapper.Map(property,
                        typeof(Property), typeof(PropertyView));
            return propertyView;
        }

        /// <summary>
        /// Edit current property
        /// </summary>
        /// <param name="property">Current property</param>
        public static void UpdateProperty(PropertyView property)
        {
            App.Rep.Update<Property>((Property)
                    App.Mapper.Map(property, typeof(PropertyView), typeof(Property)), true);
        }
    }
}