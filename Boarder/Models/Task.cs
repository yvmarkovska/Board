using Boarder.Loggers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Boarder
{
    public class Task : BoardItem
    {
        private string assignee;

        public Task(string title, string assignee, DateTime dueDate)
            : base(title, dueDate, Status.Todo)
        {
            EnsureValidTitle(assignee);
            this.assignee = assignee;
            this.AddEventLog($"Created Task:'{this.Title}' , [{this.Status}|{this.DueDate.ToString(DateFormat)}]  Assignee: {assignee}");
        }
        public string Assignee
        {
            get => this.assignee;
            set
            {
                this.EnsureValidAssignee(value);
                this.AddEventLog($"Assignee changed from'{this.Assignee}' to '{value}'");
                this.assignee = value;
            }
        }
        public override void AdvanceStatus()
        {
            if (this.Status != Status.Verified)
            {
                var prevStatus = this.Status;
                this.Status++;
                this.AddEventLog($"Task changed from {prevStatus} to {this.Status}");
            }
            else
            {
                throw new ArgumentException($"Task status already {Status.Verified}");
                this.AddEventLog($"Task status already {Status.Verified}");
            }

        }

        public override void RevertStatus()
        {
            if (this.Status == Status.Todo)
            {

                this.AddEventLog($"Can't revert, already at {Status.Todo}");
                throw new ArgumentException($"Can't revert, already at {Status.Todo}");
            }
            else if (this.Status != Status.Todo)
            {
                var prevStatus = this.Status;
                this.Status--;

                this.AddEventLog($"Task changed from {prevStatus} to {this.Status}");
            }
        }

        public override string ViewInfo()
        {
            var baseInfo = base.ViewInfo();
            return $"{baseInfo} Assignee: {assignee}";
        }

        private void EnsureValidAssignee(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Please provide a non-null or empty value");
            }

            if (value.Length < 5 || value.Length > 30)
            {
                throw new ArgumentException("Please provide a value between 5 and 30 characters.");
            }
        }


    }
}
