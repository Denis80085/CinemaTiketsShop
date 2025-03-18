using CinemaTiketsShop.Helpers;
using CinemaTiketsShop.Services;
using CloudinaryDotNet.Actions;

namespace CinemaTiketsShop.Data.Services
{
    public class PictureUploader : IPictureUploader
    {
        private readonly IPhotoService _photoService;

        public PictureUploader(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        public async Task<UploadedImageResult> UploadNewImageFromUrl(string? pictureUrl)
        {
            var uploadFaieled = new UploadedImageResult(false);

            if (string.IsNullOrWhiteSpace(pictureUrl))
            {
                uploadFaieled.SetError($"Please insert a link of a image of type .jpg, .png, .webp or .svg", "PictureUrl");

                return uploadFaieled;
            }

            if (!await PictureUrl.isValid(pictureUrl))
            {
                uploadFaieled.SetError($"Url validation failed. Make sure that it is pointed to a image of type .jpg, .png, .webp or .svg", "PictureUrl");

                return uploadFaieled;
            }

            try
            {
                var res = new ImageUploadResult();

                res = await _photoService.UploadPhotoWithUrlAsync(pictureUrl);

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var uploadResult = new UploadedImageResult(res.Url.AbsoluteUri, res.PublicId, true);

                    return uploadResult;
                }

                uploadFaieled.SetError($"Upload faield with code {res.StatusCode}", "PictureUrl");

                return uploadFaieled;
            }
            catch
            {
                uploadFaieled.SetError("Unexcpected error while uploading your picture", "PictureUrl");

                return uploadFaieled;
            }

        }

        public async Task<UploadedImageResult> UploadNewImageFromFileAsync(IFormFile? image)
        {
            var result = new ImageUploadResult();
            var uploadFaieled = new UploadedImageResult(false);

            try
            {
                if (image != null)
                {
                    result = await _photoService.UploadPhotoAsync(image);
                }
                else
                {
                    uploadFaieled.SetError($"Plese select a foto from your device", "Foto");

                    return uploadFaieled;
                }

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new UploadedImageResult(result.Url.AbsoluteUri, result.PublicId, true);
                }
                else
                {

                    uploadFaieled.SetError($"Upload faield with code {result.StatusCode}", "Foto");

                    return uploadFaieled;
                }
            }
            catch
            {

                uploadFaieled.SetError("Unexcpected error while uploading your picture", "Foto");

                return uploadFaieled;
            }
        }

        public async Task<UploadedImageResult> UpdateImageFromFileAsync(IFormFile image, string? OldPublicId)
        {
            try
            {
                var res = new ImageUploadResult();


                res = await _photoService.UploadPhotoAsync(image);


                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var uploadResult = new UploadedImageResult(res.Url.AbsoluteUri, res.PublicId, true);

                    await _photoService.DeletePhotoAsync(OldPublicId);

                    return uploadResult;
                }

                var uploadFaieled = new UploadedImageResult(false);

                uploadFaieled.SetError($"Upload faield with code {res.StatusCode}", "Foto");

                return uploadFaieled;
            }
            catch
            {
                var uploadFaieled = new UploadedImageResult(false);

                uploadFaieled.SetError("Unexcpected error while uploading your picture", "Foto");

                return uploadFaieled;
            }
        }

        public async Task<UploadedImageResult> UpdateImageFromUrlAsync(string? pictureUrl, string? OldPublicId)
        {
            var uploadFaieled = new UploadedImageResult(false);

            if (string.IsNullOrWhiteSpace(pictureUrl))
            {
                uploadFaieled.SetError($"Please insert a link of a image of type .jpg, .png, .webp or .svg", "PictureUrl");

                return uploadFaieled;
            }

            if (!await PictureUrl.isValid(pictureUrl))
            {
                uploadFaieled.SetError($"Url validation failed. Make sure that it is pointed to a image of type .jpg, .png, .webp or .svg", "PictureUrl");

                return uploadFaieled;
            }

            try
            {
                var res = new ImageUploadResult();

                res = await _photoService.UploadPhotoWithUrlAsync(pictureUrl);

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var uploadResult = new UploadedImageResult(res.Url.AbsoluteUri, res.PublicId, true);

                    await _photoService.DeletePhotoAsync(OldPublicId);

                    return uploadResult;
                }

                uploadFaieled.SetError($"Upload faield with code {res.StatusCode}", "PictureUrl");

                return uploadFaieled;
            }
            catch
            {
                uploadFaieled.SetError("Unexcpected error while uploading your picture", "PictureUrl");

                return uploadFaieled;
            }

        }
    }
}
