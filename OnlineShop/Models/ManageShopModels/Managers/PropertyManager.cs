using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.ManageShopModels.Managers
{
    /// <summary>
    /// Manager for using with View, here we had connecting with PropertyView and LinkView for set valid values
    /// </summary>
    public class PropertyManager
    {
        /// <summary>
        /// Return all Links from DB
        /// </summary>
        public List<LinkView> GetAllLinksProperties()
        {
            List<LinkView> resListLinkView = new List<LinkView>();//collection 
            List<Link> linksProperties = MvcApplication.ContextRepository.Select<Link>().ToList();

            foreach (var linkProperties in linksProperties)//mapping
            {
                var viewLink = (LinkView)MvcApplication.Mapper.Map(linkProperties,
                   typeof(Link), typeof(LinkView));
                resListLinkView.Add(viewLink);
            }
            return resListLinkView;
        }
        /// <summary>
        /// Add new link to DB
        /// </summary>
        /// <param name="link">link which will be added</param>
        public void AddNewLink(LinkView link)
        {
             var mapLink = (Link)MvcApplication.Mapper.Map(link, typeof(LinkView), typeof(Link));
             MvcApplication.ContextRepository.Insert<Link>(mapLink, true);
        }

        /// <summary>
        /// Retur all Properties of current link
        /// </summary>
        /// <param name="link_Id">Id of current link</param>
        public List<PropertyView> GetProperties(long link_Id)
        {
            List<PropertyView> resListPropertiesView = new List<PropertyView>();
            List<Property> properties = MvcApplication.ContextRepository.Select<Property>()
                .Where(p => p.Prop_Link_Id == link_Id).ToList();
            foreach (var property in properties)//mapping
            {
                var viewProperty = (PropertyView)MvcApplication.Mapper.Map(property,
                   typeof(Property), typeof(PropertyView));
                resListPropertiesView.Add(viewProperty);
            }
            return resListPropertiesView;
        }
        /// <summary>
        /// Retur name of current Link
        /// </summary>
        /// <param name="link_Id">Id of current link</param>
        public string GetLinkName(long link_Id)
        {
            string linkName = MvcApplication.ContextRepository.Select<Link>()
                .FirstOrDefault(l => l.Link_Id == link_Id).Link_Name;
            return linkName;
        }
        /// <summary>
        /// Retur id of current Link
        /// </summary>
        /// <param name="link_Id">Name of current link</param>
        public long GetLinkId(string linkName)
        {
            long link_id = MvcApplication.ContextRepository
                .Select<Link>().FirstOrDefault(l => l.Link_Name == linkName).Link_Id;
            return link_id;
        }
        /// <summary>
        /// Retur id of current Link
        /// </summary>
        /// <param name="link_Id">Id of property which contained in this link</param>
        public long GetLinkId(long prop_id)
        {
            long link_id = MvcApplication.ContextRepository.Select<Property>().FirstOrDefault(p => p.Prop_Id == prop_id).Prop_Link_Id;
            return link_id;
        }
        /// <summary>
        /// Prepare new model for creating category view
        /// </summary>
        /// <param name="parentName">Parent Name of Category in what we want to create new</param>
        /// <returns>CategoryView model prepared for enter all needed data</returns>
        public PropertyView CreateNewPropertyModel(long link_id)
        {
            var model = new PropertyView()
            {
                Link_Id = link_id
            };
            return model;
        }
        /// <summary>
        /// Remove current link
        /// </summary>
        /// <param name="link_Id">Id of current link</param>
        public void RemoveLink (long link_Id)
        {
            Link link = MvcApplication.ContextRepository.Select<Link>()
               .FirstOrDefault(l => l.Link_Id == link_Id);
            if(link != null)
            {
                MvcApplication.ContextRepository.Delete<Link>(link, true);
            }
        }


        /// <summary>
        /// Add new property to DB
        /// </summary>
        /// <param name="property">Property which will be added</param>
        public void AddNewProperty(PropertyView property)
        {
            var mapProperty = (Property)MvcApplication.Mapper.Map(property, typeof(PropertyView), typeof(Property));
            MvcApplication.ContextRepository.Insert<Property>(mapProperty, true);
        }
        /// <summary>
        /// Remove current Property
        /// </summary>
        /// <param name="prop_id">Id of current link</param>
        public void RemoveProperty(long prop_id)
        {
            Property property = MvcApplication.ContextRepository.Select<Property>()
               .FirstOrDefault(p => p.Prop_Id == prop_id);
            if (property != null)
            {
                MvcApplication.ContextRepository.Delete<Property>(property, true);
            }
        }
    }
}