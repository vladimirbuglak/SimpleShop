namespace SimpleShop.Infrastructure.AWS;

public class AwsS3Config
{
    public string AccessKeyId { get; set; }
    
    public string SecretAccessKey { get; set; }
    
    public string ProductFilesBucketName { get; set; }
}