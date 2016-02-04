using AutoMapper;
using ListAssist.Data.Models;
using ListAssist.WebAPI.Models;

using System.Linq;

namespace ListAssist.WebAPI.MappingProfile
{
    public class ShoppingListProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<LAList, ShoppingList>()
                .ForMember(s => s.ShoppingListItems, opt => opt.MapFrom(e => e.LAListItems));
        }
    }
}