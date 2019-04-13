using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using FYApp.Api.DAL;
using FYApp.Core.Models;

namespace FYApp.Api.Services
{
    public class FYCoreService : IFYCoreService
    {
        private readonly FYDbContext ctx;

        public FYCoreService()
        {
            ctx = new FYDbContext();   
        }

        /// <summary>
        /// Used to initliase the database 
        /// </summary>
        public void Initialise()
        {
            ctx.Initialise();
        }

        /// <summary>
        /// Return the household identified by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Household or null if not found</returns>
        public Household GetHouseholdById(int id)
        {
            return ctx.Households
                .Include(h => h.Bills)
                .Include(h => h.Members)
                .FirstOrDefault(h => h.HouseholdId == id);
        }

        /// <summary>
        /// Return a list of households ordered by ID or Name
        /// </summary>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Household> GetAllHouseholds(string orderBy)
        {
            switch (orderBy)
            {
                case "householdname" :
                    return ctx.Households.OrderBy(h => h.HouseholdName).ToList();
                default:
                    return ctx.Households.OrderBy(m => m.HouseholdId).ToList();
            }
        }

        /// <summary>
        /// Add a Household
        /// </summary>
        /// <param name="h"></param>
        /// <returns>The added Household if successful otherwise null</returns>
        public Household AddHousehold(Household h)
        {
            ctx.Households.Add(h);
            ctx.SaveChanges();
            return h;
        }

        ///<summary>
        ///Update a Household
        ///<summary>
        ///<param name="h"></param>
        /// <returns>Returns true if update is successful otherwise false</returns>
        public bool UpdateHousehold(Household h)
        {
            ctx.Attach(h).State = EntityState.Modified;
            ctx.SaveChanges();
            return true;
        }


        ///<summary>
        ///Delete the specified Household
        ///<summary>
        ///<param name="h"></param>
        /// <returns>Returns true if deletion is successful otherwise false</returns>
        public bool DeleteHousehold(int id)
        {
            var h = GetHouseholdById(id);
            if(h == null)
            {
                return false;
            }

            ctx.Households.Remove(h);
            ctx.SaveChanges();
            return true;
        }

        ///<summary>
        ///Add the specified bill
        ///<summary>
        ///<param name="b"></param>
        /// <returns>Returns the added Bill if successful otherwise false</returns>
        public Bill AddBill(Bill b)
        {
            var household = GetHouseholdById(b.HouseholdId);
            if(household == null)
            {
                return null;
            }
            ctx.Bills.Add(b);
            ctx.SaveChanges();
            return b;
        }

        /// <summary>
        /// Return the Bill identified by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Bill or null if not found</returns>
        public Bill GetBillById(int id)
        {
            return ctx.Bills
                .Include(b => b.Household)
                .FirstOrDefault(b => b.BillId == id);
        }

        /// <summary>
        /// Delete the specified Bill
        /// </summary>
        /// <param name="b"></param>
        /// <returns>Returns true if deletion successful otherwise false</returns>
        public bool DeleteBill(int id)
        {
            var bill = ctx.Bills.FirstOrDefault((b => b.BillId == id));
            if (bill == null)
            {
                return false;
            }

            ctx.Remove(bill);
            ctx.SaveChanges();
            return true;
        }


         public Household AddMemberToHousehold(int id, Member m)
         {
             var household = GetHouseholdById(id);
            if(household == null)
            {
                return null;
            }
            household.Members.Add(m);
            ctx.SaveChanges();
            return household;
         }
    }
}

