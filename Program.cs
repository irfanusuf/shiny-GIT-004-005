
using System.Diagnostics;
using simpleConsole.Controler;



namespace simpleConsole;
class Program ()
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Enter a number for the loop:");

            int number = int.Parse(Console.ReadLine());

            Stopwatch stopwatch = new();  // we are intializing new instance of stopwatch

            Console.WriteLine("Starting the loop...");

            stopwatch.Start();  // stop watch ko hum ne start kiya 

            int i = 0;

            while (i < number)
            {
                bool isStopWatchRunning = stopwatch.IsRunning;
                Console.WriteLine("Hello World from program.cs");
                Console.Write("Timer is Running :");
                Console.WriteLine(isStopWatchRunning);
                i++;
            }

            stopwatch.Stop();   // stop watch ko stop kiya 


            Console.WriteLine("Loop completed.");
            Console.WriteLine("Time elapsed:" + stopwatch.ElapsedMilliseconds + "milli seconds");

           
            FunctionController functionController =new();    // new instance of function controller intialized here


             FunctionController.SomethingElse();     // it returns nothing it is a void function 


             var message =   FunctionController.Something();     // returns  string which is saved in message

             Console.WriteLine(message);


            var loopoading =  FunctionController.Loading();      // returns  bool which is saved in loopoading

            Console.WriteLine(loopoading);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally{
         Console.WriteLine("All the Programs are running if u are seeing the Error kindly check the Catch block  ");   
        }
    }

    

}