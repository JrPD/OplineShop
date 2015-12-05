using AutoMapper;
using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Mappers
{
    public class CommonMapper : IMapper
    {
        public CommonMapper()
        {//todo check it
            Mapper.CreateMap<CategoryView, Category>()
                .ForMember(dest => dest.Cat_Level,
                    opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.Cat_Parent_Cat_Id,
                    opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Cat_Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Cat_Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Cat_Img_Id,
                    opt => opt.MapFrom(src => src.ImageId))
                .ForMember(dest => dest.Cat_HasChild,
                    opt => opt.MapFrom(src => src.HasSubCategories));

            Mapper.CreateMap<Category, CategoryView>()
                .ForMember(dest => dest.ParentId,
                    opt => opt.MapFrom(src => src.Cat_Parent_Cat_Id))
                .ForMember(dest => dest.Level,
                    opt => opt.MapFrom(src => src.Cat_Level))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Cat_Name))
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Cat_Id))
                .ForMember(dest => dest.ImageId,
                    opt => opt.MapFrom(src => src.Cat_Img_Id))
                .ForMember(dest => dest.HasSubCategories,
                    opt => opt.MapFrom(src => src.Cat_HasChild));

            Mapper.CreateMap<ProductView, Product>()
                .ForMember(dest => dest.Pr_Count,
                    opt => opt.MapFrom(src => src.Count))
                //todo ??? вроді це фігово
                //.ForMember(dest => dest.Pr_Cat_Id,
                //	opt => opt.MapFrom(src => src.Category.Cat_Id))
                //todo ??? і це тоже
                //.ForMember(dest => dest.Pr_Descr_Id,
                //	opt => opt.MapFrom(src => src.Description.Desc_Id))
                .ForMember(dest => dest.Pr_Price,
                    opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Pr_Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Pr_IsAvailable,
                    opt => opt.MapFrom(src => src.IsAvailable))
                .ForMember(dest => dest.Pr_Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Pr_Cat_Id,
                    opt => opt.MapFrom(src => src.SelectedCategoryId));

            Mapper.CreateMap<Product, ProductView>()
                .ForMember(dest => dest.Count,
                    opt => opt.MapFrom(src => src.Pr_Count))
                .ForMember(dest => dest.Price,
                    opt => opt.MapFrom(src => src.Pr_Price))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Pr_Name))
                .ForMember(dest => dest.IsAvailable,
                    opt => opt.MapFrom(src => src.Pr_IsAvailable))
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Pr_Id))
                .ForMember(dest => dest.SelectedCategoryId,
                    opt => opt.MapFrom(src => src.Pr_Cat_Id));

            Mapper.CreateMap<CategoryView, Image>()
                .ForMember(dest => dest.Img_Id,
                    opt => opt.MapFrom(src => src.ImageId))
                .ForMember(dest => dest.Img_Path,
                    opt => opt.MapFrom(src => src.ImagePath));

            Mapper.CreateMap<Link, LinkView>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Link_Id))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Link_Name))
                .ForMember(dest => dest.Checked,
                    opt => opt.UseValue(false));

            Mapper.CreateMap<LinkView, Link>()
                .ForMember(dest => dest.Link_Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Link_Name,
                    opt => opt.MapFrom(src => src.Name));

            Mapper.CreateMap<Property, PropertyView>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Prop_Id))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Prop_Name))
                .ForMember(dest => dest.Link_Id,
                    opt => opt.MapFrom(src => src.Prop_Link_Id));

            Mapper.CreateMap<PropertyView, Property>()
                .ForMember(dest => dest.Prop_Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Prop_Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Prop_Link_Id,
                    opt => opt.MapFrom(src => src.Link_Id));
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }

        /// <summary>
        /// Get Image details or current category
        /// </summary>
        /// <param name="model">category with needed to get image details</param>
        public void MapImageForCategory(ref CategoryView model)
        {
            if (model != null && model.ImageId != null && model.ImageId.Value != 0)
            {
                var imgId = model.ImageId.Value;
                var img = App.Rep.Select<Image>()
                    .FirstOrDefault(i => i.Img_Id == imgId);
                if (img != null)
                {
                    model.ImagePath = img.Img_Path;
                }
                else
                {
                    model.ImageId = null;
                }
            }
        }

        /// <summary>
        /// Get all links from DB and links with already added to our Category
        /// </summary>
        /// <param name="model">CategoryView model where we need to add our links</param>
        public void MapLinksForCategory(ref CategoryView model)
        {
            if (model != null)
            {
                var catId = model.Id;
                var linkCats = App.Rep.Select<LinkCategories>()
                    .Where(lc => lc.Category_Cat_Id == catId).ToList();
                var myProp = new List<Link>();
                if (linkCats != null && linkCats.Count != 0)
                {
                    foreach (var linkCat in linkCats)
                    {
                        var link = App.Rep.Select<Link>()
                            .FirstOrDefault(l => l.Link_Id == linkCat.Link_Link_Id);
                        if (link != null)
                        {
                            myProp.Add(link);
                        }
                    }
                }
                var allLinks = App.Rep.Select<Link>().ToList();
                if (allLinks != null && allLinks.Count != 0)
                {
                    foreach (var link in allLinks)
                    {
                        var linkView = (LinkView)App.Mapper.Map(link, typeof(Link),
                            typeof(LinkView));
                        if (myProp.Any(l => l.Link_Id == linkView.Id))
                        {
                            linkView.Checked = true;
                        }
                        else
                        {
                            linkView.IsNew = true;
                        }
                        model.Properties.Add(linkView);
                    }
                }
                model.Properties.Sort();
            }
        }
    }
}