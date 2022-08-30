using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ScrapManagement.Application.Interfaces.CacheRepositories;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Features.Scraps.Queries.GetById
{
    public class GetScrapByIdQuery : IRequest<Result<GetScrapByIdResponse>>
    {
        public int Id { get; set; }

        public class GetScrapByIdQueryHandler : IRequestHandler<GetScrapByIdQuery, Result<GetScrapByIdResponse>>
        {
            private readonly IScrapCacheRepository _scrapCache;
            private readonly IMapper _mapper;

            public GetScrapByIdQueryHandler(IScrapCacheRepository scrapCache, IMapper mapper)
            {
                _scrapCache = scrapCache;
                _mapper = mapper;
            }

            public async Task<Result<GetScrapByIdResponse>> Handle(GetScrapByIdQuery query, CancellationToken cancellationToken)
            {
                var scrap = await _scrapCache.GetByIdAsync(query.Id);
                var mappedScrap = _mapper.Map<GetScrapByIdResponse>(scrap);
                return Result<GetScrapByIdResponse>.Success(mappedScrap);
            }
        }
    }
}