using CinemaTiketsShop.Helpers;

namespace CinemaTiketsShop.Data.Services
{
    public interface IPictureUploader
    {
        Task<UploadedImageResult> UpdateImageFromFileAsync(IFormFile image, string? OldPublicId);
        Task<UploadedImageResult> UpdateImageFromUrlAsync(string? PictureUrl, string? OldPublicId);

        Task<UploadedImageResult> UploadNewImageFromUrl(string? pictureUrl);
        Task<UploadedImageResult> UploadNewImageFromFileAsync(IFormFile? image);
    }
}
