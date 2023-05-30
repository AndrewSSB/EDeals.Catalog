using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDeals.Catalog.Application.Models.ProductModels
{
    public class AddProductModel
    {
        public int CategoryId { get; set; }
        [NotMapped]
        [FromForm]
        public IFormFile MainImage { get; set; }
        [FromForm]
        [NotMapped]
        public IFormFileCollection Images { get; set; }
    }
}
