using System;

namespace FYApp.Core.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public string PaymentReason { get; set; }
        public int PaymentAmount { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public DateTime Date { get; set; }
        public int HouseholdId { get; set; }
        public Household Household { get; set; }
    }
}