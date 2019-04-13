using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYApp.Core.Models

{
    public enum BillType
    {
        Utility, Bill
    }
    public class Bill
    {
        public int BillId { get; set; }
        public string BillName { get; set; }
        public BillType BillType { get; set; }
        public int Amount { get; set; }
        public DateTime DueDate { get; set; }

        //Foreign Key
        public int HouseholdId { get; set; }
        public Household Household { get; set; }

    }
}