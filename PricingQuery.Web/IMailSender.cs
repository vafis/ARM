using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingQuery.Domain.Entities;

namespace PricingQuery.Web
{
    public interface IMailSender
    {
        void Send(Person p);
    }
}
