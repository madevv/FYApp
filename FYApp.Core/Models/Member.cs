using System;

namespace FYApp.Core.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int? HouseholdId { get; set; }
        public Household Household { get; set; }

    }
}
