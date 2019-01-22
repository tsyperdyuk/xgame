using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Xgame.Core
{
    public static class FileHelper
    {
        public static string SavePictureFileUpload(this IFormFile file, IHostingEnvironment environment, string picturesFolderName = "Pictures")
        {
            return SaveFileUpload(file, environment, picturesFolderName);
        }

        public static string SaveFileUpload(this IFormFile file, IHostingEnvironment environment, string targetFolderName)
        {
            if (file == null)
            {
                return null;
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(environment.WebRootPath, targetFolderName, fileName);
            file.CopyTo(new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite));

            return fileName;
        }
    }
}