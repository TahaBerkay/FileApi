using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileApi.Enums;
using FileApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using File = FileApi.Models.File;

namespace FileApi.Services
{
    public class StreamFile2FsService : IFileService
    {
        private readonly FileContext _context;

        private readonly string _rootFileDirectory;

        public StreamFile2FsService(FileContext context, IConfiguration configuration)
        {
            _context = context;

            var fileDir = configuration.GetValue<string>("CustomAppSettings:FilePathToSaveInFileSystem");
            if (Directory.Exists(fileDir))
                _rootFileDirectory = fileDir;
            else
                throw new Exception("FilePathToSaveInFileSystem does not exist");
        }

        public File UploadFile(IFormFile formFile)
        {
            //////////////////////////////////
            // TODO: if (formFile.Length > 0)
            // TODO: encoding path and name
            // TODO: fix id base get methods on url
            //////////////////////////////////

            var combined = Path.Combine(_rootFileDirectory, formFile.FileName);
            if (FileUtility.CheckPathIsValid(combined))
            {
                FileUtility.Save2Fs(combined, formFile);
                var file = AddFormFile(formFile, combined);
                _context.SaveChanges();
                return file;
            }

            throw new Exception("Combined path is invalid:" + combined);
        }

        public List<File> UploadMultipleFiles(List<IFormFile> formFiles)
        {
            var files = new List<File>();
            foreach (var formFile in formFiles)
            {
                var combined = Path.Combine(_rootFileDirectory, formFile.FileName);
                if (FileUtility.CheckPathIsValid(combined))
                {
                    FileUtility.Save2Fs(combined, formFile);
                    var file = AddFormFile(formFile, combined);
                    files.Add(file);
                }
                else
                {
                    throw new Exception("Combined path is invalid:" + combined);
                }
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
                var combined = Path.Combine(_rootFileDirectory, formFile.FileName);
                if (FileUtility.CheckPathIsValid(combined))
                {
                    System.IO.File.Delete(currentFile.FilePath);
                    currentFile.ContentType = formFile.ContentType;
                    currentFile.ContentDisposition = formFile.ContentDisposition;
                    currentFile.FileSize = formFile.Length;
                    currentFile.FileName = formFile.FileName;
                    currentFile.IsStoredInFileSystem = true;
                    currentFile.FilePath = combined;
                    currentFile.Status = StatusEnums.Status.Updated;
                    currentFile.UpdatedTs = DateTime.Now;

                    FileUtility.Save2Fs(combined, formFile);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Combined path is invalid:" + combined);
                }
            }
            else
            {
                throw new Exception("FileId not found:" + fileId);
            }

            currentFile.FileContent = null;
            return currentFile;
        }

        public File GetFile(string fileId)
        {
            return _context.Files.Single(e => e.Id == fileId);
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
            System.IO.File.Delete(deleteFile.FilePath);
            _context.Files.Remove(deleteFile);
            _context.SaveChanges();
        }

        private File AddFormFile(IFormFile formFile, string filePath)
        {
            var file = FileUtility.FormFile2File(formFile, true);
            file.FilePath = filePath;
            _context.Files.Add(file);
            return file;
        }
    }
}