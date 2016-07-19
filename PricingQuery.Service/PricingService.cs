using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingQuery.Domain;
using PricingQuery.Domain.Entities;

namespace PricingQuery.Service
{
    public class PricingService : IPricingSevice
    {
        private ArmDbContext context = new ArmDbContext();

        public void SavePerson(Person person)
        {
            context.Persons.Attach(person);
            context.Entry(person).State = EntityState.Unchanged;
            context.Persons.Add(person);
            context.SaveChanges();
        }

        public List<Product> GetLookupProducts()
        {
            return context.Products.ToList();
        }

    }
}
