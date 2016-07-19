using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using PricingQuery.Domain.Entities;

namespace PricingQuery.Web
{
    public class MailSender:IMailSender
    {
        public void Send(Person p)
        {
            MailMessage objeto_mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.Host = "smtp.internal.mycompany.com";
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("user", "Password");
            objeto_mail.From = new MailAddress("from@server.com");
            objeto_mail.To.Add(new MailAddress("to@server.com"));
            objeto_mail.Subject = "New Pricing Query Created";
            string products = "";
            p.Products.ForEach(x =>
            {
                products = products + x.Name + " ";
            });
            objeto_mail.Body = p.Name + " " + p.Surname + " " + p.Phone + " " + p.Email + " Products : " + products;

            //set uo first the above settings and uncomment below
            //client.Send(objeto_mail);
        }
    }
}