using MailKit.Net.Smtp;
using MimeKit;
//using System.Net.Mail;

namespace MyNewsletter.Tools
{
    class EmailSender
    {
        public async static void SendEmail(string From, string To, string Subject, string Body)
        {
            await Task.Run(() =>
            {

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(From, "mytempnewsletter@gmail.com"));
                message.To.Add(new MailboxAddress("Name", To));
                message.Subject = Subject;

                message.Body = new TextPart("plain")
                {
                    Text = Body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate("mytempnewsletter@gmail.com", "mafg yqtw dbto sonf"); // Passord: #1EmailSender
                    client.Send(message);
                    client.Disconnect(true);
                }
            });
        }
        public async static void SendEmail(string From, string To, string Subject, BodyBuilder Body)
        {
            await Task.Run(() =>
            {

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(From, "mytempnewsletter@gmail.com"));
                message.To.Add(new MailboxAddress("Name", To));
                message.Subject = Subject;

                message.Body = Body.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate("mytempnewsletter@gmail.com", "mafg yqtw dbto sonf");
                    client.Send(message);
                    client.Disconnect(true);
                }
            });
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
