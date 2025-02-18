using System;

namespace WebApI.Interfaces;

public interface IMailService
{
public Task SendEmailAsync(string to, string subject, string body, bool isHtml = true);


}
