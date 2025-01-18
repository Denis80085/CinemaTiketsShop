namespace CinemaTiketsShop.Helpers
{
    public class UploadedImageResult
    {
        public string PictureUrl { get; }
        public string? PublicId { get; }
        public bool Succeded { get; }

        public UploadedImageResult(string picUrl, string pId, bool succed)
        {
            PictureUrl = picUrl;
            PublicId = pId;
            Succeded = succed;
        }

        public UploadedImageResult(bool succed = false, string picUrl = "") 
        {
            Succeded = succed;
            PictureUrl = picUrl;
        }
    }
}
