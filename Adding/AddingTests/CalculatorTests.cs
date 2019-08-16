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
            string[] expectedNums = { "1", "2", "3", "", "5", "6", "7" }; //expecting missing num with 2 adjacent newline characters

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
            calculator.Input = "23,-4";

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
    }
}