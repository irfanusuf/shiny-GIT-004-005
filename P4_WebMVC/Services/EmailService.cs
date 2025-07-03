using System;
using System.Net;
using System.Net.Mail;
using dotenv.net;
using Microsoft.AspNetCore.Mvc;
using P4_WebMVC.Interfaces;


namespace P4_WebMVC.Services;

public class EmailService : IMailService
{


    // defining  private readonly feilds 
    private readonly string smtpHost;
    private readonly int smtpPort;
    private readonly string smtpUsername;
    private readonly string smtpPassword;



    public EmailService()
    {
        // Load environment variables
        DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));

        smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST") ?? throw new InvalidOperationException("SMTP_HOST is not configured.");

        smtpPort = int.TryParse(Environment.GetEnvironmentVariable("SMTP_PORT"), out var port) ? port : 587;

        smtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME") ?? throw new InvalidOperationException("SMTP_USERNAME is not configured.");

        smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? throw new InvalidOperationException("SMTP_PASSWORD is not configured.");


    }

    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = false)
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

            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to send email: {ex.Message}");
        }
        
    }
}
    