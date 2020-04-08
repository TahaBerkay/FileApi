using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using File = FileApi.Models.File;

namespace FileApi.Services
{
    public class File2FsService : IFileService
    {
        private readonly FileContext _context;

        private readonly string _rootFileDirectory;

        public File2FsService(FileContext context, IConfiguration configuration)
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
            var fileNameInFs = FileUtility.GenerateFileNameForFs(formFile);
            var filePathInFs = GetFilePathInFs(fileNameInFs);

            FileUtility.Save2FsByStreaming(filePathInFs, formFile);
            var file = AddFormFile(formFile, fileNameInFs);
            _context.SaveChanges();
            file.FileNameInFs = null;
            return file;
        }

        public List<File> UploadMultipleFiles(List<IFormFile> formFiles)
        {
            var files = new List<File>();
            foreach (var formFile in formFiles)
            {
                var fileNameInFs = FileUtility.GenerateFileNameForFs(formFile);
                var filePathInFs = GetFilePathInFs(formFile.FileName);

                FileUtility.Save2FsByStreaming(filePathInFs, formFile);
                var file = AddFormFile(formFile, fileNameInFs);
                files.Add(file);
            }

            _context.SaveChanges();
            files.ForEach(file => file.FileNameInFs = null);
            return files;
        }

        public File UpdateFile(IFormFile formFile, string fileId)
        {
            var currentFile = _context.Files
                .Include(file => file.FileContent)
                .Single(e => e.Id == fileId);
            if (currentFile != null)
            {
                var newFileNameInFs = FileUtility.GenerateFileNameForFs(formFile);
                var newFilePathInFs = GetFilePathInFs(formFile.FileName);
                var oldFilePathInFs = GetFilePathInFs(currentFile.FileNameInFs);

                System.IO.File.Delete(oldFilePathInFs);
                FileUtility.UpdatedFileByFormFile(formFile, currentFile, newFileNameInFs, true);
                FileUtility.Save2FsByStreaming(newFilePathInFs, formFile);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("FileId not found:" + fileId);
            }

            currentFile.FileNameInFs = null;
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
            var filePathInFs = GetFilePathInFs(deleteFile.FileNameInFs);
            System.IO.File.Delete(filePathInFs);
            _context.Files.Remove(deleteFile);
            _context.SaveChanges();
        }

        public FileStream GetFileStream(File file)
        {
            var filePathInFs = GetFilePathInFs(file.FileNameInFs);
            return new FileStream(filePathInFs, FileMode.Open, FileAccess.Read);
        }

        public string GetFilePathInFs(string fileNameInFs)
        {
            return FileUtility.GetFilePathInFs(_rootFileDirectory, fileNameInFs);
        }

        private File AddFormFile(IFormFile formFile, string fileNameInFs)
        {
            var file = FileUtility.FormFile2File(formFile, true);
            file.FileNameInFs = fileNameInFs;
            _context.Files.Add(file);
            return file;
        }
    }
}