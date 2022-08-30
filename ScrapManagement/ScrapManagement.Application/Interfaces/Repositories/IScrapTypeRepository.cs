using ScrapManagement.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Interfaces.Repositories
{
    public interface IScrapTypeRepository
    {
        IQueryable<ScrapType> ScrapTypes { get; }

        Task<List<ScrapType>> GetListAsync();

        Task<ScrapType> GetByIdAsync(int scrapTypeId);

        Task<int> InsertAsync(ScrapType scrapType);

        Task UpdateAsync(ScrapType scrapType);

        Task DeleteAsync(ScrapType scrapType);
    }
}