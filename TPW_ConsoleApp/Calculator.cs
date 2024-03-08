public class SimpleCalculator
{
    public static double Calculate(double num1, double num2, char operation)
    {
        double result = 0;

        switch (operation)
        {
            case '+':
                result = num1 + num2;
                break;
            case '-':
                result = num1 - num2;
                break;
            default:
                throw new ArgumentException("Nieprawidłowa operacja!");
        }

        return result;
    }
}
class Calculator
{
    static void Main(string[] args)
    {
        Console.WriteLine("Kalkulator");
        Console.WriteLine("------------------");
        Console.WriteLine("Podaj pierwszą liczbę:");
        double num1 = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Podaj drugą liczbę:");
        double num2 = Convert.ToDouble(Console.ReadLine());   
        Console.WriteLine("Wybierz operację (wpisz + lub -):");
        char operation = Convert.ToChar(Console.ReadLine());
        double result = 0; 
        result = SimpleCalculator.Calculate(num1, num2, operation);
        Console.WriteLine($"Wynik: {num1} {operation} {num2} = {result}");
        Console.ReadLine();
    }
}