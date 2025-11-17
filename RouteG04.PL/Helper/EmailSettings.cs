using System.Net;
using System.Net.Mail;

namespace RouteG04.PL.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("mahanyyousef8@gmail.com", "lfhumhwcumgukthd");
            Client.Send("mahanyyousef8@gmail.com", email.To, email.Subject, email.Body);

        }
    }
}
