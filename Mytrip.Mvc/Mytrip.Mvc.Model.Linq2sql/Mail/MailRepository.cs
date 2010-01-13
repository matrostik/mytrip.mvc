/*   Mytrip.Mvc.Model.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Web;
using System.Collections;
using Mytrip.Mvc.Model.Linq2sql;

namespace Mytrip.Mvc.Model.Linq2sql.Mail
{
    public class MailRepository
    {
        /*  РАЗДЕЛ 1  */

        #region Подключение к SQL Репозиторию
        /// <summary>
        /// Подключение к SQL Репозиторию
        /// </summary>
        private SqlRepositoryDataContext _db;
        public MailRepository(SqlRepositoryDataContext db)
        {
            _db = db;
        }
        #endregion

        /*  РАЗДЕЛ 2  */
        public aspnet_Users Get_user()
        {
            return _db.aspnet_Users.FirstOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name);
        }
        
        public void Send_mail_registration()
        {
            Guid userid = Get_user().UserId;
            string domain_name = _db.mt_models.First().DomainName;
            string domain_email = _db.mt_models.First().Email;
            string to_email = Get_user().aspnet_Membership.Email;
            string smtp = _db.mt_models.First().Smtp;
            int port = _db.mt_models.First().Port;
            bool ssl = _db.mt_models.First().EnableSsl;
            string login_email = _db.mt_models.First().Login_email;
            string pass_email = _db.mt_models.First().Password_email;
 
            MailMessage Message = new MailMessage();

            Message.Subject = "Регистрация на сайте "+ domain_name;

            Message.Body = "Здравствуйте! Вы получили это письмо т.к. на сайте "
                + domain_name +
                " была произведена регистрация пользователя с Вашим email. " +
            "Если эту регистрацию произвели Вы то Вам необходимо подтвердить её перейдя по ссылке" +
            " http://"+domain_name+"/B/I/"+userid.ToString()+" Администрация сайта "+ domain_name;


            Message.From = new System.Net.Mail.MailAddress(domain_email);

            Message.To.Add(new MailAddress(to_email));

            System.Net.Mail.SmtpClient Smtp = new SmtpClient(smtp, port);

            Smtp.Host = smtp;

            Smtp.EnableSsl = ssl;

            Smtp.Credentials = new System.Net.NetworkCredential(login_email, pass_email);


            Smtp.Send(Message);



        }
    }
}
