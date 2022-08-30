using ScrapManagement.Application.Interfaces.Shared;
using System;

namespace ScrapManagement.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}