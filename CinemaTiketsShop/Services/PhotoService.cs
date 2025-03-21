﻿using CinemaTiketsShop.Helpers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace CinemaTiketsShop.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILogger _logger;

        public PhotoService(IOptions<CloudinarySettings> config, ILogger<PhotoService> logger)
        {
            var acc = new Account(
                    config.Value.CloudName,
                    config.Value.ApiKey,
                    config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);

            _logger = logger;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string? publicId)
        {
            if (string.IsNullOrWhiteSpace(publicId))
            {
                return new DeletionResult();
            }

            DeletionParams deleteParams = new DeletionParams(publicId);

            var DeleteResult = await _cloudinary.DestroyAsync(deleteParams);

            return DeleteResult;
        }

        public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile file)
        {
            var UploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();

                var UploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.Name, stream),
                    Transformation = new Transformation().Width(250).Crop("fill").Gravity("face"),
                    AllowedFormats = ["jpg", "png", "svg", "webp", "jpeg"]
                };

                UploadResult = await _cloudinary.UploadAsync(UploadParams);
            }
            else
            {
                _logger.LogWarning("Uploading image failed");
            }

            return UploadResult;
        }

        public async Task<ImageUploadResult> UploadPhotoWithUrlAsync(string PicUrl)
        {
            var UploadResult = new ImageUploadResult();

            if (!string.IsNullOrWhiteSpace(PicUrl))
            {
                var UploadParams = new ImageUploadParams
                {
                    File = new FileDescription(PicUrl),
                    Transformation = new Transformation().Width(250).Crop("fill").Gravity("face"),
                    AllowedFormats = ["jpg", "png", "svg", "webp"]
                };

                UploadResult = await _cloudinary.UploadAsync(UploadParams);
            }
            else
            {
                _logger.LogWarning("Uploading image failed");
            }

            return UploadResult;
        }
    }
}
