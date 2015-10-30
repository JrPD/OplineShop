using AutoMapper;
using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels;
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
            Mapper.CreateMap<CategoryViewVld, Category>()
                .ForMember(dest => dest.Cat_Level,
                    opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.Cat_Parent_Cat_Id,
                    opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.Cat_Name,
                    opt => opt.MapFrom(src => src.Name))
                //.ForMember(dest => dest.Image.Img_Path,
                //    opt => opt.MapFrom(src => src.ImagePath))
                .ForMember(dest => dest.Cat_HasChild,
                    opt => opt.MapFrom(src => src.HasChild));
            Mapper.CreateMap<Category, CategoryViewVld>()
                .ForMember(dest => dest.Level,
                    opt => opt.MapFrom(src => src.Cat_Level))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Cat_Name))
                .ForMember(dest => dest.ParentId,
                    opt => opt.MapFrom(src => src.Cat_Parent_Cat_Id))
                //.ForMember(dest => dest.ImagePath,
                //    opt => opt.MapFrom(src => src.Image.Img_Path))
                .ForMember(dest => dest.HasChild,
                    opt => opt.MapFrom(src => src.Cat_HasChild))
                //todo ??? хз чи так піде треба стестувати
                .ForMember(dest => dest.Products,
                    opt => opt.MapFrom(src => src.Products.ToList()));
            Mapper.CreateMap<Category, CategoryViewSmpl>()
                .ForMember(dest => dest.ParentName,
                    opt => opt.MapFrom(src => src.Cat_Parent_Cat_Id))
                .ForMember(dest => dest.Level,
                    opt => opt.MapFrom(src => src.Cat_Level))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Cat_Name))
                //.ForMember(dest => dest.ImagePath,
                //    opt => opt.MapFrom(src => src.Image.Img_Path))
                .ForMember(dest => dest.HasSubCategories,
                    opt => opt.MapFrom(src => src.Cat_HasChild));
            Mapper.CreateMap<ProductView, Product>()
                .ForMember(dest => dest.Pr_Count,
                    opt => opt.MapFrom(src => src.Count))
                //todo ??? вроді це фігово
                .ForMember(dest => dest.Pr_Cat_Id,
                    opt => opt.MapFrom(src => src.Category.Cat_Id))
                //todo ??? і це тоже
                .ForMember(dest => dest.Pr_Descr_Id,
                    opt => opt.MapFrom(src => src.Description.Desc_Id))
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
        }
        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }
    }
}