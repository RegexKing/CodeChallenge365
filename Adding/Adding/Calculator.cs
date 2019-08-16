using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adding
{
    public class Calculator
    {
        public string Input { get; set; }

        public Calculator() { }

        //add numbers from a formatted string
        public int AddNumbers()
        {
            string[] numbers = Input.Split(','); //seperate numbers by comma

            int sum = 0;

            string formattedNum1 = SimplifyNum(numbers[0]);
            int num1 = Int32.Parse(formattedNum1);

            sum += num1;

            //add second number if it exists
            if (numbers.Length > 1)
            {
                string formattedNum2 = SimplifyNum(numbers[1]);
                int num2 = Int32.Parse(formattedNum2);

                sum += num2;
            }

            return sum;
        }

        //convert missing/invalid numbers to 0
        public string SimplifyNum(string strNum)
        {
            if (strNum == "") //Convert missing numbers to 0
            {
                return "0";
            }

            //detect if there is a non-digit character in the string
            string validNumberPattern = @"\D";
            Regex checkValidNumber = new Regex(validNumberPattern);

            if (checkValidNumber.Match(strNum).Success) //convert invalid number to 0
            {
                return "0";
            }

            return strNum;
        }
    }
}
