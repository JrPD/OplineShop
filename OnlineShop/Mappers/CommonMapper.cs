using AutoMapper;
using OnlineShop.Models;
using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels;
using OnlineShop.Models.ManageShopModels.Managers;
using OnlineShop.Models.ManageShopModels.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
					opt => opt.MapFrom(src =>src.ParentId))
				.ForMember(dest => dest.Cat_Name,
					opt => opt.MapFrom(src => src.Name))
				.ForMember(dest=>dest.Cat_Id,
					opt=>opt.MapFrom(src=>src.Id))
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
				.ForMember(dest=>dest.Id,
					opt=>opt.MapFrom(src=>src.Cat_Id))
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
					opt => opt.MapFrom(src => src.IsAvailable));
			Mapper.CreateMap<Product, ProductView>()
				.ForMember(dest => dest.Count,
					opt => opt.MapFrom(src => src.Pr_Count))
				.ForMember(dest => dest.Price,
					opt => opt.MapFrom(src => src.Pr_Price))
				.ForMember(dest => dest.Name,
					opt => opt.MapFrom(src => src.Pr_Name))
				.ForMember(dest => dest.IsAvailable,
					opt => opt.MapFrom(src => src.Pr_IsAvailable));
            Mapper.CreateMap<CategoryView, Image>()
                .ForMember(dest => dest.Img_Id,
                    opt => opt.MapFrom(src => src.ImageId))
                .ForMember(dest => dest.Img_Path,
                    opt => opt.MapFrom(src => src.ImagePath));
            Mapper.CreateMap<Link,LinkView>()
                .ForMember(dest=>dest.Id,
                    opt=>opt.MapFrom(src=>src.Link_Id))
                .ForMember(dest=>dest.Name,
                    opt=>opt.MapFrom(src=>src.Link_Name));
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
            if (model.ImageId != CategoryManager.DefParentId)
            {
                var imgId = model.ImageId;
                var img = MvcApplication.ContextRepository.Select<Image>()
                    .FirstOrDefault(i => i.Img_Id == imgId);
                if (img != null)
                {
                    model.ImagePath = img.Img_Path;
                }
            }
        }
    }
}