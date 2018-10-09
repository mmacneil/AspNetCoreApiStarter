using AutoMapper;
using Web.Api.Core.Domain.Entities;
using Web.Api.Infrastructure.Identity;


namespace Web.Api.Infrastructure.Data.Mapping
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<User, AppUser>().ConstructUsing(u => new AppUser {UserName = u.UserName, Email = u.Email}).ForMember(au=>au.Id,opt=>opt.Ignore());
            CreateMap<AppUser, User>().ConstructUsing(au => new User("","", au.Email, au.UserName, au.Id, au.PasswordHash)).ForMember(u => u.Id, opt => opt.Ignore());
        }
    }
}
