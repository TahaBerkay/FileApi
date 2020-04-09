using System;
using System.Collections.Generic;
using System.Linq;
using FileApi.Models;
using FileApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FileApi.Services
{
    public class File2DbService : IFileService
    {
        private readonly FileContext _context;

        public File2DbService(FileContext context)
        {
            _context = context;
        }

        public File UploadFile(IFormFile formFile)
        {
            var file = AddFormFile(formFile);
            _context.SaveChanges();
            return file;
        }

        public List<File> UploadMultipleFiles(List<IFormFile> formFiles)
        {
            var files = new List<File>();
            foreach (var formFile in formFiles)
            {
                var file = AddFormFile(formFile);
                files.Add(file);
            }

            _context.SaveChanges();
            return files;
        }

        public File UpdateFile(IFormFile formFile, string fileId)
        {
            var currentFile = _context.Files
                .Include(file => file.FileContent)
                .Single(e => e.Id == fileId);
            if (currentFile != null)
            {
                FileUtility.UpdatedFileByFormFile(formFile, currentFile, null, false);
                FileUtility.CopyFileContentFromFormFile(formFile, currentFile);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("FileId not found:" + fileId);
            }

            return currentFile;
        }

        public File GetFile(string fileId)
        {
            return _context.Files
                .Include(file => file.FileContent)
                .Single(e => e.Id == fileId);
        }

        public File GetFileInfo(string fileId)
        {
            return _context.Files.Find(fileId);
        }

        public void DeleteFile(string fileId)
        {
            var deleteFile = _context.Files
                .Include(file => file.FileContent)
                .Single(e => e.Id == fileId);
            _context.Files.Remove(deleteFile);
            _context.SaveChanges();
        }

        private File AddFormFile(IFormFile formFile)
        {
            var file = FileUtility.FormFile2File(formFile, false);

            _context.Files.Add(file);
            return file;
        }
    }
}