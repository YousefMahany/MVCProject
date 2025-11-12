using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.Services.AttachmentsService
{
    public interface IAttachmentService
    {
        public string? Upload(IFormFile file, string FolderName);
        public bool Delete(string filePath);
    }
}
