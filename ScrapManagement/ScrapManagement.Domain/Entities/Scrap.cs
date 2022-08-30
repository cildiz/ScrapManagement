using AspNetCoreHero.Abstractions.Domain;
using System;

namespace ScrapManagement.Domain.Entities
{
    public class Scrap : AuditableEntity
    {
        public int ScrapTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public virtual ScrapType ScrapType { get; set; }
    }
}