using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ScrapManagement.Application.Interfaces.Repositories;
using ScrapManagement.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Features.ScrapTypes.Commands.Create
{
    public partial class CreateScrapTypeCommand : IRequest<Result<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateScrapTypeCommandHandler : IRequestHandler<CreateScrapTypeCommand, Result<int>>
    {
        private readonly IScrapTypeRepository _scrapTypeRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateScrapTypeCommandHandler(IScrapTypeRepository scrapTypeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _scrapTypeRepository = scrapTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateScrapTypeCommand request, CancellationToken cancellationToken)
        {
            var scrap = _mapper.Map<ScrapType>(request);
            await _scrapTypeRepository.InsertAsync(scrap);
            await _unitOfWork.Commit(cancellationToken);
            return Result<int>.Success(scrap.Id);
        }
    }
}