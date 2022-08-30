using AspNetCoreHero.Extensions.Caching;
using AspNetCoreHero.ThrowR;
using Microsoft.Extensions.Caching.Distributed;
using ScrapManagement.Application.Interfaces.CacheRepositories;
using ScrapManagement.Application.Interfaces.Repositories;
using ScrapManagement.Domain.Entities;
using ScrapManagement.Infrastructure.CacheKeys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrapManagement.Infrastructure.CacheRepositories
{
    public class ScrapCacheRepository : IScrapCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IScrapRepository _scrapRepository;

        public ScrapCacheRepository(IDistributedCache distributedCache, IScrapRepository scrapRepository)
        {
            _distributedCache = distributedCache;
            _scrapRepository = scrapRepository;
        }

        public async Task<Scrap> GetByIdAsync(int scrapId)
        {
            string cacheKey = ScrapCacheKeys.GetKey(scrapId);
            var scrap = await _distributedCache.GetAsync<Scrap>(cacheKey);
            if (scrap == null)
            {
                scrap = await _scrapRepository.GetByIdAsync(scrapId);
                Throw.Exception.IfNull(scrap, "Hurda", "Hurda Bulunamadı.");
                await _distributedCache.SetAsync(cacheKey, scrap);
            }
            return scrap;
        }

        public async Task<List<Scrap>> GetCachedListAsync()
        {
            string cacheKey = ScrapCacheKeys.ListKey;
            var scrapList = await _distributedCache.GetAsync<List<Scrap>>(cacheKey);
            if (scrapList == null)
            {
                scrapList = await _scrapRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, scrapList);
            }
            return scrapList;
        }
    }
}