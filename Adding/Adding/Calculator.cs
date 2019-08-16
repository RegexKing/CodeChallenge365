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

            if (isEmpty || ContainsInvalidChar(strNum)) //Convert missing/invalid numbers to 0
            {
                return 0;
            }

            int castedNum = Int32.Parse(strNum);

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

            return checkValidNumber.Match(strNum).Success;
        }

        // split input into array of strings
        public string[] SplitNums() {

            string inputStr = Input.Replace("\\n", ","); //replace newline characters with comma

            string[] numbersList = inputStr.Split(','); //seperate numbers by comma

            return numbersList;
        }
    }
}
