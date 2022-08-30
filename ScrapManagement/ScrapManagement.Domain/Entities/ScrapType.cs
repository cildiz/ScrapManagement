using AspNetCoreHero.Abstractions.Domain;

namespace ScrapManagement.Domain.Entities
{
    public class ScrapType : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}