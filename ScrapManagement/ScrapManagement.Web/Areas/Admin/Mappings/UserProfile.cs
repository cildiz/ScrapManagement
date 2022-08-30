using AutoMapper;
using ScrapManagement.Infrastructure.Identity.Models;
using ScrapManagement.Web.Areas.Admin.Models;

namespace ScrapManagement.Web.Areas.Admin.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}