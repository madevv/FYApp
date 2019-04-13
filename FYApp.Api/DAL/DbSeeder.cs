using FYApp.Core.Models;
using FYApp.Api.Services;
using System;

namespace FYApp.Api.DAL
{
    public static class DbSeeder
    {
        public static void Seed(IFYCoreService svc)
        {
            svc.Initialise();

            Household h = svc.AddHousehold(new Household
            {
               HouseholdName = "House1",
               HouseholdAddress = "123 Address"
            });

            svc.AddBill(new Bill
            {
               HouseholdId = h.HouseholdId, BillName = "Rent", BillType = BillType.Bill, Amount = 200, DueDate = new DateTime(2019,09,29)
            });

            svc.AddBill(new Bill
            {
               HouseholdId = h.HouseholdId, BillName = "Electricity", BillType = BillType.Utility, Amount = 100, DueDate =  new DateTime(2019,09,29)
            });

            svc.AddMemberToHousehold(h.HouseholdId, new Member{
                Name = "Name", Email = "email@email.com", Password = "password"
            }); 
        }
    }
}