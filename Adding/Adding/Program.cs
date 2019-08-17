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

            Console.WriteLine("Enter numbers to add: ");
            string formula = calculator.DisplayFormula(Console.ReadLine());

            Console.WriteLine(formula);

            Console.Write("\nPress enter to exit.");
            Console.ReadLine();
        }
    }
}
