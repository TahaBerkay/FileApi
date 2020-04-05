using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileApi.Enums;
using FileApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using File = FileApi.Models.File;

namespace FileApi.Services
{
    public interface IFileService
    {
        File UploadFile2Db(IFormFile formFile);
        List<File> UploadMultipleFiles2Db(List<IFormFile> formFiles);
        File UpdateFile2Db(IFormFile formFile, string fileId);
        File GetFileFromDb(string fileId);
        File GetFileInfoFromDb(string fileId);
        void DeleteFileFromDb(string fileId);
    }

    public class FileService : IFileService
    {
        private readonly FileContext _context;

        public FileService(FileContext context)
        {
            _context = context;
        }

        public File UploadFile2Db(IFormFile formFile)
        {
            var file = AddFormFile2Db(formFile);
            _context.SaveChanges();
            file.FileContent = null;
            return file;
        }

        public List<File> UploadMultipleFiles2Db(List<IFormFile> formFiles)
        {
            var files = new List<File>();
            foreach (var formFile in formFiles)
            {
                var file = AddFormFile2Db(formFile);
                files.Add(file);
            }

            _context.SaveChanges();
            files.ForEach(file => file.FileContent = null);
            return files;
        }

        public File UpdateFile2Db(IFormFile formFile, string fileId)
        {
            var currentFile = _context.Files
                .Include(file => file.FileContent)
                .Single(e => e.Id == fileId);
            if (currentFile != null)
            {
                currentFile.ContentType = formFile.ContentType;
                currentFile.ContentDisposition = formFile.ContentDisposition;
                currentFile.FileSize = formFile.Length;
                currentFile.FileName = formFile.FileName;
                currentFile.IsStoredInFileSystem = false;
                currentFile.Status = StatusEnums.Status.Updated;
                currentFile.UpdatedTs = DateTime.Now;
                CopyFileContentFromFormFile(formFile, currentFile);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("FileId not found:" + fileId);
            }

            currentFile.FileContent = null;
            return currentFile;
        }

        public File GetFileFromDb(string fileId)
        {
            return _context.Files
                .Include(file => file.FileContent)
                .Single(e => e.Id == fileId);
        }

        public File GetFileInfoFromDb(string fileId)
        {
            return _context.Files.Find(fileId);
        }

        public void DeleteFileFromDb(string fileId)
        {
            var deleteFile = _context.Files
                .Include(file => file.FileContent)
                .Single(e => e.Id == fileId);
            _context.Files.Remove(deleteFile);
            _context.SaveChanges();
        }

        private File AddFormFile2Db(IFormFile formFile)
        {
            var file = FormFile2File(formFile);

            _context.Files.Add(file);
            return file;
        }

        private static File FormFile2File(IFormFile formFile)
        {
            var file = new File
            {
                ContentType = formFile.ContentType,
                ContentDisposition = formFile.ContentDisposition,
                FileSize = formFile.Length,
                FileName = formFile.FileName,
                IsStoredInFileSystem = false,
                Status = StatusEnums.Status.Created,
                CreatedTs = DateTime.Now,
                UpdatedTs = DateTime.Now
            };
            CopyFileContentFromFormFile(formFile, file);

            return file;
        }

        private static void CopyFileContentFromFormFile(IFormFile formFile, File file)
        {
            if (formFile.Length > 0)
            {
                using var ms = new MemoryStream();
                formFile.CopyTo(ms);
                var fileBytes = ms.ToArray();
                file.FileContent = new FileBytes {ContentBytes = fileBytes};
            }
        }
    }
}