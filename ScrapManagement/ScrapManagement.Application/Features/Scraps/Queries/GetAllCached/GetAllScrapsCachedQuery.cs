using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ScrapManagement.Application.Interfaces.CacheRepositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Features.Scraps.Queries.GetAllCached
{
    public class GetAllScrapsCachedQuery : IRequest<Result<List<GetAllScrapsCachedResponse>>>
    {
        public GetAllScrapsCachedQuery()
        {
        }
    }

    public class GetAllScrapsCachedQueryHandler : IRequestHandler<GetAllScrapsCachedQuery, Result<List<GetAllScrapsCachedResponse>>>
    {
        private readonly IScrapCacheRepository _scrapCache;
        private readonly IMapper _mapper;

        public GetAllScrapsCachedQueryHandler(IScrapCacheRepository scrapCache, IMapper mapper)
        {
            _scrapCache = scrapCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllScrapsCachedResponse>>> Handle(GetAllScrapsCachedQuery request, CancellationToken cancellationToken)
        {
            var scrapList = await _scrapCache.GetCachedListAsync();
            var mappedScraps = _mapper.Map<List<GetAllScrapsCachedResponse>>(scrapList);
            return Result<List<GetAllScrapsCachedResponse>>.Success(mappedScraps);
        }
    }
}