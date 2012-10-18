using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Xml.Linq;
using mtm.Core.Settings;

namespace mtm.Core.Install
{
    internal static class Crypto
    {
        private static string password()
        {
            string absolutDirectory = GeneralMethods.GetPath("Web.config");
            XDocument doc = XDocument.Load(absolutDirectory);
            var core = doc.Root.Elements("appSettings").Elements("add");
            return core.FirstOrDefault(x => x.Attribute("key").Value == "applicationId")
                .Attribute("value").Value;
        }
        private static byte[] Encrypt(byte[] data)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            ICryptoTransform ct = sa.CreateEncryptor(
                (new PasswordDeriveBytes(password(), null)).GetBytes(16),
                new byte[16]);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();
            return ms.ToArray();
        }
        internal static string Encrypt(string data)
        {
            try
            {
                return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(data)));
            }
            catch { return "null"; }
        }
        private static byte[] Decrypt(byte[] data)
        {
            BinaryReader br = new BinaryReader(InternalDecrypt(data));
            return br.ReadBytes((int)br.BaseStream.Length);
        }
        internal static string Decrypt(string data)
        {
            try
            {
                CryptoStream cs = InternalDecrypt(Convert.FromBase64String(data));
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch { return "null"; }
        }
        private static CryptoStream InternalDecrypt(byte[] data)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            ICryptoTransform ct = sa.CreateDecryptor(
                (new PasswordDeriveBytes(password(), null)).GetBytes(16),
                new byte[16]);
            MemoryStream ms = new MemoryStream(data);
            return new CryptoStream(ms, ct, CryptoStreamMode.Read);
        }
        /******************************/
        private static byte[] Encrypt(byte[] data,string password)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            ICryptoTransform ct = sa.CreateEncryptor(
                (new PasswordDeriveBytes(password, null)).GetBytes(16),
                new byte[16]);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();
            return ms.ToArray();
        }
        private static string Encrypt(string data,string password)
        {
            try
            {
                return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(data),password));
            }
            catch { return "null"; }
        }
        private static byte[] Decrypt(byte[] data,string password)
        {
            BinaryReader br = new BinaryReader(InternalDecrypt(data,password));
            return br.ReadBytes((int)br.BaseStream.Length);
        }
        private static string Decrypt(string data,string password)
        {
            try
            {
                CryptoStream cs = InternalDecrypt(Convert.FromBase64String(data),password);
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch { return "null"; }
        }
        private static CryptoStream InternalDecrypt(byte[] data,string password)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            ICryptoTransform ct = sa.CreateDecryptor(
                (new PasswordDeriveBytes(password, null)).GetBytes(16),
                new byte[16]);
            MemoryStream ms = new MemoryStream(data);
            return new CryptoStream(ms, ct, CryptoStreamMode.Read);
        }
        /*****************************/
        internal static void ReplaceKeyEncrypt()
        {
            string email = EmailSetting.from_email();
            string smtp = EmailSetting.smtp();
            string loginemail = EmailSetting.login_email();
            string passwordemail = EmailSetting.pass_email();

            string server = CoreSetting.Server();
            string database = CoreSetting.DataBase();
            string user = CoreSetting.User();
            string password = CoreSetting.Password();

            string newkey=Guid.NewGuid().ToString();

            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
            XDocument _doc = XDocument.Load(_absolutDirectory);            
            
            var _email = _doc.Root.Elements("email").Elements("add");
            _email.FirstOrDefault(x => x.Attribute("name").Value == "EmailAdress")
                .SetAttributeValue("value", Crypto.Encrypt(email, newkey));
            _email.FirstOrDefault(x => x.Attribute("name").Value == "Smtp")
                .SetAttributeValue("value", Crypto.Encrypt(smtp, newkey));
            _email.FirstOrDefault(x => x.Attribute("name").Value == "Login")
                .SetAttributeValue("value", Crypto.Encrypt(loginemail, newkey));
            _email.FirstOrDefault(x => x.Attribute("name").Value == "Password")
                .SetAttributeValue("value", Crypto.Encrypt(passwordemail, newkey));

            var connection = _doc.Root.Elements("connection").Elements("add");
            connection.FirstOrDefault(x => x.Attribute("name").Value == "DataBase")
                .SetAttributeValue("value", Crypto.Encrypt(database, newkey));
            connection.FirstOrDefault(x => x.Attribute("name").Value == "Password")
                .SetAttributeValue("value", Crypto.Encrypt(password, newkey));
            connection.FirstOrDefault(x => x.Attribute("name").Value == "Server")
                .SetAttributeValue("value", Crypto.Encrypt(server, newkey));
            connection.FirstOrDefault(x => x.Attribute("name").Value == "User")
                .SetAttributeValue("value", Crypto.Encrypt(user, newkey));

            _doc.Save(_absolutDirectory);

            string absolutDirectory = GeneralMethods.GetPath("Web.config");
            XDocument doc = XDocument.Load(absolutDirectory);
            var core = doc.Root.Elements("appSettings").Elements("add");
            core.FirstOrDefault(x => x.Attribute("key").Value == "applicationId")
                .SetAttributeValue("value", newkey);
            doc.Save(absolutDirectory);

            GeneralMethods.MytripCacheRemove("mtm_es_fromemail");
            GeneralMethods.MytripCacheRemove("mtm_es_smtp");
            GeneralMethods.MytripCacheRemove("mtm_es_user");
            GeneralMethods.MytripCacheRemove("mtm_es_password");

            GeneralMethods.MytripCacheRemove("mtm_cs_server");
            GeneralMethods.MytripCacheRemove("mtm_cs_database");
            GeneralMethods.MytripCacheRemove("mtm_cs_user");
            GeneralMethods.MytripCacheRemove("mtm_cs_password");
        }
    }
}