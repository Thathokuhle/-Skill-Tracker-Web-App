using AutoMapper;
using DataLogic.Account;
using ViewLogic;
using ViewLogic.AccountViews;

namespace BusinessLogic.StaticMappings
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GetUserByEmail, GetUserByEmailView>().ReverseMap();
                cfg.CreateMap<CreateAccount, CreateAccountView>().ReverseMap();


                //Admin
            });

            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

}
