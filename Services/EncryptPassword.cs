using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace Services
{
    public class EncryptPassword
    {

        public static string GenerationPassWord()
        {
            String pass = Guid.NewGuid().ToString("N").Substring(0,10);
            return pass;
        }


        public static string ConvertTOSHA256(string PassWord)
        {
            StringBuilder stringBuilder = new StringBuilder();
            // usar la referencia de "System.security.cryptographry 
            using (SHA256 sHA = SHA256.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] Result = sHA.ComputeHash(encoding.GetBytes(PassWord));
                foreach (byte b in Result)
                {
                    stringBuilder.Append(b);
                }
                return stringBuilder.ToString();
            }
        }

        //public bool SendEmail(string Email, string Case, String message)
        //{
        //    bool Result = false;
        //    MailMessage mail = new(); 
        //    mail.To.Add(Email);
        //    mail.From = new MailAddress(Email);
        //    mail.Subject = Case;
        //    mail.Body = message;
        //    mail.IsBodyHtml = true;

        //    var smtp = new SmtpClient()
        //    {
        //        Credentials = new NetworkCredential("","")
        //    };
        //}
    }
}
