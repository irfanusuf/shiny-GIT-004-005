
using Microsoft.Data.SqlClient;
using SqlDbConsole.Services;  // Ado

class Program
{

    static void Main(string[] args)
    {
        Console.WriteLine("Data base CRUD Console App.... intializing");
        Console.WriteLine("Option 1 : Create User!  press 1");
        Console.WriteLine("Option 2 : Get User! press 2");
        Console.WriteLine("Option 3 : Delete User! press 3");
        Console.WriteLine("Option 4 : Edit Your Account press 4");
        Console.WriteLine("Kindly Select Your Option!");

        string option = Console.ReadLine();

        string connectionString = "";
 
        SqlService sqlService = new(connectionString);


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

            sqlService.CreateUser( username, email, encryptThePass);

        }
        else if (option == "2")
        {

            Console.WriteLine("Enter email to find user!:");
            string email = Console.ReadLine();

            sqlService.FindUser(email);

        }
        else if (option == "3")
        {
            // third option logic is here 

            Console.WriteLine("Enter email to delete User Account! :");
            string email = Console.ReadLine();
        
            sqlService.DeleteUser( email);

        }
        else if (option == "4")
        {

            // logic of edit User


            Console.WriteLine("Enter your email to find user");
            string email = Console.ReadLine();


            Console.WriteLine("Enter your new  username:");
            string newUserName = Console.ReadLine();

            Console.WriteLine("Enter your new  password:");
            string password = Console.ReadLine();


            var encryptednewPass = BCrypt.Net.BCrypt.HashPassword(password);


            // we will be taking email new username and new password from user 

            // password will be encrypted 


            sqlService.EditUserAccount( email , newUserName , encryptednewPass);

        }
        else
        {
            Console.WriteLine("Kindly Select an appropriate option! ");

        }
    }
}
