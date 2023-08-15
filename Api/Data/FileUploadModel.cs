using Microsoft.AspNetCore.Http;

namespace Api.Data
{
    public class FileUploadModel
    {
       public IFormFile FileDetails { get; set; }
       public FileType FileType { get; set; }
    }
}
