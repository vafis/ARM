using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;
using Moq;
using PricingQuery.Domain.Entities;
using PricingQuery.Service;
using PricingQuery.Web.App_Start;
using PricingQuery.Web.Controllers;
using PricingQuery.Web.Models;
using Xunit;
using PricingQuery.Web;

namespace PricingQuery.Web.Tests
{
    public class Fixture : IDisposable
    {
        public Fixture()
        {
            PricingSevice = AutofacConfig.Setup().Resolve<IPricingSevice>();
            MailSender = AutofacConfig.Setup().Resolve<IMailSender>();
        }

        public IPricingSevice PricingSevice { get; private set; }
        public IMailSender MailSender { get; private set; }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
    
    public class UnitTests : IClassFixture<Fixture>
    {
        private Fixture _fixture;
        public UnitTests(Fixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Create_PricingQuery_without_error()
        {
            var product = _fixture.PricingSevice.GetLookupProducts().FirstOrDefault();
            product.IsChecked = true;
            var personViewModel = new PersonViewModel
            {
                Id=Guid.NewGuid(),
                Name = "Kostas",
                Surname = "Vafeiadakis",
                Phone = "23312323322",
                Email = "vafeiadakis@valuesoft.eu",
                Address = new Address(){City = "Liverpool", Country = "UK", Line1 = "Dock road", PostCode = "L20"},
                Products = new List<Product>() { }
            };
            personViewModel.Address.Id = personViewModel.Id;
            personViewModel.Products.Add(product);

            var pricing = new Mock<IPricingSevice>();
            pricing.Setup(x => x.SavePerson(personViewModel));
            var mailing = new Mock<IMailSender>();
            mailing.Setup(x => x.Send(personViewModel));

            var application = new HomeController(pricing.Object, mailing.Object);

            ActionResult result = application.Create(personViewModel);
            Assert.Equal(result.GetType(), typeof(RedirectToRouteResult));
            Assert.Equal(((RedirectToRouteResult)result).RouteValues["action"], "Created");
        }

        [Fact]
        public void Create_Application_with_error()
        {
            var product = _fixture.PricingSevice.GetLookupProducts().FirstOrDefault();
            product.IsChecked = false;
            var personViewModel = new PersonViewModel
            {
                Id = Guid.NewGuid(),
                Name = "",
                Surname = "V",
                Phone = "23312323322",
                Email = "vafeiadakis@valuesoft.eu",
                Address = new Address() { City = "Liverpool", Country = "UK", Line1 = "Dock road", PostCode = "L20" },
                Products = new List<Product>() { }
            };
            personViewModel.Address.Id = personViewModel.Id;
            personViewModel.Products.Add(product);

            var pricing = new Mock<IPricingSevice>();
            pricing.Setup(x => x.SavePerson(personViewModel));
            var mailing = new Mock<IMailSender>();
            mailing.Setup(x => x.Send(personViewModel));

            var application = new HomeController(pricing.Object, mailing.Object);

            var modelBinder = new ModelBindingContext()
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(
                                  () => personViewModel, personViewModel.GetType()),
                ValueProvider = new NameValueCollectionValueProvider(
                                    new NameValueCollection(), CultureInfo.InvariantCulture)
            };

            var binder = new DefaultModelBinder().BindModel(
                 new ControllerContext(), modelBinder);
            application.ModelState.Clear();
            application.ModelState.Merge(modelBinder.ModelState);


            ActionResult result = application.Create(personViewModel);
            Assert.Equal(result.GetType(), typeof(ViewResult));
            Assert.False(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
