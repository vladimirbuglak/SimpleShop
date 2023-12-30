using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using SimpleShop.Application.Modify.Interfaces;

namespace SimpleShop.Infrastructure.AWS;

public class AwsS3Service : IFilesStorage
{
    private AwsS3Config Config { get; }
    
    public AwsS3Service(AwsS3Config config)
    {
        Config = config ?? throw new ArgumentNullException(nameof(config));
    }
    
    public async Task<string> UploadAsync(UploadFile file)
    {
        using (var client = new AmazonS3Client(Config.AccessKeyId, Config.SecretAccessKey, RegionEndpoint.EUNorth1))
        {
            using (var newMemoryStream = new MemoryStream(file.Content))
            {
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = newMemoryStream,
                    Key = file.Name,
                    BucketName = Config.ProductFilesBucketName,
                    CannedACL = S3CannedACL.PublicRead
                };

                var fileTransferUtility = new TransferUtility(client);
                
                await fileTransferUtility.UploadAsync(uploadRequest);

                return $"https://{Config.ProductFilesBucketName}.s3.amazonaws.com/{file.Name}";
            }
        }
    }
}