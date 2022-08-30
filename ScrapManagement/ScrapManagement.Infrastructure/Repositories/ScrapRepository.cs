using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using ScrapManagement.Application.Interfaces.Repositories;
using ScrapManagement.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapManagement.Infrastructure.Repositories
{
    public class ScrapRepository : IScrapRepository
    {
        private readonly IRepositoryAsync<Scrap> _repository;
        private readonly IDistributedCache _distributedCache;

        public ScrapRepository(IDistributedCache distributedCache, IRepositoryAsync<Scrap> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Scrap> Scraps => _repository.Entities;

        public async Task DeleteAsync(Scrap scrap)
        {
            await _repository.DeleteAsync(scrap);
            await _distributedCache.RemoveAsync(CacheKeys.ScrapCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ScrapCacheKeys.GetKey(scrap.Id));
        }

        public async Task<Scrap> GetByIdAsync(int scrapId)
        {
            return await _repository.Entities.Where(p => p.Id == scrapId).FirstOrDefaultAsync();
        }

        public async Task<List<Scrap>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Scrap scrap)
        {
            await _repository.AddAsync(scrap);
            await _distributedCache.RemoveAsync(CacheKeys.ScrapCacheKeys.ListKey);
            return scrap.Id;
        }

        public async Task UpdateAsync(Scrap scrap)
        {
            await _repository.UpdateAsync(scrap);
            await _distributedCache.RemoveAsync(CacheKeys.ScrapCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.ScrapCacheKeys.GetKey(scrap.Id));
        }
    }
}