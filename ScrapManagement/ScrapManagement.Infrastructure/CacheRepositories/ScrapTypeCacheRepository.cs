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
    public class ScrapTypeCacheRepository : IScrapTypeCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IScrapTypeRepository _scrapTypeRepository;

        public ScrapTypeCacheRepository(IDistributedCache distributedCache, IScrapTypeRepository scrapTypeRepository)
        {
            _distributedCache = distributedCache;
            _scrapTypeRepository = scrapTypeRepository;
        }

        public async Task<ScrapType> GetByIdAsync(int scrapTypeId)
        {
            string cacheKey = ScrapTypeCacheKeys.GetKey(scrapTypeId);
            var scrapType = await _distributedCache.GetAsync<ScrapType>(cacheKey);
            if (scrapType == null)
            {
                scrapType = await _scrapTypeRepository.GetByIdAsync(scrapTypeId);
                Throw.Exception.IfNull(scrapType, "Hurda Tipi", "Hurda Tipi Bulunamadı.");
                await _distributedCache.SetAsync(cacheKey, scrapType);
            }
            return scrapType;
        }

        public async Task<List<ScrapType>> GetCachedListAsync()
        {
            string cacheKey = ScrapTypeCacheKeys.ListKey;
            var scrapTypeList = await _distributedCache.GetAsync<List<ScrapType>>(cacheKey);
            if (scrapTypeList == null)
            {
                scrapTypeList = await _scrapTypeRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, scrapTypeList);
            }
            return scrapTypeList;
        }
    }
}