using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ScrapManagement.Application.Interfaces.CacheRepositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Features.ScrapTypes.Queries.GetAllCached
{
    public class GetAllScrapTypesCachedQuery : IRequest<Result<List<GetAllScrapTypesCachedResponse>>>
    {
        public GetAllScrapTypesCachedQuery()
        {
        }
    }

    public class GetAllScrapTypesCachedQueryHandler : IRequestHandler<GetAllScrapTypesCachedQuery, Result<List<GetAllScrapTypesCachedResponse>>>
    {
        private readonly IScrapTypeCacheRepository _scrapCache;
        private readonly IMapper _mapper;

        public GetAllScrapTypesCachedQueryHandler(IScrapTypeCacheRepository scrapCache, IMapper mapper)
        {
            _scrapCache = scrapCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllScrapTypesCachedResponse>>> Handle(GetAllScrapTypesCachedQuery request, CancellationToken cancellationToken)
        {
            var scrapTypeList = await _scrapCache.GetCachedListAsync();
            var mappedScrapTypes = _mapper.Map<List<GetAllScrapTypesCachedResponse>>(scrapTypeList);
            return Result<List<GetAllScrapTypesCachedResponse>>.Success(mappedScrapTypes);
        }
    }
}