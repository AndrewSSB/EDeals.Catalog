using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDeals.Catalog.Application.Models.ProductModels
{
    public class AddProductModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int SellerId { get; set; }
        public int BrandId { get; set; }
        public int? Quantity { get; set; }
        [FromForm]
        [NotMapped]
        public IFormFileCollection Images { get; set; }
    }
}
