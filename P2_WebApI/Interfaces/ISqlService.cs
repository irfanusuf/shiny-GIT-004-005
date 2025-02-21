using System;
using WebApI.Models;

namespace WebApI.Interfaces;

public interface ISqlService
{
  Task<bool> CreateUser(User User);
  Task<User?> FindUser(string mail);
  Task<User?> FindUser(Guid UserId);
  Task<bool> DeleteUser(string Email);
  Task<bool> UpdatePass(Guid UserId, string Password);

}
