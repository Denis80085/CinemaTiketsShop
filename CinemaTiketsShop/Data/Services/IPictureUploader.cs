using CinemaTiketsShop.Helpers;
using CloudinaryDotNet.Actions;

namespace CinemaTiketsShop.Data.Services
{
    public interface IPictureUploader
    {
        Task<UploadedImageResult> UploadImageFromFileAsync(object Model);
        Task<UploadedImageResult> UploadImageFromUrlAsync(object Model);
    }
}
