namespace CinemaTiketsShop.Helpers
{
    public class UploadedImageResult
    {
        public string PictureUrl { get; }
        public string? PublicId { get; }
        public bool Succeded { get; }

        public bool ErrorAcured { get; set; } = false;

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

        public void SetError() 
        {
            ErrorAcured = true;
        }
    }
}
