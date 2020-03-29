using Microsoft.EntityFrameworkCore;
using AttachmentApi.Models;

namespace AttachmentApi
{
    public class AttachmentContext : DbContext
    {
        public AttachmentContext(DbContextOptions<AttachmentContext> options) : base(options)
        {
        }

        public DbSet<Attachment> Attachments { get; set; }
    }
}