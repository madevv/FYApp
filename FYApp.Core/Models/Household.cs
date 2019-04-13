using System.Collections.Generic;

namespace FYApp.Core.Models
{
    public class Household
    {
        public Household()
        {
            Bills = new List<Bill>();
            Members = new List<Member>();
            Payments = new List<Payment>();
        }

        public int HouseholdId { get; set; }
        public string HouseholdName { get; set; }
        public string HouseholdAddress { get; set; }

        /// <summary>
        /// List of Bills for each House EF Relationship Household -> Bills (1:*)
        /// </summary>
        public IList<Bill> Bills { get; set; }
        public IList<Member> Members { get; set; }
        public IList<Payment> Payments {get;set;}


        /// <summary>
        /// ReadOnly property - Calculates number of bills based on bill list
        /// </summary>
        public int BillCount => Bills.Count;
        public int MemberCount => Members.Count;
        public int PaymentCount => Payments.Count;
    }
}