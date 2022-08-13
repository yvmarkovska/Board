using System;

namespace Boarder
{
    public class EventLog
    {
        public EventLog(string description)
        {
            this.Description = description ?? throw new ArgumentNullException("Please provide a valid description");
            this.Time = DateTime.Now;
        }

        public string Description { get; }

        public DateTime Time { get; }

        public string ViewInfo()
        {
            return $"[{this.Time:yyyyMMdd|HH:mm:ss.ffff}] {this.Description}";
        }

    }
}
