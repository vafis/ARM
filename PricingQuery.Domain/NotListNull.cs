using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingQuery.Domain.Entities;

namespace PricingQuery.Domain
{
    public class NotListNull : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as List<Product>;
            if (list != null)
            {
                return list.Where(x => x.IsChecked == true).ToList().Count > 0;
            }
            return false;
        }
    }
}
