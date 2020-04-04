using FileApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FileApi
{
    public class FileContext : DbContext
    {
        public FileContext(DbContextOptions<FileContext> options) : base(options)
        {
        }

        public DbSet<File> Files { get; set; }
        public DbSet<FileBytes> FileBytes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>()
                .HasOne(a => a.FileContent)
                .WithOne(b => b.File)
                .HasForeignKey<FileBytes>(b => b.FileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}