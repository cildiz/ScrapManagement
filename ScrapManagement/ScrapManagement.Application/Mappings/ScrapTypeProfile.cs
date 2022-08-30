using AutoMapper;
using ScrapManagement.Application.Features.ScrapTypes.Commands.Create;
using ScrapManagement.Application.Features.ScrapTypes.Queries.GetAllCached;
using ScrapManagement.Application.Features.ScrapTypes.Queries.GetById;
using ScrapManagement.Domain.Entities;

namespace ScrapManagement.Application.Mappings
{
    internal class ScrapTypeProfile : Profile
    {
        public ScrapTypeProfile()
        {
            CreateMap<CreateScrapTypeCommand, ScrapType>().ReverseMap();
            CreateMap<GetScrapTypeByIdResponse, ScrapType>().ReverseMap();
            CreateMap<GetAllScrapTypesCachedResponse, ScrapType>().ReverseMap();
        }
    }
}