using AspNetCoreHero.Results;
using MediatR;
using ScrapManagement.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Features.ScrapTypes.Commands.Delete
{
    public class DeleteScrapTypeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteScrapTypeCommandHandler : IRequestHandler<DeleteScrapTypeCommand, Result<int>>
        {
            private readonly IScrapTypeRepository _scrapTypeRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteScrapTypeCommandHandler(IScrapTypeRepository scrapTypeRepository, IUnitOfWork unitOfWork)
            {
                _scrapTypeRepository = scrapTypeRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteScrapTypeCommand command, CancellationToken cancellationToken)
            {
                var scrap = await _scrapTypeRepository.GetByIdAsync(command.Id);
                await _scrapTypeRepository.DeleteAsync(scrap);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(scrap.Id);
            }
        }
    }
}