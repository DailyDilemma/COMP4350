using AutoMapper;

using ListAssist.Data.Models;
using ListAssist.WebAPI.Models;

namespace ListAssist.WebAPI.MappingProfile
{
    public class ShoppingListSuggestionProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<LASuggestion, ShoppingListSuggestion>();
        }
    }
}