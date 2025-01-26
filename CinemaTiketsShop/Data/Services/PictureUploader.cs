using CinemaTiketsShop.Helpers;
using CinemaTiketsShop.Services;
using CinemaTiketsShop.ViewModels.ProducerVMs;
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

                uploadFaieled.SetError();

                return uploadFaieled;
            }
            catch 
            {
                var uploadFaieled = new UploadedImageResult(false);

                uploadFaieled.SetError();

                return uploadFaieled;
            }
        }

        public async Task<UploadedImageResult> UpdateImageFromUrlAsync(string PictureUrl, string? OldPublicId)
        {
            try 
            {
                var res = new ImageUploadResult();

                res = await _photoService.UploadPhotoWithUrlAsync(PictureUrl);

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var uploadResult = new UploadedImageResult(res.Url.AbsoluteUri, res.PublicId, true);

                    await _photoService.DeletePhotoAsync(OldPublicId);

                    return uploadResult;
                }

                var uploadFaieled = new UploadedImageResult(false);

                uploadFaieled.SetError();

                return uploadFaieled;
            }
            catch 
            {
                var uploadFaieled = new UploadedImageResult(false);

                uploadFaieled.SetError();

                return uploadFaieled; 
            }
            
        }
    }
}
