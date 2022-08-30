using AspNetCoreHero.Results;
using MediatR;
using ScrapManagement.Application.Exceptions;
using ScrapManagement.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Features.Scraps.Commands.Update
{
    public class UpdateScrapImageCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }

        public class UpdateScrapImageCommandHandler : IRequestHandler<UpdateScrapImageCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IScrapRepository _scrapRepository;

            public UpdateScrapImageCommandHandler(IScrapRepository scrapRepository, IUnitOfWork unitOfWork)
            {
                _scrapRepository = scrapRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateScrapImageCommand command, CancellationToken cancellationToken)
            {
                var scrap = await _scrapRepository.GetByIdAsync(command.Id);

                if (scrap == null)
                {
                    throw new ApiException($"Hurda Bulunamadı.");
                }
                else
                {
                    scrap.Image = command.Image;
                    await _scrapRepository.UpdateAsync(scrap);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(scrap.Id);
                }
            }
        }
    }
}