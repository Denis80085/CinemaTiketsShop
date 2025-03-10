namespace CinemaTiketsShop.Configs
{
    public class CognitoAppConfig
    {
        public required string Region { get; set; }
        public required string UserPoolId { get; set; }
        public required string AppClientId { get; set; }
        public required string AccessKeyId { get; set; }
        public required string AccessSecretKey { get; set; }
        public required string AppClientSecret { get; set; }
    }
}
