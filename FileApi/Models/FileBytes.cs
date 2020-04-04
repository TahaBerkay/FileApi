using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileApi.Models
{
    public class FileBytes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }

        public byte[] ContentBytes { get; set; }

        [ForeignKey(nameof(File))] public string FileId { get; set; }

        public File File { get; set; }
    }
}