using AutoMapper;
using ScrapManagement.Application.Features.Scraps.Commands.Create;
using ScrapManagement.Application.Features.Scraps.Commands.Update;
using ScrapManagement.Application.Features.Scraps.Queries.GetAllCached;
using ScrapManagement.Application.Features.Scraps.Queries.GetById;
using ScrapManagement.Web.Areas.Apps.Models;

namespace ScrapManagement.Web.Areas.Apps.Mappings
{
    internal class ScrapProfile : Profile
    {
        public ScrapProfile()
        {
            CreateMap<GetAllScrapsCachedResponse, ScrapViewModel>().ReverseMap();
            CreateMap<GetScrapByIdResponse, ScrapViewModel>().ReverseMap();
            CreateMap<CreateScrapCommand, ScrapViewModel>().ReverseMap();
            CreateMap<UpdateScrapCommand, ScrapViewModel>().ReverseMap();
        }
    }
}