using Microsoft.AspNetCore.Http;

namespace EDeals.Catalog.Application.Interfaces
{
    public interface IUploadPhotosService
    {
        Task UploadPhotos(IFormFileCollection images, Guid productId, string productName);
    }
}
