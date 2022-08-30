using ScrapManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Interfaces.CacheRepositories
{
    public interface IScrapTypeCacheRepository
    {
        Task<List<ScrapType>> GetCachedListAsync();

        Task<ScrapType> GetByIdAsync(int brandId);
    }
}