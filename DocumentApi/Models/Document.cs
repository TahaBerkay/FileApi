using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DocumentApi.Enums;

namespace DocumentApi.Models
{
    public class Document
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string FileSize { get; set; }
        public ContentTypeEnums ContentType { get; set; }
        public StatusEnums.Status Status { get; set; }
        [DataType(DataType.Date)] public DateTime CreatedTs { get; set; }
        [DataType(DataType.Date)] public DateTime UpdatedTs { get; set; }
        [DataType(DataType.Date)] public DateTime DeletedTs { get; set; }
    }
}