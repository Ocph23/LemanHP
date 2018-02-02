using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace LemanWeb
{
    public class Helper
    {
        public static void SendEmail(string address, string subject, string message)
        {
            try
            {
                string email = "ocph23.test@gmail.com";
                string password = "Sony@7777";
                var loginInfo = new NetworkCredential(email, password);
                var msg = new MailMessage();
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);

                msg.From = new MailAddress(email);
                msg.To.Add(new MailAddress(address));
                msg.Subject = subject;
                msg.Body = message;
                msg.IsBodyHtml = true;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }

        }

        internal static string GenerateNomorResi()
        {
            return "000000001";
        }


        public static byte[] ResizeImage(byte[] images, int width)
        {
            byte[] imageBytes;

            //Of course image bytes is set to the bytearray of your image      

            using (MemoryStream ms = new MemoryStream(images, 0, images.Length))
            {
                using (Image img = Image.FromStream(ms))
                {


                    using (Bitmap b = new Bitmap(img, new Size(width, width)))
                    {
                        using (MemoryStream ms2 = new MemoryStream())
                        {
                            b.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                            imageBytes = ms2.ToArray();
                        }
                    }
                }
            }

            return imageBytes;
        }

    }
}