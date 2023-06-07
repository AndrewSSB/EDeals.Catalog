using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EDeals.Catalog.Application.Interfaces;
using EDeals.Catalog.Application.Models.ProductModels;
using EDeals.Catalog.Application.Settings;
using EDeals.Catalog.Domain.Entities.ItemEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EDeals.Catalog.Application.Services
{
    public class UploadPhotosService : IUploadPhotosService
    {
        private readonly AzureSettings _azureSettings;
        private readonly ILogger<UploadPhotosService> _logger;
        private readonly IGenericRepository<Image> _imageRepository;

        private const string containerName = "edeals";

        public UploadPhotosService(IOptions<AzureSettings> azureSettings, ILogger<UploadPhotosService> logger, IGenericRepository<Image> imageRepository)
        {
            _azureSettings = azureSettings.Value;
            _logger = logger;
            _imageRepository = imageRepository;
        }

        public async Task UploadPhotos(IFormFileCollection images, Guid productId, string productName)
        {
            string[] extensions = { "jpeg", "jpg", "png" };

            var connectionString = _azureSettings.ConnectionString;
            var blobServiceClient = new BlobServiceClient(connectionString);

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            if (!containerClient.Exists())
            {
                containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName, PublicAccessType.BlobContainer);
            }

            var index = 0;

            foreach (var image in images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!extensions.Contains(extension))
                {
                    _logger.LogError("Invalid extension");
                    return;
                }

                var blobHttpHeader = new BlobHttpHeaders { ContentType = "image/jpeg" };
                using Stream stream = new MemoryStream();                
                await image.CopyToAsync(stream);
                stream.Position = 0;

                var uniqueImageName = $"{productId}_{productName}_{index}";
                index++;
                var blob = containerClient.GetBlobClient(uniqueImageName);
                
                await blob.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = blobHttpHeader });

                var imageToAdd = new Image
                {
                    ImageUrl = blob.Uri.ToString(),
                    ProductId = productId
                };

                await _imageRepository.AddAsync(imageToAdd);
            }
        }
    }
}
