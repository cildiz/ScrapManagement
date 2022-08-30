using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace ScrapManagement.Web.Areas.Apps.Models
{
    public class ScrapViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int ScrapTypeId { get; set; }
        public SelectList ScrapTypes { get; set; }
    }
}