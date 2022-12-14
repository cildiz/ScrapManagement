using System;

namespace ScrapManagement.Application.Features.Scraps.Queries.GetAllCached
{
    public class GetAllScrapsCachedResponse
    {
        public int Id { get; set; }
        public int ScrapTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}