using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AmazonS3Service : IAmazonS3Service
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string BucketName { get; set; }
        public string Region { get; set; }
        public BasicAWSCredentials AwsCredentials { get; set; }

        private readonly IAmazonS3 _awsS3Client;

        public AmazonS3Service(IConfiguration configuration)
        {
            AccessKey = configuration["S3:AccessKey"];
            SecretKey = configuration["S3:SecretKey"];
            BucketName = configuration["S3:BucketName"];
            Region = configuration["S3:Region"];

            AwsCredentials = new BasicAWSCredentials(AccessKey, SecretKey);
            var config = new AmazonS3Config
            {
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(Region)
            };
            _awsS3Client = new AmazonS3Client(AwsCredentials, config);
        }

        public async Task<bool> UploadFileAsync(string key, IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);

            var fileTransferUtility = new TransferUtility(_awsS3Client);

            await fileTransferUtility.UploadAsync(new TransferUtilityUploadRequest
            {
                InputStream = memoryStream,
                Key = key,
                BucketName = BucketName,
                ContentType = file.ContentType
            });

            return true;
        }
    }
}
