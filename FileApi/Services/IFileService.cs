using System.Collections.Generic;
using FileApi.Models;
using Microsoft.AspNetCore.Http;

namespace FileApi.Services
{
    public interface IFileService
    {
        File UploadFile(IFormFile formFile);
        List<File> UploadMultipleFiles(List<IFormFile> formFiles);
        File UpdateFile(IFormFile formFile, string fileId);
        File GetFile(string fileId);
        File GetFileInfo(string fileId);
        void DeleteFile(string fileId);
    }
}