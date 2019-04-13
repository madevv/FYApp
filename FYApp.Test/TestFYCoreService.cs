using System;
using Xunit;
using FYApp.Api.Services;
using FYApp.Core.Models;
using FYApp.Api.DAL;

namespace FYApp.Test
{
    public class TestFYCoreService
    {
        private readonly IFYCoreService svc;

        public TestFYCoreService()
        {
            svc = new FYCoreService();
            //ensures blank database before each test
            svc.Initialise();
            DbSeeder.Seed(svc);
        }

        [Fact]
        public void AddingHousehold_ToEmptyService()
        {
            var h = new Household{HouseholdName = "Household1", HouseholdAddress = "123 Address" };
            
            h = svc.AddHousehold(h);

            var nh = svc.GetHouseholdById(h.HouseholdId);

            Assert.NotNull(nh);

            Assert.Equal(h.HouseholdName, nh.HouseholdName);
        }

        [Fact]
        public void GetAllHouseholdsFromSeededDb()
        {
            var Households = svc.GetAllHouseholds();
            Assert.Single(Households);
        }

        [Fact]
        public void AddHouseholdandAddBill_ToService_ShouldBeFound()
        {
            var h = new Household{HouseholdName = "HouseholdName", HouseholdAddress = "HouseholdAddress"};

            h = svc.AddHousehold(h);

            //set HouseholdId using primary key of Household
            var b = new Bill() { HouseholdId = h.HouseholdId, BillName = "Rent", BillType = BillType.Bill, Amount = 200, DueDate =  new DateTime(2019,09,29)};

            svc.AddBill(b);

            Assert.Single(h.Bills);
        }

        [Fact]
        public void AddBillThenDeleteBill()
        {
            var h = new Household {HouseholdName = "ThisHouseholdName", HouseholdAddress = "123Address" };
            h = svc.AddHousehold(h);

            var b = new Bill() {HouseholdId = h.HouseholdId, BillName = "BillName", BillType = BillType.Utility, Amount = 100, DueDate = new DateTime(2019,09,29)};
            b = svc.AddBill(b);

            Assert.Single(h.Bills);

            var ok = svc.DeleteBill(b.BillId);
            Assert.True(ok);
        }
    }
}
