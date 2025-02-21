using Microsoft.Data.SqlClient;
using WebApI.Interfaces;
using WebApI.Models;

namespace WebApI.Services;

public class SqlService : ISqlService
{
    private readonly string _connectionString;

    public SqlService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("main")
        ?? throw new Exception("Connection string 'main' is missing in configuration.");
    }

    public async Task<bool> CreateUser(User User)
    {
        try
        {
            using SqlConnection connection = new(_connectionString);
            // connection.Open();
            await connection.OpenAsync();  

            string query = "INSERT INTO Users (UserId, Username, Email, Password) VALUES (@UserId, @Username, @Email, @Password)";

            using SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@UserId", User.UserId);
            command.Parameters.AddWithValue("@Username", User.Username);
            command.Parameters.AddWithValue("@Email", User.Email);
            command.Parameters.AddWithValue("@Password", User.Password);

            // int rowsAffected = command.ExecuteNonQuery(); 
            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            throw new Exception("DbError: " + ex.Message);
        }
    }

    public async Task<User?> FindUser(string Email)
    {
        try
        {
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string query = "SELECT * FROM Users WHERE Email = @Email";
            using SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@Email", Email);

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new User
                {
                    UserId = Guid.Parse(reader["UserId"].ToString()),
                    Username = reader["Username"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString()
                };
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("DbError: " + ex.Message);
        }


    }

    public async Task<User?> FindUser(Guid UserId)
    {
        try
        {
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string query = "SELECT * FROM Users WHERE UserId = @UserId";
            using SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@UserId", UserId);

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new User
                {
                    UserId = Guid.Parse(reader["UserId"].ToString()),
                    Username = reader["Username"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString()
                };
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("DbError: " + ex.Message);
        }


    }

    public async Task<bool> DeleteUser(string Email)
    {
        try
        {
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string query = "DELETE FROM Users WHERE Email = @Email";
            using SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@Email", Email);

            return await command.ExecuteNonQueryAsync() > 0;
        }
        catch (Exception ex)
        {
            throw new Exception("DbError: " + ex.Message);
        }
    }

    public async Task<bool> UpdatePass(Guid UserId ,string Password)
    {
        try
        {
            using SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            string query = "UPDATE Users SET Password = @Password WHERE UserId = @UserId";
            using SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@UserId", UserId);

            return await command.ExecuteNonQueryAsync() > 0;
        }
        catch (Exception ex)
        {
            throw new Exception("DbError: " + ex.Message);
        }
    }

  
}
