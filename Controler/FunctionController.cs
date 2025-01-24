using System;

namespace simpleConsole.Controler;

public class FunctionController
{

    public static string Something()
    {
        try
        {
            return "Something is happenng!";
        }
        catch (Exception error)
        {

            Console.WriteLine(error);
            return error.Message;
        }
    }


    public static void SomethingElse()
    {
        try
        {
            Console.WriteLine("Something Else is saying hellllo World");
        }
        catch (Exception error)
        {

            Console.WriteLine(error.Message);
        }
    }


    public static bool Loading()
    {
        try
        {
            var x = 88;

            var y = 12;

            if (x + y == 100)
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


}




