using Microsoft.EntityFrameworkCore;
using DocumentApi.Models;

namespace DocumentApi
{
    public class DocumentContext : DbContext
    {
        public DocumentContext(DbContextOptions<DocumentContext> options) : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
    }
}