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
        private const string FileNameDelimiter = ".";

        /*
        public static bool CheckPathIsValid(string fullPath)
        {
            if (!System.IO.File.Exists(fullPath))
            {
                new Uri(fullPath);
                Path.GetFullPath(fullPath);
                return true;
            }

            return false;
        }
        */

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

        public static void UpdatedFileByFormFile(IFormFile formFile, File currentFile, string newFileNameInFs,
            bool isStoredInFileSystem)
        {
            currentFile.ContentType = formFile.ContentType;
            currentFile.ContentDisposition = formFile.ContentDisposition;
            currentFile.FileSize = formFile.Length;
            currentFile.FileName = formFile.FileName;
            currentFile.IsStoredInFileSystem = isStoredInFileSystem;
            currentFile.FileNameInFs = newFileNameInFs;
            currentFile.Status = StatusEnums.Status.Updated;
            currentFile.UpdatedTs = DateTime.Now;
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

        public static void Save2FsByStreaming(string combined, IFormFile formFile)
        {
            using var stream = System.IO.File.Create(combined);
            formFile.CopyTo(stream);
        }

        public static string GenerateFileNameForFs(IFormFile formFile)
        {
            var extension = Path.GetExtension(formFile.FileName);
            var fileName = Guid.NewGuid().ToString();
            var fileNameInFs = String.Concat(fileName, FileNameDelimiter, extension);
            return fileNameInFs;
        }

        public static string GetFilePathInFs(string rootFileDirectory, string fileNameInFs)
        {
            return Path.Combine(rootFileDirectory, fileNameInFs);
        }
    }
}