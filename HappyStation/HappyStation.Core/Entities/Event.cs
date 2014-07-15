using System;

namespace HappyStation.Core.Entities
{
    public class Event : DatabaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Text { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
