using System;
using WebApI.Models;

namespace WebApI.Interfaces;

public interface ISqlService 
{
  public bool CreateUser(string username, string email, string password);
  public User FindUser(string email);
  public bool DeleteUser(string email);
  public void EditUserAccount(string email, string username, string password);
  public void EditUsername (string Email , string Username);
  public void ForgotPassword(string Email );
  public void ForgotPassword(string Email , string newPass);

}
