using Microsoft.VisualStudio.TestTools.UnitTesting;
using Adding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adding.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void AddNumbers_InputTwoNumbers_ReturnCorrectSum()
        {
            Calculator calculator = new Calculator();
            calculator.Input = "1,20";
            int expectedSum = 21;

            int calculatedSum = calculator.AddNumbers();

            Assert.AreEqual(expectedSum, actual: calculatedSum);
        }

        [TestMethod()]
        public void AddNumbers_InputMoreThanTwoNumbers_ReturnCorrectSum()
        {
            Calculator calculator = new Calculator();
            calculator.Input = "2,4,5,6,7";
            int expectedSum = 24;

            int calculatedSum = calculator.AddNumbers();

            Assert.AreEqual(expectedSum, actual: calculatedSum);
        }

        [TestMethod()]
        public void SimplifyNum_CheckMissingNumber_ReturnZero()
        {
            Calculator calculator = new Calculator();
            string missingNumber = "";
            int expectedConversion = 0;

            int conversion = calculator.SimplifyNum(missingNumber);

            Assert.AreEqual(expectedConversion, actual: conversion);
        }

        [TestMethod()]
        public void SimplifyNum_CheckInvalidNumber_ReturnZero()
        {
            Calculator calculator = new Calculator();
            string invalidNumber = "foxygrandpa";
            int expectedConversion = 0;

            int conversion = calculator.SimplifyNum(invalidNumber);

            Assert.AreEqual(expectedConversion, actual: conversion);
        }

        [TestMethod()]
        public void ContainsInvalidChar_SingleLetterInInput_ReturnTrue()
        {
            Calculator calculator = new Calculator();
            string invalidNumber = "12c4";

            Assert.IsTrue(calculator.ContainsInvalidChar(invalidNumber));
        }

        [TestMethod()]
        public void AddNumbers_InputMissingNumbers_ReturnZero()
        {
            Calculator calculator = new Calculator();
            calculator.Input = "";
            int expectedSum = 0;

            int calculatedSum = calculator.AddNumbers();

            Assert.AreEqual(expectedSum, actual: calculatedSum);
        }

        [TestMethod()]
        public void AddNumbers_InputInvalidNumber_OmitInvalidNumber()
        {
            Calculator calculator = new Calculator();
            calculator.Input = "5,tytyt";
            int expectedSum = 5;

            int calculatedSum = calculator.AddNumbers();

            Assert.AreEqual(expectedSum, actual: calculatedSum);
        }

        [TestMethod()]
        public void AddNumbers_UseNewLineDelimeter_ReturnCorrectSum()
        {
            Calculator calculator = new Calculator();
            calculator.Input = "1\\n2,3";
            int expectedSum = 6;

            int calculatedSum = calculator.AddNumbers();

            Assert.AreEqual(expectedSum, actual: calculatedSum);
        }

        [TestMethod()]
        public void SplitNums_ConvertMultipleNewLines_ReturnCorrectFormat()
        {
            Calculator calculator = new Calculator();
            calculator.Input = "1\\n2,3\\n\\n5,6\\n7";
            string[] expectedNums = { "1", "2", "3", "", "5", "6", "7" }; //expecting missing num from 2 adjacent newline characters

            string[] splitNums = calculator.SplitNums();

            CollectionAssert.AreEqual(splitNums, expectedNums, "Something went wrong in the split: ");
        }

        [TestMethod()]
        public void DetectNegative_findNegativeNumber_ThrowsException()
        {
            Calculator calculator = new Calculator();
            string num = "-13";

            Assert.ThrowsException<NegativeNumberException>(() => calculator.DetectNegative(num));
        }

        [TestMethod()]
        public void AddNumbers_InputContainsNegativeNumber_ThrowsException()
        {
            Calculator calculator = new Calculator();
            calculator.Input = "23,22,-1";

            Assert.ThrowsException<NegativeNumberException>(() => calculator.AddNumbers());
        }

        [TestMethod()]
        public void AddNumbers_InputContainsNegativeCharOnly_ReturnCorrectSum()
        {
            Calculator calculator = new Calculator();
            calculator.Input = "7,-,13"; //expecting - to be evaluated as invalid char
            int expectedSum = 20;

            int calculatedSum = calculator.AddNumbers();

            Assert.AreEqual(expectedSum, actual: calculatedSum);
        }

        [TestMethod()]
        public void AddNumbers_AddNumberGreaterThanThousand_ReturnCorrectSum()
        {
            Calculator calculator = new Calculator();
            calculator.Input = "2,1001,6";
            int expectedSum = 8;

            int calculatedSum = calculator.AddNumbers();

            Assert.AreEqual(expectedSum, actual: calculatedSum);
        }

        [TestMethod()]
        public void AddNumbers_UsingSingleCustomDelimter_ReturnCorrectSum()
        {
            Calculator calculator = new Calculator();
            calculator.Input = "//;\\n2;5";
            int expectedSum = 7;

            int calculatedSum = calculator.AddNumbers();

            Assert.AreEqual(expectedSum, actual: calculatedSum);
        }

        [TestMethod()]
        public void AddNumbers_UsingMissingCustomDelimter_ReturnCorrectSumIgnoreDelimterArg()
        {
            Calculator calculator = new Calculator();
            calculator.Input = "//\\n123,21"; // expected there are no custom delimiters specified
            int expectedSum = 144;

            int calculatedSum = calculator.AddNumbers();

            Assert.AreEqual(expectedSum, actual: calculatedSum);
        }

        [TestMethod()]
        public void AddNumbers_UsingGreaterLengthCustomDelimter_ReturnCorrectSum()
        {
            Calculator calculator = new Calculator();
            calculator.Input = "//[***]\\n11***22***33";
            int expectedSum = 66;

            int calculatedSum = calculator.AddNumbers();

            Assert.AreEqual(expectedSum, actual: calculatedSum);
        }

        [TestMethod()]
        public void AddNumbers_MoreThanOneDelimterArgument_ReturnCorrectSumIgnoreOtherDelimterArgument()
        {
            Calculator calculator = new Calculator();
            calculator.Input = "//;\\n//a\\n2;5";
            int expectedSum = 7;

            int calculatedSum = calculator.AddNumbers();

            Assert.AreEqual(expectedSum, actual: calculatedSum);
        }

        [TestMethod()]
        public void ConvertDelimiters_UnclosedRightBracket_ReturnNotEqualToAssumedForm()
        {
            Calculator calculator = new Calculator();
            string input = "//[*;\n12#;4"; //user makes a typo forgets to close bracket
            string assumedForm = "12,4";

            string formattedNum = calculator.ConvertDelimiters(input);

            Assert.AreNotEqual(assumedForm, actual: formattedNum);
        }

        [TestMethod()]
        public void ApplyCustomDelimiters_MixedCharactersInDelimArg_ReturnCorrectStringFormat()
        {
            Calculator calculator = new Calculator();
            string delimArgTest  = "//[a*b[]\\n"; //testing with a left bracket in delimter entry
            string numStringTest = "10a*b[4";
            string expectedString = "10,4";

            string formattedNum = calculator.ApplyCustomDelimiters(delimArgTest, numStringTest);

            Assert.AreEqual(expectedString, actual: formattedNum);
        }

        [TestMethod()]
        public void AddNumbers_UsingMultipleCustomDelimter_ReturnCorrectSum()
        {
            Calculator calculator = new Calculator();
            calculator.Input = "//[*][!!][rrr]\\n11rrr22*33!!44";
            int expectedSum = 110;

            int calculatedSum = calculator.AddNumbers();

            Assert.AreEqual(expectedSum, actual: calculatedSum);
        }

        [TestMethod()]
        public void GetDelimList_MissingOpeningBracket_ReturnNull()
        {
            Calculator calculator = new Calculator();
            string input = "*][!!][rrr]";

            Assert.IsNull(calculator.GetDelimList(input));
        }

        [TestMethod()]
        public void GetDelimList_InvalidDelimiterFormat_ReturnNull()
        {
            Calculator calculator = new Calculator();
            string input = "[ab*]][!!]"; //extra closing bracket

            Assert.IsNull(calculator.GetDelimList(input));
        }

        [TestMethod()]
        public void GetDelimList_ValidDelimiterFormat_ReturnNotNull()
        {
            Calculator calculator = new Calculator();
            string input = "[123][::!][#ab][**!][!@#%^&*()]";

            Assert.IsNotNull(calculator.GetDelimList(input));
        }
    }
}