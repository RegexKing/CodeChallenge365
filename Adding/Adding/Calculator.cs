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
        public List<int> addendList { get; } //list to hold numbers for formula

        private static readonly string _delimArgStart = "//";
        private static readonly string _delimArgEnd = "\\n";
        private static readonly char _bracketOpen = '[';
        private static readonly char _bracketEnd = ']';

        public Calculator()
        {
            addendList = new List<int>();
        }

        //loadNumbers
        public string DisplayFormula(string formattedStringNum)
        {
            Input = formattedStringNum;
            int sum = AddNumbers();
            string formula = "";

            for (int i = 0; i < addendList.Count; i++)
            {
                formula += addendList[i];

                if (i == addendList.Count - 1)
                {
                    formula += " = "; //append "=" at end of list
                }
                else
                {
                    formula += "+"; //appdend "+" between nums
                }
            }

            formula += sum;

            return formula;
        }

        //add numbers from a formatted string
        public int AddNumbers()
        {
            string[] numbers = SplitNums();

            int sum = 0;

            foreach (string addend in numbers)
            {
                int simplifiedNum = SimplifyNum(addend); //reduces invalid/missing numbers to 0
                addendList.Add(simplifiedNum);
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
            if (strNum[0] == '-') //check if first char in string is "-"
            {
                //pattern for digit-only negative number
                Regex checkValidNegativeNumber = new Regex(@"^\-\d+$");

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
            Regex checkValidNumber = new Regex(@"\D");

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

            //pattern for custom delimiter argument
            Regex delimRegex = new Regex(@"^\/\/.*?\\n");
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
            //trim the delim argument
            int startIndex = _delimArgStart.Length;
            int endIndex = delimArg.Length - _delimArgEnd.Length - startIndex;
            string delim = delimArg.Substring(startIndex, endIndex);

            //if custom delim is empty, delim arg is an invalid number
            if (string.IsNullOrEmpty(delim))
            {
                return numString;
            }

            //check if only a single delimter character is specified
            if (delim.Length == 1)
            {
                return numString.Replace(delim, ",");
            }

            //custom delimters legnth > 2
            if (delim.Length > 2)
            {
                List<string> delimList = GetDelimList(delim); //build list of delimiters to use

                if (delimList == null) //check if multiple delimter format is invalid
                {
                    return numString;
                }

                foreach (string delimEntry in delimList)
                {
                    numString = numString.Replace(delimEntry, ","); //replace each custom delimiter with comma
                }
            }

            return numString;
        }

        //breaks up string of bracketed delimiters into a list
        public List<string> GetDelimList(string delim)
        {
            List<string> delimList = new List<string>();
            Queue<char> queue = new Queue<char>(delim);

            //if delimter collection begin with "[", delim arg is an invalid number
            if (queue.Dequeue() != _bracketOpen)
            {
                return null;
            }

            string delimBuilder = ""; //holds delimiter constructions
            bool isBuilding = true; //flag for when constructing each delimiter

            while (queue.Count > 0)
            {
                char nextChar = queue.Dequeue();

                if (nextChar != _bracketEnd && isBuilding) //checking when character is within a set of brackets
                {
                    delimBuilder += nextChar;
                }
                else if (nextChar == _bracketEnd && isBuilding) //reaching the end of a bracketed delimter entry
                {
                    delimList.Add(delimBuilder);

                    //reset values to search for new delimiter
                    isBuilding = false;
                    delimBuilder = "";
                }
                else if (nextChar == _bracketOpen && !isBuilding) //check to start new construction of delimiter
                {
                    isBuilding = true;
                }
                else
                {
                    return null; //delim arg is not formatted with brackets properly
                }
            }

            return delimList;
        }
    }
}
