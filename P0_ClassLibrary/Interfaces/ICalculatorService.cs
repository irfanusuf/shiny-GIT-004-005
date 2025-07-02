using System;

namespace P0_ClassLibrary;

public interface ICalculatorService
{

    public int Add(int a, int b);
    public int Sub(int a, int b);
    public int Multiply(int a, int b);
    public double Divide(int a, int b);

}
