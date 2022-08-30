using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ScrapManagement.Application.Interfaces.Repositories;
using ScrapManagement.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Features.Scraps.Commands.Create
{
    public partial class CreateScrapCommand : IRequest<Result<int>>
    {
        public int ScrapTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

    }

    public class CreateScrapCommandHandler : IRequestHandler<CreateScrapCommand, Result<int>>
    {
        private readonly IScrapRepository _scrapRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateScrapCommandHandler(IScrapRepository scrapRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _scrapRepository = scrapRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateScrapCommand request, CancellationToken cancellationToken)
        {
            var scrap = _mapper.Map<Scrap>(request);
            await _scrapRepository.InsertAsync(scrap);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(scrap.Id);
        }
    }
}