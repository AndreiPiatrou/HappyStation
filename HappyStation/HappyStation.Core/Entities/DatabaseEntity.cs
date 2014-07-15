using System;

namespace HappyStation.Core.Entities
{
    public class DatabaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}