using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ScrapManagement.Web.Areas.Admin.Models;

namespace ScrapManagement.Web.Areas.Admin.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>().ReverseMap();
        }
    }
}