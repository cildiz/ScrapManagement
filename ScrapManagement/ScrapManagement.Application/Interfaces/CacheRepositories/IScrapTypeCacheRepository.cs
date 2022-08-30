using ScrapManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Interfaces.CacheRepositories
{
    public interface IScrapCacheRepository
    {
        Task<List<Scrap>> GetCachedListAsync();

        Task<Scrap> GetByIdAsync(int brandId);
    }
}