using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingQuery.Domain.Entities;

namespace PricingQuery.Service
{
    public interface IPricingSevice
    {
        void SavePerson(Person person);
        List<Product> GetLookupProducts();
    }
}
