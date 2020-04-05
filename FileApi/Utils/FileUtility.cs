using System;
using System.IO;
using FileApi.Enums;
using FileApi.Models;
using Microsoft.AspNetCore.Http;
using File = FileApi.Models.File;

namespace FileApi.Utils
{
    public static class FileUtility
    {
        public static bool CheckPathIsValid(string fullPath)
        {
            if (!System.IO.File.Exists(fullPath))
            {
                new Uri(fullPath);
                Path.GetDirectoryName(fullPath);
                Path.GetFullPath(fullPath);
                return true;
            }

            return false;
        }

        public static File FormFile2File(IFormFile formFile, bool storeInFileSystem)
        {
            var file = new File
            {
                ContentType = formFile.ContentType,
                ContentDisposition = formFile.ContentDisposition,
                FileSize = formFile.Length,
                FileName = formFile.FileName,
                IsStoredInFileSystem = storeInFileSystem,
                Status = StatusEnums.Status.Created,
                CreatedTs = DateTime.Now,
                UpdatedTs = DateTime.Now
            };
            if (!storeInFileSystem)
                CopyFileContentFromFormFile(formFile, file);

            return file;
        }

        public static void CopyFileContentFromFormFile(IFormFile formFile, File file)
        {
            if (formFile.Length > 0)
            {
                using var ms = new MemoryStream();
                formFile.CopyTo(ms);
                var fileBytes = ms.ToArray();
                file.FileContent = new FileBytes {ContentBytes = fileBytes};
            }
        }

        public static void Save2Fs(string combined, IFormFile formFile)
        {
            using var stream = System.IO.File.Create(combined);
            formFile.CopyTo(stream);
        }
    }
}