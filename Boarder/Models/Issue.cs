using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Boarder
{
    public class Issue : BoardItem
    {
        public Issue(string title, string description, DateTime dueDate)
            : base(title, dueDate, Status.Open)
        {
            this.Description = description ?? throw new ArgumentNullException("Provide desription");
            this.AddEventLog($"Created Issue:'{this.Title}' , [{this.Status}|{this.DueDate.ToString(DateFormat)}] Description: {description}");
            this.Status = Status.Open;
        }
        public string Description { get; }

        public override void AdvanceStatus()
        {
            if (this.Status != Status.Verified)
            {
                this.Status = Status.Verified;
                this.AddEventLog($"Issue status set to {this.Status}");
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Issue status already {Status.Verified}");
                this.AddEventLog($"Issue status already {Status.Verified}");
            }
        }

        public override void RevertStatus()
        {
            if (this.Status != Status.Open)
            {
                this.Status = Status.Open;
                this.AddEventLog($"Issue status set to {this.Status}");
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Issue status already {Status.Open}");
                this.AddEventLog($"Issue status already {Status.Open}");
            }
        }

        public override string ViewInfo()
        {
            var baseInfo = base.ViewInfo();
            return $"{baseInfo} Description: {this.Description}";
        }


    }
}
