using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingQuery.Domain.Entities
{
    public class Address
    {
       // 
       // public Guid PersonId { get; set; }

        [Key, ForeignKey("Person")]
        public Guid Id { get; set; }
        [Required]
        public string Line1 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string Country { get; set; }

        public virtual Person Person { get; set; }
    }
}
