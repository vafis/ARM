using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PricingQuery.Domain;
using PricingQuery.Domain.Entities;

namespace PricingQuery.Web
{
    public class EntitiesContextInitializer: DropCreateDatabaseIfModelChanges<ArmDbContext>
    {
        protected override void Seed(ArmDbContext context)
        {
            var skills = new List<Product>()
            {
                new Product() {Id = Guid.NewGuid(), Name = "Processor ARM Cortex-A"},
                new Product() {Id = Guid.NewGuid(), Name = "Mali Video Processor"},
                new Product() {Id = Guid.NewGuid(), Name = "Processor ARM Cortex-M"},
            };

            skills.ForEach(x => context.Products.Add(x));
            context.SaveChanges();
        }
    }
 
}