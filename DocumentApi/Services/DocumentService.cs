using System;
using System.Collections.Generic;
using System.Linq;
using DocumentApi.Enums;
using DocumentApi.Models;

namespace DocumentApi.Services
{
    public interface IDocumentService
    {
        void AddDocument(Document document);
        List<Document> GetAll();
        Document GetById(long id);
        List<Document> GetByNotifierId(string notifierId);
        List<Document> GetByNotifiedBy(string notifiedBy);
        List<Document> GetNotifierDocumentsAfterDate(string notifierId, DateTime afterDate);
        List<Document> GetNotifiedDocumentsAfterDate(string notifiedBy, DateTime afterDate);

        List<Document> GetDocumentsDetailed(string notifierId, string notifiedBy,
            DateTime afterDate, DateTime beforeDate,
            EntityEnums.EntityType entityType, EntityEnums.EntityAction entityAction,
            StatusEnums.Status status);
    }

    public class DocumentService : IDocumentService
    {
        private readonly DocumentContext _context;

        public DocumentService(DocumentContext context)
        {
            _context = context;
        }

        public void AddDocument(Document document)
        {
            _context.Documents.Add(document);
            _context.SaveChanges();
        }

        public List<Document> GetAll()
        {
            return _context.Documents.ToList();
        }

        public Document GetById(long id)
        {
            return _context.Documents.Find(id);
        }

        public List<Document> GetByNotifierId(string notifierId)
        {
            return _context.Documents.Where(n => n.NotifierId == notifierId).ToList();
        }

        public List<Document> GetByNotifiedBy(string notifiedBy)
        {
            return _context.Documents.Where(n => n.NotifiedBy == notifiedBy).ToList();
        }

        public List<Document> GetNotifierDocumentsAfterDate(string notifierId, DateTime afterDate)
        {
            return _context.Documents.Where(n => n.NotifierId == notifierId && n.CreatedTs > afterDate).ToList();
        }

        public List<Document> GetNotifiedDocumentsAfterDate(string notifiedBy, DateTime afterDate)
        {
            return _context.Documents.Where(n => n.NotifiedBy == notifiedBy && n.CreatedTs > afterDate).ToList();
        }

        public List<Document> GetDocumentsDetailed(string notifierId, string notifiedBy,
            DateTime afterDate, DateTime beforeDate,
            EntityEnums.EntityType entityType, EntityEnums.EntityAction entityAction,
            StatusEnums.Status status)
        {
            return _context.Documents
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