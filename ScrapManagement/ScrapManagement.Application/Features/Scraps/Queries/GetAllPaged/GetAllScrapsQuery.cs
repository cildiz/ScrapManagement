using AspNetCoreHero.Results;
using MediatR;
using ScrapManagement.Application.Extensions;
using ScrapManagement.Application.Interfaces.Repositories;
using ScrapManagement.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Features.Scraps.Queries.GetAllPaged
{
    public class GetAllScrapsQuery : IRequest<PaginatedResult<GetAllScrapsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllScrapsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GGetAllScrapsQueryHandler : IRequestHandler<GetAllScrapsQuery, PaginatedResult<GetAllScrapsResponse>>
    {
        private readonly IScrapRepository _repository;

        public GGetAllScrapsQueryHandler(IScrapRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllScrapsResponse>> Handle(GetAllScrapsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Scrap, GetAllScrapsResponse>> expression = e => new GetAllScrapsResponse
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                DateTime = e.DateTime,
                Code = e.Code
            };
            var paginatedList = await _repository.Scraps
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}