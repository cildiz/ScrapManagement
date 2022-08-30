using AspNetCoreHero.EntityFrameworkCore.AuditTrail.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrapManagement.Application.DTOs;
using ScrapManagement.Application.Interfaces.Repositories;
using ScrapManagement.Application.Interfaces.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapManagement.Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryAsync<Audit> _repository;

        public LogRepository(IRepositoryAsync<Audit> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddLogAsync(string action, string userId)
        {
            var audit = new Audit()
            {
                Type = action,
                UserId = userId,
                DateTime = DateTime.UtcNow
            };
            await _repository.AddAsync(audit);
        }

        public async Task<List<AuditLogResponse>> GetAuditLogsAsync(string userId)
        {
            var logs = await _repository.Entities.Where(a => a.UserId == userId).OrderByDescending(a => a.Id).Take(250).ToListAsync();
            var mappedLogs = _mapper.Map<List<AuditLogResponse>>(logs);
            return mappedLogs;
        }
    }

    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<AuditLogResponse, Audit>().ReverseMap();
        }
    }
}