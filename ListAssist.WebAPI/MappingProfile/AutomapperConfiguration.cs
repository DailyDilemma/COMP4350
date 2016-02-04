using AutoMapper;

namespace ListAssist.WebAPI.MappingProfile
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ShoppingListProfile>();
                x.AddProfile<ShoppingListItemProfile>();
            });
        }
    }
}