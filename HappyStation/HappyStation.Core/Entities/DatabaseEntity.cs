using System;
using System.ComponentModel.DataAnnotations;

namespace HappyStation.Core.Entities
{
    public class DatabaseEntity
    {
        protected DatabaseEntity()
        {
            CreatedAt = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}