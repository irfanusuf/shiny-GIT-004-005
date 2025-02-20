using System;
using WebApI.Models;

namespace WebApI.Interfaces;

public interface ISqlService 
{
  public bool CreateUser(User user);
  public User FindUser(string email);
  public User FindUser(Guid UserId);
  public bool DeleteUser(string email);
  public bool UpdatePass(string password);



}
