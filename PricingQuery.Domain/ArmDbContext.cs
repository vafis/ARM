using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingQuery.Domain.Entities;

namespace PricingQuery.Domain
{
    public class ArmDbContext : DbContext
    {
        public ArmDbContext() : base("name=ArmDbContext") { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Person>()
                .HasMany<Product>(s => s.Products)
                        .WithMany(c => c.Persons)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("PersonId");
                            cs.MapRightKey("ProductId");
                            cs.ToTable("PersonProduct");
                        });

            modelBuilder.Entity<Address>()
             .HasKey(e => e.Id);
            modelBuilder.Entity<Address>()
                        .HasRequired(s => s.Person)
                        .WithRequiredDependent(x => x.Address); 


            base.OnModelCreating(modelBuilder);
        }
    }

}
