using System;
using NUnit.Framework;
using LoanInterestCalculator;

namespace LoanInterestCalculatorTest
{
    [TestFixture]
    public class TestLoan
    {

        [TestCase(1000, .06, 12, 5)]
        [TestCase(1345, .11, 12, 12.33)]
        [TestCase(103741, .05, 12, 432.25)]
        [TestCase(103741, .05, 26, 199.50)]
        [TestCase(100000, .06, 12, 500)]
        [Category("Test_InterestPayment")]
        public void Test_PaymentOfInterestCalculation(double principle, double interestRate, int periodicPayments, double expected)
        {
            Loan newLoan = new Loan(principle, interestRate, DateTime.Today.Date, 1, periodicPayments);

            double interestPayment = newLoan.calcPaymentOfInterest();

            Assert.That(interestPayment, Is.EqualTo(expected));

        }

        [TestCase(1000, .05, 1, 1027.32)]
        [TestCase(13412, .08, 7, 17559.36)]
        [TestCase(193748, .075, 15, 323292.60)]
        [TestCase(13412, .08, 5.5, 16622.10)]
        [Category("Test_TotalRepayment")]
        public void Test_TotalRepaymentCalculation(double principle, double interestRate, double loanLength, double expected)
        {
            Loan newLoan = new Loan(principle, interestRate, DateTime.Today.Date, loanLength);

            double totalRepayment = newLoan.calcTotalRepayment();

            Assert.That(totalRepayment, Is.EqualTo(expected));
        }

        [TestCase(1000, .05, 1, 85.61)]
        [TestCase(13412, .08, 7, 209.04)]
        [TestCase(193748, .075, 15, 1796.07)]
        [TestCase(13412, .08, 5.5, 251.85)]
        [TestCase(30000, .05, 5, 566.14)]
        [Category("Test_MonthlyPayment")]
        public void Test_MonthlyPaymentCalculation(double principle, double interestRate, double loanLengthInYears, double expected)
        {
            Loan loan = new Loan(principle,interestRate, DateTime.Today.Date, loanLengthInYears);

            double actual = loan.calcMonthlyPayment();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(1000, .05, 1, 12)]
        [TestCase(13412, .08, 7, 84)]
        [TestCase(193748, .075, 15, 180)]
        [Category("Test_AmoritazationEntryCount")]
        public void Test_Ammoritazation_CreatesCorrectNumberOfEntries(double principle, double interestRate, double loanLengthInYears, double expected)
        {
            Loan loan = new Loan(principle, interestRate, DateTime.Today.Date, loanLengthInYears);

            loan.calculateAmmoritazation();

            Assert.That(loan.AmortizationList.Count, Is.EqualTo(expected));
        }

        [TestCase(1000, .05, 1, 27.29)]
        [TestCase(13412, .08, 7, 4147.55)]
        [TestCase(193748, .075, 15, 129544.22)]
        [Category("Test_AmoritazationTotalInterest")]
        public void Test_Ammoritazation_HasCorrectInterestTotal(double principle, double interestRate, double loanLengthInYears, double expected)
        {
            Loan loan = new Loan(principle, interestRate, DateTime.Today.Date, loanLengthInYears);

            loan.calculateAmmoritazation();
            double calculatedTotalInterest = loan.TotalInterest;

            Assert.That(calculatedTotalInterest, Is.EqualTo(expected).Within(.5));
        }
    }
}
