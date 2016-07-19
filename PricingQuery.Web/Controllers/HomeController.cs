using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PricingQuery.Domain.Entities;
using PricingQuery.Service;
using PricingQuery.Web.Models;

namespace PricingQuery.Web.Controllers
{
    public class HomeController : Controller
    {
        private IPricingSevice _pricingSevice;
        private IMailSender _mailSender;

        public HomeController(IPricingSevice pricingSevice, IMailSender mailSender)
        {
            _pricingSevice = pricingSevice;
            _mailSender = mailSender;
        }

       
        public ActionResult Create()
        {
            var model = new PersonViewModel()
            {
                Products = _pricingSevice.GetLookupProducts()
            };
            return View(model);

        }

        [HttpPost]
        public ActionResult Create(PersonViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                Person p = new Person
                {
                   Id=Guid.NewGuid(),
                    Address = model.Address,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    Phone = model.Phone,
                    Products = model.Products.Where(x => x.IsChecked == true).ToList()
                };
                p.Address.Id = p.Id;
                try
                {
                    _pricingSevice.SavePerson(p);

                    //start task in order to send email
                    Task.Factory.StartNew(() => _mailSender.Send(p));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
                TempData["person"] = p;
                return RedirectToAction("Created");
            }

            return View(model);
        }

        public ActionResult Created()
        {
            return View((Person)TempData["person"]);
        }

 
    }
}