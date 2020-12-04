using System.Net;
using System.Net.Mail;

namespace Cursos_WPF.Helpers
{
    public static class EmailHelper
    {
        public static bool SendEmail(string recipient, string subject, string body)
        {
            bool isEmailSend = false;

            try
            {
                MailAddress from = new MailAddress("iniguez.moto@gmail.com", "Courses WPF");
                MailAddress to = new MailAddress(recipient);

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from.Address, "XXXXXXXXX")
                };

                MailMessage message = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body
                };

                smtp.Send(message);

                isEmailSend = true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return isEmailSend;
        }
    }
}
