using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FileApi.Enums;

namespace FileApi.Models
{
    public class File
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }

        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public bool IsStoredInFileSystem { get; set; }
        public string ContentDisposition { get; set; }
        public FileBytes FileContent { get; set; }
        public StatusEnums.Status Status { get; set; }
        [DataType(DataType.Date)] public DateTime CreatedTs { get; set; }
        [DataType(DataType.Date)] public DateTime UpdatedTs { get; set; }
    }
}