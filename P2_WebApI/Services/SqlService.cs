
using Microsoft.Data.SqlClient;
using WebApI.Interfaces;
using WebApI.Models;

namespace WebApI.Services;


// class tempeltae hai object ka 
// object hamesha inheritance leta hai class se ya inteerfcae se
// base class 
public class SqlService : ISqlService
{
    private readonly string _connectionString;     // feild   // encapsulation // class ki state ko store kertay hai 


    // constructorr
    public SqlService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("main") ;
    }


    public bool CreateUser(User user)
    {
        try
        {
            using SqlConnection connection = new(_connectionString);  // intiliazing a connection  and creatimng a new instance of Sql Connection 
            connection.Open();

            string query = "INSERT INTO Users (UserId , Username, Email, Password) VALUES (@UserId, @Username, @Email, @Password)";

            using SqlCommand command = new(query, connection);


            command.Parameters.AddWithValue("@UserId", user.UserId);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("User Saved Succesfully !");
                return true;

            }
            else
            {
                Console.WriteLine("Query Not Executed");
                return false;
            }



        }
        catch (Exception error)
        {

            Console.WriteLine(error.Message);
            return false;
        }
    }

    public User FindUser(string email)
    {

        var EmptyUser = new User
        {

            Username = "",
            Email = "",
            Password = ""
        };
        try
        {
            string query = "SELECT * FROM Users WHERE Email = @Email";

            using SqlConnection connection = new(_connectionString);

            connection.Open();

            using SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@Email", email);

            SqlDataReader reader = command.ExecuteReader();


            if (reader.Read())
            {

                var User = new User
                {
                    Username = reader["Username"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString()

                };

                return User;
            }
            else
            {
                return EmptyUser;
            }


        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
            return EmptyUser;

        }


    }
  
    public User FindUser(Guid UserId)
    {

        var EmptyUser = new User
        {
            Username = "",
            Email = "",
            Password = ""
        };
        try
        {
            string query = "SELECT * FROM Users WHERE UserId = @UserId";

        

            using SqlConnection connection = new(_connectionString);

            connection.Open();

            using SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@UserId", UserId);

            SqlDataReader reader = command.ExecuteReader();


            if (reader.Read())
            {

                    Console.WriteLine(UserId);

                var User = new User
                {
                    Username = reader["Username"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString()

                };

                

                return User;
            }
            else
            {
                return EmptyUser;
            }


        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
            return EmptyUser;

        }


    }
  
    public bool DeleteUser(string email)
    {

        try
        {
            // connect to db 
            SqlConnection connection = new(_connectionString);
            var query = "DELETE FROM Users WHERE Email = @Email";  // we dont take email directly from params because to prevent sql injection 


            connection.Open();

            SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@Email", email);   // malicious query ka security check

            int rowsDeleted = command.ExecuteNonQuery();    // return rows deleted

            if (rowsDeleted > 0)
            {
               return true;
            }
            else
            {
              return false;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

    }


    public bool UpdatePass(string password){

        return true;

    }


}
