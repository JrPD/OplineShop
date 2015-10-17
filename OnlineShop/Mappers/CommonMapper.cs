using AutoMapper;
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
            //Mapper.CreateMap<User, UserView>();
        }
        public object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }
    }
}