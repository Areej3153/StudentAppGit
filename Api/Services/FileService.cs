using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class FileService : IFileService
    {
        private readonly Context _context;

        public FileService(Context context)
        {
            _context = context;
        }
        public async Task<FileDetails> DeleteAsync(int id)
        {
            var fileDetails = await _context.FileDetails.FirstOrDefaultAsync(x => x.ID == id);
            if (fileDetails == null)
            {
                return null;
            }

            var removedFileDetails = fileDetails;
            _context.FileDetails.Remove(removedFileDetails);
            await _context.SaveChangesAsync();
            return fileDetails;
        }

        public async Task<FileDetails> GetAsync(int id)
        {
            var fileDetails = await _context.FileDetails.FirstOrDefaultAsync(x => x.ID == id);
            if (fileDetails == null)
            {
                return null;
            }

            
           
           
           
            return fileDetails;
        }

        public async Task<Graduate> GetGraduateAsync(string id)
        {
            var graduate = await _context.Graduate.FirstOrDefaultAsync(x => x.Id == id);
            if (graduate == null)
            {
                return null;
            }





            return graduate;
        }

        public async Task<int> PostFileAsync(IFormFile fileData, FileType fileType)
        {
            try
            {
                var fileDetails = new FileDetails()
                {
                    ID = 0,
                    FileName = fileData.FileName,
                    FileType = fileType
                };

                using (var stream = new MemoryStream())
                {
                    if (stream.Length < 2097152)
                    {
                        fileData.CopyTo(stream);
                        fileDetails.FileData = stream.ToArray();

                        var result = _context.FileDetails.Add(fileDetails);
                        await _context.SaveChangesAsync();

                        return fileDetails.ID;
                    }
                    else return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        FileDetails IFileService.GetAsync(int id)
        {
            
            var fileDetails = _context.FileDetails.FirstOrDefault(x => x.ID == id);
            if (fileDetails == null)
            {
                return null;
            }





            return fileDetails;
        }
    }
}
