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

        public async Task<UploadedImageResult> UploadImageFromFileAsync(object Model)
        {
            try 
            {
                var res = new ImageUploadResult();

                if (Model is EditProducerViewModel)
                {
                    var ProducerVM = (EditProducerViewModel)Model;

                    if (ProducerVM.Foto != null)
                    { 
                        res = await _photoService.UploadPhotoAsync(ProducerVM.Foto);
                    }

                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var uploadResult = new UploadedImageResult(res.Url.AbsoluteUri, res.PublicId, true);

                        await _photoService.DeletePhotoAsync(ProducerVM.PublicId);

                        return uploadResult;
                    }
                }

                return new(false);
            }
            catch 
            {
                return new(false);
            }
        }

        public async Task<UploadedImageResult> UploadImageFromUrlAsync(object Model)
        {
            try 
            {
                var res = new ImageUploadResult();

                if (Model is EditProducerViewModel)
                {
                    var ProducerVM = (EditProducerViewModel)Model;

                    res = await _photoService.UploadPhotoWithUrlAsync(ProducerVM.PictureUrl);

                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var uploadResult = new UploadedImageResult(res.Url.AbsoluteUri, res.PublicId, true);

                        await _photoService.DeletePhotoAsync(ProducerVM.PublicId);

                        return uploadResult;
                    }

                }

                return new(false);
            }
            catch 
            {  
                return new(false); 
            }
            
        }
    }
}
