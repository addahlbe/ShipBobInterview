using AutoMapper;
using AspCoreVue.Dtos;
using AspCoreVue.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AspCoreVue.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserOrderDto, UserOrder>();
            CreateMap<UserOrder, UserOrderDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();
        }
    }
}