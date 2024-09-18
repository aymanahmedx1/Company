using System.Net;
using System.Net.Mail;

namespace Company.Web.Helpers
{
    public class EmailSettings
    {
        public static void SendMail(EmailData email) { 
            var client = new SmtpClient("smtp.gmail.com",587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("iayman8064@gmail.com", "bvehmvtgnlmavvvj");
            client.Send("iayman8064@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
