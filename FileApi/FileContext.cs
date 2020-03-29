using Microsoft.EntityFrameworkCore;
using FileApi.Models;

namespace FileApi
{
    public class FileContext : DbContext
    {
        public FileContext(DbContextOptions<FileContext> options) : base(options)
        {
        }

        public DbSet<File> Files { get; set; }
    }
}