namespace CinemaTiketsShop.Helpers
{
    public class UploadedImageResult
    {
        private string _ErrorMessage = string.Empty;
        private string _ErrorAt = string.Empty;


        public string PictureUrl { get; }
        public string? PublicId { get; }
        public bool Succeded { get; }

        public bool ErrorAcured { get; set; } = false;

        public string ErrorMessage { get => _ErrorMessage; set => _ErrorMessage = value; }

        public string ErrorAt { get => _ErrorAt; set => _ErrorAt = value; }


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

        public void SetError(string message, string errorAt)
        {
            ErrorAcured = true;
            ErrorMessage = message;
            ErrorAt = errorAt;
        }
    }
}
