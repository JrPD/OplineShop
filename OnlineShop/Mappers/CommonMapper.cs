using AutoMapper;
using OnlineShop.Models.Db.Tables;
using OnlineShop.Models.ManageShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Mappers
{
    public class CommonMapper : IMapper
    {
        public CommonMapper()
        {
            Mapper.CreateMap<CategoryView, Category>()
                .ForMember(dest => dest.Cat_Level,
                    opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.Cat_Parent_Cat_Id,
                    opt => opt.MapFrom(src => src.ParentId))
                .ForMember(dest => dest.IsAvailable,
                    opt => opt.UseValue(true))
                .ForMember(dest => dest.Cat_Name,
                    opt => opt.MapFrom(src => src.Name));
            Mapper.CreateMap<Category, CategoryView>()
                .ForMember(dest => dest.Level,
                    opt => opt.MapFrom(src => src.Cat_Level))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Cat_Name))
                .ForMember(dest => dest.ParentId,
                    opt => opt.MapFrom(src => src.Cat_Parent_Cat_Id))
                //todo ??? хз чи так піде треба стестувати
                .ForMember(dest => dest.Products,
                    opt => opt.MapFrom(src => src.Products.ToList()));
        }
        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }
    }
}