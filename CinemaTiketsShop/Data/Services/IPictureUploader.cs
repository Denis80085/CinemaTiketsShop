using CinemaTiketsShop.Helpers;
using CloudinaryDotNet.Actions;

namespace CinemaTiketsShop.Data.Services
{
    public interface IPictureUploader
    {
        Task<UploadedImageResult> UpdateImageFromFileAsync(IFormFile image, string? OldPublicId);
        Task<UploadedImageResult> UpdateImageFromUrlAsync(string PictureUrl, string? OldPublicId);
    }
}
