using System;

namespace ScrapManagement.Application.Features.Scraps.Queries.GetAllPaged
{
    public class GetAllScrapsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}