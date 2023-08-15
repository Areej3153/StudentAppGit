using Api.Data;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IFileService
    {
        public Task<int> PostFileAsync(IFormFile fileData, FileType fileType);
        Task<FileDetails> DeleteAsync(int id);

        public FileDetails GetAsync(int id);
    }
}
