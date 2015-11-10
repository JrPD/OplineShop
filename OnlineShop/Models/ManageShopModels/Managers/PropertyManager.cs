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
    }
}