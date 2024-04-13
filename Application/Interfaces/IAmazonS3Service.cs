using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAmazonS3Service
    {
        Task<byte[]> GetFileAsync(string key);
        Task<List<byte[]>> GetFilesAsync(List<string> keys);
        Task<bool> UploadFileAsync(string key, IFormFile file);
        Task<bool> UploadFilesAsync(Dictionary<string, IFormFile> files);
        Task<bool> DeleteFileAsync(string key);
        Task<bool> DeleteFilesAsync(List<string> keys);
    }
}
