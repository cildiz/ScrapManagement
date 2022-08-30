using AspNetCoreHero.Results;
using MediatR;
using ScrapManagement.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Features.Scraps.Commands.Delete
{
    public class DeleteScrapCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteScrapCommandHandler : IRequestHandler<DeleteScrapCommand, Result<int>>
        {
            private readonly IScrapRepository _scrapRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteScrapCommandHandler(IScrapRepository scrapRepository, IUnitOfWork unitOfWork)
            {
                _scrapRepository = scrapRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteScrapCommand command, CancellationToken cancellationToken)
            {
                var scrap = await _scrapRepository.GetByIdAsync(command.Id);
                await _scrapRepository.DeleteAsync(scrap);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(scrap.Id);
            }
        }
    }
}