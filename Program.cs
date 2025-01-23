
using Microsoft.Data.SqlClient;  // Ado

class Program
{

    static void Main(string[] args)
    {

        Console.WriteLine("Data base CRUD Console App.... intializing");


        Console.WriteLine("Option 1 : Create User!  press 1");
        Console.WriteLine("Option 2 : Get User! press 2");


        Console.WriteLine("Kindly Select Your Option!");

        string option = Console.ReadLine();

        if (option == "1")
        {

            Console.WriteLine("Kindly Fill the Details of User!");

            Console.WriteLine("Enter your username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter your email:");
            string email = Console.ReadLine();

            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();


            string encryptThePass = BCrypt.Net.BCrypt.HashPassword(password);

            string connectionString = "Server=db12888.public.databaseasp.net; Database=db12888; User Id=db12888; Password=3Qn%#j2RmZ-5; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;";

            CreateUser(connectionString, username, email, encryptThePass);

        }
        else if (option == "2")
        {

            string connectionString = "Server=db12888.public.databaseasp.net; Database=db12888; User Id=db12888; Password=3Qn%#j2RmZ-5; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;";

            Console.WriteLine("Enter email to find user!:");
            string email = Console.ReadLine();

            FindUser(connectionString, email);

        }
        else{
             Console.WriteLine("Kindly Select an appropriate option! ");

        }


    }

    static void CreateUser(string connectionString, string username, string email, string password)
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


    static void FindUser(string connectionString, string email)
    {

        try
        {
            // string mewooo = $"meowowo ${connectionString}";

            // string query = $"SELECT FROM Users WHERE Email = ${email}";

            string query = "SELECT * FROM Users WHERE Email = @Email";

            using SqlConnection connection = new(connectionString);

            connection.Open();

            using SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@Email", email);

            SqlDataReader reader = command.ExecuteReader();


            if (reader.Read())
            {
                Console.WriteLine("User Found! Below are the Details of User");
                Console.WriteLine(reader["Username"].ToString());
                Console.WriteLine(reader["Email"].ToString());
                Console.WriteLine(reader["Password"].ToString());

            }
            else
            {
                Console.WriteLine("No User Found With this Email!");
            }



        }
        catch (Exception error)
        {

            Console.WriteLine(error.Message);
        }


    }



    // create a void function in which we are finding user by its email and then delete if found otherwise say "no user Found!



}
