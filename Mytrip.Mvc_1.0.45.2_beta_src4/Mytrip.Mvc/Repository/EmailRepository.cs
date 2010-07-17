using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Xml.Linq;
using Mytrip.Mvc.Models;

namespace Mytrip.Mvc.Repository
{
   public class EmailRepository
   {
       string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
       public string from_email()
       {
           XDocument _doc = XDocument.Load(_absolutDirectory);
           var core = _doc.Root.Elements("email").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "EmailAdress");
           return core.Attribute("value").Value;
       }
       public string smtp()
       {
           XDocument _doc = XDocument.Load(_absolutDirectory);
           var core = _doc.Root.Elements("email").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "Smtp");
           return core.Attribute("value").Value;
       }
       public int port()
       {
           XDocument _doc = XDocument.Load(_absolutDirectory);
           var core = _doc.Root.Elements("email").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "Port");
           return int.Parse(core.Attribute("value").Value);
       }
       public bool ssl()
       {
           XDocument _doc = XDocument.Load(_absolutDirectory);
           var core = _doc.Root.Elements("email").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "Ssl");
           return bool.Parse(core.Attribute("value").Value);
       }
       public string login_email()
       {
           XDocument _doc = XDocument.Load(_absolutDirectory);
           var core = _doc.Root.Elements("email").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "Login");
           return core.Attribute("value").Value;
       }
       public string pass_email()
       {
           XDocument _doc = XDocument.Load(_absolutDirectory);
           var core = _doc.Root.Elements("email").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "Password");
           return core.Attribute("value").Value;
       }
       public void SendEmail(string to_email,string subject,string body)
       {
           
               MailMessage Message = new MailMessage();
               Message.Subject = subject;
               Message.Body = body;
               Message.From = new System.Net.Mail.MailAddress(from_email());
               Message.To.Add(new MailAddress(to_email));
               SmtpClient Smtp = new SmtpClient(smtp(), port());
               Smtp.Host = smtp();
               Smtp.EnableSsl = ssl();
               Smtp.Credentials = new System.Net.NetworkCredential(login_email(), pass_email());
               Smtp.Send(Message);
           
       }
       public bool approvedSendEmail(EmailModel m)
       {
           try
           {
               MailMessage Message = new MailMessage();
               Message.Subject = m.FromEmail;
               Message.Body = m.FromEmail;
               Message.From = new System.Net.Mail.MailAddress(m.FromEmail);
               Message.To.Add(new MailAddress(m.FromEmail));
               SmtpClient Smtp = new SmtpClient(m.Smtp, m.Port);
               Smtp.Host = m.Smtp;
               Smtp.EnableSsl = m.Ssl;
               Smtp.Credentials = new System.Net.NetworkCredential(m.LoginEmail, m.PasswordEmail);
               Smtp.Send(Message);
               return true;
           }catch{return false;}
       }
    }
}
