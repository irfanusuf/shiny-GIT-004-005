using System;
using dotenv.net;
using SqlDbConsole.Interfaces;

namespace SqlDbConsole.Structs;

public struct EnvService : ILoadEnv
{
    public readonly string LoadEnv(string envVariable)
    {
        try
        {

            DotEnv.Load(options : new DotEnvOptions(probeForEnv : true));

            Console.WriteLine("Env Load Complete!");

            return  Environment.GetEnvironmentVariable(envVariable);

            // DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            // string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            // return connectionString;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
