using System;
using NUnit.Framework;
using LoanInterestCalculator;
using System.Collections.Generic;

namespace LoanInterestCalculatorTest
{
    [TestFixture]
    public class LoansTest
    {

        [Test]
        [Category("Test_MultipleLoans")]
        public void Test_MultipleLoansCalculateCorrectly()
        {
            Loans loans = new Loans();
            loans.Add(new Loan(15000, .05, 7));
            loans.Add(new Loan(5000, .06, 5));
            loans.Add(new Loan(75000, .045, 10));
            loans.Add(new Loan(25000, .10, 4));

            List<double> interest = new List<double>() { 2808.73, 799.89, 18274.52, 5435.14 };
            List<double> monthlyPayment = new List<double>() { 212.01, 96.66, 777.29, 634.06 };
            //[TestCase(15000, .05, 7, 2808.73, 212.01)]
            //[TestCase(5000, .06, 5, 799.89, 96.66)]
            //[TestCase(75000, .045, 10, 18274.52, 777.29)]
            //[TestCase(25000, .10, 4, 5435.14, 634.06)]

            loans.CalculateLoans();

            int counter = 0;
            foreach (Loan loan in loans)
            {
                Assert.That(loan.TotalInterest, Is.EqualTo(interest[counter]).Within(.5));
                Assert.That(loan.MonthlyPayment, Is.EqualTo(monthlyPayment[counter]));
                counter++;
            }
        }
    }
}
