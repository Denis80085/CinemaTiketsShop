namespace CinemaTiketsShop.IdentityServerData.Results
{
    public class RegisterResult
    {
        public bool Succeeded { get; set; } = false;
        public Queue<string> Errors { get; set; } = new();
    }
}
