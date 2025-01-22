
using Microsoft.Data.SqlClient;  // Ado

class Program
{

    static void Main(string[] args)
    {

        Console.WriteLine("Enter your username:");
        string username = Console.ReadLine();

        Console.WriteLine("Enter your email:");
        string email = Console.ReadLine();

        Console.WriteLine("Enter your password:");
        string password = Console.ReadLine();


        string encryptThePass = BCrypt.Net.BCrypt.HashPassword(password);

        string connectionString = "Server=db12888.public.databaseasp.net; Database=db12888; User Id=db12888; Password=3Qn%#j2RmZ-5; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;";

        ConnectDb(connectionString, username, email, encryptThePass);

    }

    static void ConnectDb(string connectionString, string username, string email, string password)
    {
        try
        {
            Console.WriteLine("Trying to connect Db !");

            using SqlConnection connection = new(connectionString);  // intiliazing a connection  and creatimng a new instance of Sql Connection 
            
            connection.Open();

            string query = "INSERT INTO Users (Username, Email, Password) VALUES (@Username, @Email, @Password)";

            using SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", password);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("User Saved Succesfully !");
            }
            else
            {
                Console.WriteLine("Query Not Executed");
            }



        }
        catch (Exception error)
        {

            Console.WriteLine(error.Message);
        }
    }


}
