using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boarder
{
    public abstract class BoardItem
    {
       protected const string DateFormat = "dd-MM-yyyy";

        private DateTime dueDate;
        private string title;
        private readonly List<EventLog> history = new List<EventLog>();

        protected BoardItem(string title, DateTime dueDate, Status InitialStatus)
        {
            this.EnsureValidDate(dueDate);
            this.EnsureValidTitle(title);

            this.dueDate = dueDate;
            this.title = title;
            this.Status = InitialStatus;

        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.EnsureValidTitle(value);

                if (value != this.title)
                {
                    this.AddEventLog($"Title changed from '{this.title}' to '{value}'");

                    this.title = value;

                }
            }
        }

        public DateTime DueDate
        {
            get
            {
                return this.dueDate;
            }
            set
            {
                this.EnsureValidDate(value);
                this.AddEventLog($"DueDate changed from '{this.dueDate.ToString(DateFormat)}' to '{value.ToString(DateFormat)}'");

                this.dueDate = value;
            }
        }

        public Status Status { get; protected set; }

        public abstract void RevertStatus();

        public abstract void AdvanceStatus();
        

        public virtual string ViewInfo()
        {
            return $"'{this.Title}', [{this.Status}|{this.DueDate.ToString(DateFormat)}]";
        }

        public string ViewHistory()
        {
            this.history.OrderBy(d => d.Time);
            StringBuilder result = new StringBuilder();
            foreach (EventLog entry in this.history)
            {
                result.AppendLine(entry.ViewInfo());
            }

            return result.ToString();
        }

        protected void AddEventLog(string desc)
        {
            this.history.Add(new EventLog(desc));
        }

        private void EnsureValidDate(DateTime value)
        {
            if (value < DateTime.Now)
            {
                throw new ArgumentException("DueDate can't be in the past");
            }
        }

        protected void EnsureValidTitle(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Please provide a non-empty name");
            }

            if (value.Length < 5 || value.Length > 30)
            {
                throw new ArgumentException("Please provide a value with length between 5 and 30 characters.");
            }
        }
    }
}
