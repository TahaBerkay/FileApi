using System;
using System.Collections.Generic;
using System.Linq;
using FileApi.Enums;
using FileApi.Models;

namespace FileApi.Services
{
    public interface IFileService
    {
        void AddFile(File file);
        List<File> GetAll();
        File GetById(long id);
        List<File> GetByNotifierId(string notifierId);
        List<File> GetByNotifiedBy(string notifiedBy);
        List<File> GetNotifierFilesAfterDate(string notifierId, DateTime afterDate);
        List<File> GetNotifiedFilesAfterDate(string notifiedBy, DateTime afterDate);

        List<File> GetFilesDetailed(string notifierId, string notifiedBy,
            DateTime afterDate, DateTime beforeDate,
            EntityEnums.EntityType entityType, EntityEnums.EntityAction entityAction,
            StatusEnums.Status status);
    }

    public class FileService : IFileService
    {
        private readonly FileContext _context;

        public FileService(FileContext context)
        {
            _context = context;
        }

        public void AddFile(File file)
        {
            _context.Files.Add(file);
            _context.SaveChanges();
        }

        public List<File> GetAll()
        {
            return _context.Files.ToList();
        }

        public File GetById(long id)
        {
            return _context.Files.Find(id);
        }

        public List<File> GetByNotifierId(string notifierId)
        {
            return _context.Files.Where(n => n.NotifierId == notifierId).ToList();
        }

        public List<File> GetByNotifiedBy(string notifiedBy)
        {
            return _context.Files.Where(n => n.NotifiedBy == notifiedBy).ToList();
        }

        public List<File> GetNotifierFilesAfterDate(string notifierId, DateTime afterDate)
        {
            return _context.Files.Where(n => n.NotifierId == notifierId && n.CreatedTs > afterDate).ToList();
        }

        public List<File> GetNotifiedFilesAfterDate(string notifiedBy, DateTime afterDate)
        {
            return _context.Files.Where(n => n.NotifiedBy == notifiedBy && n.CreatedTs > afterDate).ToList();
        }

        public List<File> GetFilesDetailed(string notifierId, string notifiedBy,
            DateTime afterDate, DateTime beforeDate,
            EntityEnums.EntityType entityType, EntityEnums.EntityAction entityAction,
            StatusEnums.Status status)
        {
            return _context.Files
                .Where(n => n.NotifiedBy == notifiedBy)
                .Where(n => n.NotifierId == notifierId)
                .Where(n => n.EntityType == entityType)
                .Where(n => n.EntityAction == entityAction)
                .Where(n => n.CreatedTs > afterDate)
                .Where(n => n.CreatedTs < beforeDate)
                .Where(n => n.Status == status)
                .ToList();
        }
    }
}