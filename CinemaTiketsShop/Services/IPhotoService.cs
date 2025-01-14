using CloudinaryDotNet.Actions;

namespace CinemaTiketsShop.Services
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string? publicId);

        Task<ImageUploadResult> UploadPhotoWithUrlAsync(string PicUrl);
    }
}
