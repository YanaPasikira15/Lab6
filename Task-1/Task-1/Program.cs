using System;

public class Calculator<T> where T : struct
{
    public delegate T Operation(T a, T b);
    public T Add(T a, T b, Operation operation) => operation(a, b);
    public T Subtract(T a, T b, Operation operation) => operation(a, b);
    public T Multiply(T a, T b, Operation operation) => operation(a, b);
    public T Divide(T a, T b, Operation operation)
    {
        if (b.Equals(default(T)))
            throw new DivideByZeroException("Ділення на нуль!");
        return operation(a, b);
    }
}
class Program
{
    static void Main()
    {
        var intCalculator = new Calculator<int>();
        Console.WriteLine("Int Operations:");
        Console.WriteLine("Add: " + intCalculator.Add(10, 5, (a, b) => a + b));
        Console.WriteLine("Subtract: " + intCalculator.Subtract(10, 5, (a, b) => a - b));
        Console.WriteLine("Multiply: " + intCalculator.Multiply(10, 5, (a, b) => a * b));
        Console.WriteLine("Divide: " + intCalculator.Divide(10, 5, (a, b) => a / b));
        var doubleCalculator = new Calculator<double>();
        Console.WriteLine("\nDouble Operations:");
        Console.WriteLine("Add: " + doubleCalculator.Add(10.5, 5.2, (a, b) => a + b));
        Console.WriteLine("Subtract: " + doubleCalculator.Subtract(10.5, 5.2, (a, b) => a - b));
        Console.WriteLine("Multiply: " + doubleCalculator.Multiply(10.5, 5.2, (a, b) => a * b));
        Console.WriteLine("Divide: " + doubleCalculator.Divide(10.5, 2.0, (a, b) => a / b));
        var floatCalculator = new Calculator<float>();
        Console.WriteLine("\nFloat Operations:");
        Console.WriteLine("Add: " + floatCalculator.Add(10.5f, 5.2f, (a, b) => a + b));
        Console.WriteLine("Subtract: " + floatCalculator.Subtract(10.5f, 5.2f, (a, b) => a - b));
        Console.WriteLine("Multiply: " + floatCalculator.Multiply(10.5f, 5.2f, (a, b) => a * b));
        Console.WriteLine("Divide: " + floatCalculator.Divide(10.5f, 2.0f, (a, b) => a / b));
    }
}
