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
            string[] numbers = SplitNums();

            int sum = 0;

            foreach (string addend in numbers)
            {
                int simplifiedNum = SimplifyNum(addend);
                sum += simplifiedNum;
            }

            return sum;
        }

        //convert missing/invalid numbers to 0
        public int SimplifyNum(string strNum)
        {
            bool isEmpty = string.IsNullOrEmpty(strNum);

            if (!isEmpty) //skip negative check if string is empty
            {
                DetectNegative(strNum);
            }

            if (isEmpty || ContainsInvalidChar(strNum)) //convert missing/invalid numbers to 0
            {
                return 0;
            }

            int castedNum = int.Parse(strNum); //cast string representation to number

            if (castedNum > 1000)
            {
                return 0;
            }

            return castedNum;
        }

        // throw exception if negative number is found
        public void DetectNegative(string strNum)
        {
            if (strNum[0] == '-') //check if first char in string is -
            {
                string negativeNumberPattern = @"^\-\d+$"; //pattern for digit-only negative number
                Regex checkValidNegativeNumber = new Regex(negativeNumberPattern);

                if (checkValidNegativeNumber.Match(strNum).Success)
                {
                    throw new NegativeNumberException("Negative number found in input.");
                }
            }
        }

        //detect if there is a non-digit character in the string
        public bool ContainsInvalidChar(string strNum)
        {
            //find match of non-digit character
            string validNumberPattern = @"\D";
            Regex checkValidNumber = new Regex(validNumberPattern);

            return checkValidNumber.IsMatch(strNum);
        }

        // split input into array of strings
        public string[] SplitNums() {

            string inputStr = Input;

            inputStr = ConvertDelimiters(inputStr);

            string[] numbersList = inputStr.Split(','); //seperate numbers by comma

            return numbersList;
        }

        //manages alternative delimiters
        public string ConvertDelimiters(string inputStr)
        {
            string numString = inputStr;
            string delimArg;

            string customDelimPattern = @"^\/\/.*?\\n"; //pattern for custom delimiter argument
            Regex delimRegex = new Regex(customDelimPattern);
            Match delimMatch = delimRegex.Match(inputStr);

            if (delimMatch.Success) //check if argument for custom delim is found
            {
                //seperate delimiter arguemnts from list of numbers to add
                delimArg = inputStr.Substring(delimMatch.Index, delimMatch.Length);
                numString = inputStr.Substring(delimMatch.Length);

                numString = ApplyCustomDelimiters(delimArg, numString);
            }

            numString = numString.Replace("\\n", ","); //replace newline characters with comma

            return numString;
        }

        //converts custom delimter to comma
        public string ApplyCustomDelimiters(string delimArg, string numString)
        {
            string delim = delimArg.Substring(2, 1); //

            return numString.Replace(delim, ","); ;
        }

    }
}
