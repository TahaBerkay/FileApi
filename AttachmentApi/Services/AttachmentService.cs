using System;
using System.Collections.Generic;
using System.Linq;
using AttachmentApi.Enums;
using AttachmentApi.Models;

namespace AttachmentApi.Services
{
    public interface IAttachmentService
    {
        void AddAttachment(Attachment attachment);
        List<Attachment> GetAll();
        Attachment GetById(long id);
        List<Attachment> GetByNotifierId(string notifierId);
        List<Attachment> GetByNotifiedBy(string notifiedBy);
        List<Attachment> GetNotifierAttachmentsAfterDate(string notifierId, DateTime afterDate);
        List<Attachment> GetNotifiedAttachmentsAfterDate(string notifiedBy, DateTime afterDate);

        List<Attachment> GetAttachmentsDetailed(string notifierId, string notifiedBy,
            DateTime afterDate, DateTime beforeDate,
            EntityEnums.EntityType entityType, EntityEnums.EntityAction entityAction,
            StatusEnums.Status status);
    }

    public class AttachmentService : IAttachmentService
    {
        private readonly AttachmentContext _context;

        public AttachmentService(AttachmentContext context)
        {
            _context = context;
        }

        public void AddAttachment(Attachment attachment)
        {
            _context.Attachments.Add(attachment);
            _context.SaveChanges();
        }

        public List<Attachment> GetAll()
        {
            return _context.Attachments.ToList();
        }

        public Attachment GetById(long id)
        {
            return _context.Attachments.Find(id);
        }

        public List<Attachment> GetByNotifierId(string notifierId)
        {
            return _context.Attachments.Where(n => n.NotifierId == notifierId).ToList();
        }

        public List<Attachment> GetByNotifiedBy(string notifiedBy)
        {
            return _context.Attachments.Where(n => n.NotifiedBy == notifiedBy).ToList();
        }

        public List<Attachment> GetNotifierAttachmentsAfterDate(string notifierId, DateTime afterDate)
        {
            return _context.Attachments.Where(n => n.NotifierId == notifierId && n.CreatedTs > afterDate).ToList();
        }

        public List<Attachment> GetNotifiedAttachmentsAfterDate(string notifiedBy, DateTime afterDate)
        {
            return _context.Attachments.Where(n => n.NotifiedBy == notifiedBy && n.CreatedTs > afterDate).ToList();
        }

        public List<Attachment> GetAttachmentsDetailed(string notifierId, string notifiedBy,
            DateTime afterDate, DateTime beforeDate,
            EntityEnums.EntityType entityType, EntityEnums.EntityAction entityAction,
            StatusEnums.Status status)
        {
            return _context.Attachments
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