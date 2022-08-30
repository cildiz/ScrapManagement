using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ScrapManagement.Application.Interfaces.CacheRepositories;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Features.ScrapTypes.Queries.GetById
{
    public class GetScrapTypeByIdQuery : IRequest<Result<GetScrapTypeByIdResponse>>
    {
        public int Id { get; set; }

        public class GetScrapByIdQueryHandler : IRequestHandler<GetScrapTypeByIdQuery, Result<GetScrapTypeByIdResponse>>
        {
            private readonly IScrapTypeCacheRepository _scrapTypeCache;
            private readonly IMapper _mapper;

            public GetScrapByIdQueryHandler(IScrapTypeCacheRepository scrapCache, IMapper mapper)
            {
                _scrapTypeCache = scrapCache;
                _mapper = mapper;
            }

            public async Task<Result<GetScrapTypeByIdResponse>> Handle(GetScrapTypeByIdQuery query, CancellationToken cancellationToken)
            {
                var scrap = await _scrapTypeCache.GetByIdAsync(query.Id);
                var mappedScrap = _mapper.Map<GetScrapTypeByIdResponse>(scrap);
                return Result<GetScrapTypeByIdResponse>.Success(mappedScrap);
            }
        }
    }
}