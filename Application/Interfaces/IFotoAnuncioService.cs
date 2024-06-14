using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFotoAnuncioService
    {
        Task<bool> AddArchiveAsync(int anuncioId, IFormFile file, int sequencia);
        Task<List<byte[]>> GetArchivesAsync(int anuncioId);
    }
}
