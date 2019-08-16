using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adding
{
    public class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();

            Console.WriteLine("Enter up to 2 numbers to add.");
            calculator.Input = Console.ReadLine();

            Console.WriteLine(calculator.AddNumbers());

            Console.Write("\nPress enter to exit.");
            Console.ReadLine();
        }
    }
}
