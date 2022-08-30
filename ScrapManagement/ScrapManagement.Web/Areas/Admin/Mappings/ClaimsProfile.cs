using AutoMapper;
using ScrapManagement.Web.Areas.Admin.Models;
using System.Security.Claims;

namespace ScrapManagement.Web.Areas.Admin.Mappings
{
    public class ClaimsProfile : Profile
    {
        public ClaimsProfile()
        {
            CreateMap<Claim, RoleClaimsViewModel>().ReverseMap();
        }
    }
}