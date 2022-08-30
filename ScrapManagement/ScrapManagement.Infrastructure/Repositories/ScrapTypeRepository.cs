using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using ScrapManagement.Application.Interfaces.Repositories;
using ScrapManagement.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapManagement.Infrastructure.Repositories
{
    public class ScrapTypeRepository : IScrapTypeRepository
    {
        private readonly IRepositoryAsync<ScrapType> _repository;
        private readonly IDistributedCache _distributedCache;

        public ScrapTypeRepository(IDistributedCache distributedCache, IRepositoryAsync<ScrapType> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<ScrapType> ScrapTypes => _repository.Entities;

        public async Task DeleteAsync(ScrapType scrapType)
        {
            await _repository.DeleteAsync(scrapType);
            await _distributedCache.RemoveAsync(CacheKeys.ScrapTypeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ScrapTypeCacheKeys.GetKey(scrapType.Id));
        }

        public async Task<ScrapType> GetByIdAsync(int scrapTypeId)
        {
            return await _repository.Entities.Where(p => p.Id == scrapTypeId).FirstOrDefaultAsync();
        }

        public async Task<List<ScrapType>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(ScrapType scrapType)
        {
            await _repository.AddAsync(scrapType);
            await _distributedCache.RemoveAsync(CacheKeys.ScrapTypeCacheKeys.ListKey);
            return scrapType.Id;
        }

        public async Task UpdateAsync(ScrapType scrapType)
        {
            await _repository.UpdateAsync(scrapType);
            await _distributedCache.RemoveAsync(CacheKeys.ScrapTypeCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ScrapTypeCacheKeys.GetKey(scrapType.Id));
        }
    }
}