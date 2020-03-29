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

        public string NotifierId { get; set; }
        public string NotifiedBy { get; set; }
        public StatusEnums.Status Status { get; set; }
        public EntityEnums.EntityType EntityType { get; set; }
        public EntityEnums.EntityAction EntityAction { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "text")] public string Content { get; set; }
        [DataType(DataType.Date)] public DateTime CreatedTs { get; set; }
    }
}