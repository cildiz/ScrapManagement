using AutoMapper;
using ScrapManagement.Application.Features.Scraps.Commands.Create;
using ScrapManagement.Application.Features.Scraps.Queries.GetAllCached;
using ScrapManagement.Application.Features.Scraps.Queries.GetAllPaged;
using ScrapManagement.Application.Features.Scraps.Queries.GetById;
using ScrapManagement.Domain.Entities;

namespace ScrapManagement.Application.Mappings
{
    internal class ScrapProfile : Profile
    {
        public ScrapProfile()
        {
            CreateMap<CreateScrapCommand, Scrap>().ReverseMap();
            CreateMap<GetScrapByIdResponse, Scrap>().ReverseMap();
            CreateMap<GetAllScrapsCachedResponse, Scrap>().ReverseMap();
            CreateMap<GetAllScrapsResponse, Scrap>().ReverseMap();
        }
    }
}