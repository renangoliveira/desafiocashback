using AutoMapper;
using CashbackDomain.DTO;
using CashbackDomain.Identity;
using CashbackDomain.Model;

namespace CashbackAPI.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {                     
            CreateMap<User, Revendedor>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
        }
    }
}
