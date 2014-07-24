using System;
using System.ComponentModel.DataAnnotations;

namespace HappyStation.Core.Entities
{
    public class Event : DatabaseEntity
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Text { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
