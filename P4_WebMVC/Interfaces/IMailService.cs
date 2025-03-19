using System;

namespace P4_WebMVC.Interfaces;

public interface IMailService
{
public Task SendEmailAsync(string to, string subject, string body, bool isHtml = true);


}
