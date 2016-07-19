using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using PricingQuery.Web;
using System.Reflection;
using PricingQuery.Service;

namespace PricingQuery.Web.App_Start
{
    public class AutofacConfig
    {
        public static IContainer Setup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<PricingService>()
                .As<IPricingSevice>().InstancePerLifetimeScope();

            builder.RegisterType<MailSender>()
                .As<IMailSender>().InstancePerLifetimeScope();
 
            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            //Set dependency resolver
            var container = builder.Build();
            return container;
        }

    }
}