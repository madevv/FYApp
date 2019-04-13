using System;
using System.Collections.Generic;

using FYApp.Core.Models;

namespace FYApp.Api.Services
{
    public interface IFYCoreService
    {
        void Initialise();
        List<Household> GetAllHouseholds(string orderBy=null);
        Household GetHouseholdById(int id);
        Household AddHousehold(Household h);
        bool DeleteHousehold(int id);
        bool UpdateHousehold(Household h);

        Bill AddBill(Bill b);
        Bill GetBillById(int id);
        bool DeleteBill(int id);

        Household AddMemberToHousehold(int id, Member m);
    
    }
}
