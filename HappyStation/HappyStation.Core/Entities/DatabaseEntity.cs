using System;
using System.ComponentModel.DataAnnotations;

namespace HappyStation.Core.Entities
{
    public class DatabaseEntity
    {
        public DatabaseEntity()
        {
            CreatedAt = DateTime.Now;
            IsDeleted = false;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}