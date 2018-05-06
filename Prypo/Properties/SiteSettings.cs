using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Prypo.Properties
{
    public class Settings
    {
        public static string SmtpEmail = ConfigurationManager.AppSettings["smtpemail"];
        public static string SmtpPassword = ConfigurationManager.AppSettings["smtppassword"];
        public static string SmtpSmtp = ConfigurationManager.AppSettings["smtpsmtp"];
        public static int SmtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpport"]);
    }
}