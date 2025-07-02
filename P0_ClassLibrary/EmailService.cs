using System;
using System.Net;
using System.Net.Mail;

using P0_ClassLibrary.Interfaces;



public class EmailService : IMailService
{


    // defining  private readonly feilds 
    private readonly string smtpHost;
    private readonly int smtpPort;
    private readonly string smtpUsername;
    private readonly string smtpPassword;



    public EmailService(string smtpHost, int smtpPort, string smtpUsername, string smtpPassword)
    {


        this.smtpHost = smtpHost;
        this.smtpPort = smtpPort;
        this.smtpUsername = smtpUsername;
        this.smtpPassword = smtpPassword;


    }

    public async Task SendEmailAsync(string emailAddress, string subject, string body, bool isHtml = false)
    {

        try
        {
            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpUsername),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml,
            };

            mailMessage.To.Add(emailAddress);

            await client.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to send email: {ex.Message}");
        }

    }
}
