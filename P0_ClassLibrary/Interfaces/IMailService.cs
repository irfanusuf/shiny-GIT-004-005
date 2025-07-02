using System;

namespace P0_ClassLibrary.Interfaces;

public interface IMailService
{
public Task SendEmailAsync(string emailAddress, string subject, string body, bool isHtml = true);


}
