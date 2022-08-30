using AspNetCoreHero.Results;
using MediatR;
using ScrapManagement.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapManagement.Application.Features.Scraps.Commands.Update
{
    public class UpdateScrapCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int ScrapTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public class UpdateScrapCommandHandler : IRequestHandler<UpdateScrapCommand, Result<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IScrapRepository _scrapRepository;

            public UpdateScrapCommandHandler(IScrapRepository scrapRepository, IUnitOfWork unitOfWork)
            {
                _scrapRepository = scrapRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateScrapCommand command, CancellationToken cancellationToken)
            {
                var scrap = await _scrapRepository.GetByIdAsync(command.Id);

                if (scrap == null)
                {
                    return Result<int>.Fail($"Hurda Bulunamadı.");
                }
                else
                {
                    scrap.Name = command.Name ?? scrap.Name;
                    scrap.DateTime = (command.DateTime == null) ? scrap.DateTime : command.DateTime;
                    scrap.Description = command.Description ?? scrap.Description;
                    scrap.ScrapTypeId = (command.ScrapTypeId == 0) ? scrap.ScrapTypeId : command.ScrapTypeId;
                    await _scrapRepository.UpdateAsync(scrap);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(scrap.Id);
                }
            }
        }
    }
}