using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.Services.AttachmentsService
{
    public class AttachmentService : IAttachmentService
    {
        List<string> AllowedExtentions = [".png", ".jpg", ".jpeg"];
        const int MaxSize = 2_097_152;

        public string? Upload(IFormFile file, string FolderName)
        {
            //1-Check Extention
            var Extention = Path.GetExtension(file.FileName);
            if(!AllowedExtentions.Contains(Extention)) return null;
            //2-Check Size
            if(file.Length == 0 || file.Length > MaxSize) return null;
            //3-Get Located Folder Path
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            //Make Attachment Name Unique
            var fileName = $"{Guid.NewGuid()}-{file.FileName}";
            //5-Get File Path
            var filePath = Path.Combine(FolderPath, fileName);
            //6-Create File Stream To Copy File [Unmanaged]
            using FileStream FS = new FileStream(filePath, FileMode.Create);
            //7-Use Stream To Copy File
            file.CopyTo(FS);
            //8-Return File Name To Store In Database
            return fileName;
        }
        public bool Delete(string filePath)
        {
          if(!File.Exists(filePath)) return false;
            else
            {
                File.Delete(filePath);
            }
          return true;
        }
    }
}
