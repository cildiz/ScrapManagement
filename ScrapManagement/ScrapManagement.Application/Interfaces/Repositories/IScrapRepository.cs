using ScrapManagement.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Interfaces.Repositories
{
    public interface IScrapRepository
    {
        IQueryable<Scrap> Scraps { get; }

        Task<List<Scrap>> GetListAsync();

        Task<Scrap> GetByIdAsync(int ScrapId);

        Task<int> InsertAsync(Scrap Scrap);

        Task UpdateAsync(Scrap Scrap);

        Task DeleteAsync(Scrap Scrap);
    }
}