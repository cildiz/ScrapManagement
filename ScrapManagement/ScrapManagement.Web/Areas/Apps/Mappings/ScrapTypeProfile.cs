using AutoMapper;
using ScrapManagement.Application.Features.ScrapTypes.Commands.Create;
using ScrapManagement.Application.Features.ScrapTypes.Commands.Update;
using ScrapManagement.Application.Features.ScrapTypes.Queries.GetAllCached;
using ScrapManagement.Application.Features.ScrapTypes.Queries.GetById;
using ScrapManagement.Web.Areas.Apps.Models;

namespace ScrapManagement.Web.Areas.Apps.Mappings
{
    internal class ScrapTypeProfile : Profile
    {
        public ScrapTypeProfile()
        {
            CreateMap<GetAllScrapTypesCachedResponse, ScrapTypeViewModel>().ReverseMap();
            CreateMap<GetScrapTypeByIdResponse, ScrapTypeViewModel>().ReverseMap();
            CreateMap<CreateScrapTypeCommand, ScrapTypeViewModel>().ReverseMap();
            CreateMap<UpdateScrapTypeCommand, ScrapTypeViewModel>().ReverseMap();
        }
    }
}