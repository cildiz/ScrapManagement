using AspNetCoreHero.Results;
using MediatR;
using ScrapManagement.Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Features.ScrapTypes.Commands.Update
{
    public class UpdateScrapTypeCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public class UpdateScrapCommandHandler : IRequestHandler<UpdateScrapTypeCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IScrapTypeRepository _scrapTypeRepository;

            public UpdateScrapCommandHandler(IScrapTypeRepository scrapTypeRepository, IUnitOfWork unitOfWork)
            {
                _scrapTypeRepository = scrapTypeRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateScrapTypeCommand command, CancellationToken cancellationToken)
            {
                var scrapType = await _scrapTypeRepository.GetByIdAsync(command.Id);

                if (scrapType == null)
                {
                    return Result<int>.Fail($"Hurda Tipi Bulunamadı.");
                }
                else
                {
                    scrapType.Name = command.Name ?? scrapType.Name;
                    scrapType.Description = command.Description ?? scrapType.Description;
                    await _scrapTypeRepository.UpdateAsync(scrapType);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(scrapType.Id);
                }
            }
        }
    }
}