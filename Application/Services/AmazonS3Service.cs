using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        #region Atributos
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string BucketName { get; set; }
        public string Region { get; set; }
        public BasicAWSCredentials AwsCredentials { get; set; }

        private readonly IAmazonS3 _awsS3Client;
        #endregion

        #region Construtor
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
        #endregion

        #region Métodos
        public async Task<byte[]> GetFileAsync(string key)
        {
            var request = new GetObjectRequest
            {
                BucketName = BucketName,
                Key = key
            };

            using var response = await _awsS3Client.GetObjectAsync(request);
            using var responseStream = response.ResponseStream;
            using var memoryStream = new MemoryStream();

            await responseStream.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }

        public async Task<List<byte[]>> GetFilesAsync(List<string> keys)
        {
            var files = new List<byte[]>();
            foreach (var key in keys)
            {
                files.Add(await GetFileAsync(key));
            }
            return files;
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

        public async Task<bool> UploadFilesAsync(Dictionary<string, IFormFile> files)
        {
            foreach (var file in files)
            {
                await UploadFileAsync(file.Key, file.Value);
            }

            return true;
        }

        public async Task<bool> DeleteFileAsync(string key)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = BucketName,
                Key = key
            };

            await _awsS3Client.DeleteObjectAsync(request);

            return true;
        }

        public async Task<bool> DeleteFilesAsync(List<string> keys)
        {
            foreach (var key in keys)
            {
                await DeleteFileAsync(key);
            }

            return true;
        }
        #endregion

    }
}
