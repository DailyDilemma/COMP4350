using AutoMapper;

using ListAssist.Data.Models;
using ListAssist.WebAPI.Models;

namespace ListAssist.WebAPI.MappingProfile
{
    public class ShoppingListItemProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<LAListItem, ShoppingListItem>()
                .ForMember(s => s.Checked, opt => opt.MapFrom(e => e.Done));
        }
    }
}