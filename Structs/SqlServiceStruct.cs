using System;
using SqlDbConsole.Interfaces;

namespace SqlDbConsole.Structs;

public struct SqlServiceStruct : ISqlService
{
    public void CreateUser(string username, string email, string password)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser(string email)
    {
        throw new NotImplementedException();
    }

    public void EditUserAccount(string email, string username, string password)
    {
        throw new NotImplementedException();
    }

    public void EditUsername(string Email, string Username)
    {
        throw new NotImplementedException();
    }

    public void FindUser(string email)
    {
        throw new NotImplementedException();
    }

    public void ForgotPassword(string Email)
    {
        throw new NotImplementedException();
    }

    public void ForgotPassword(string Email, string newPass)
    {
        throw new NotImplementedException();
    }
}
