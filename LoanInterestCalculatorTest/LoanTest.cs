using System;
using NUnit.Framework;
using LoanInterestCalculator;

namespace LoanInterestCalculatorTest
{
    [TestFixture]
    public class LoanTest
    {

        [TestCase(1000, .06, 12, 5)]
        [TestCase(1345, .11, 12, 12.33)]
        [TestCase(103741, .05, 12, 432.25)]
        [TestCase(103741, .05, 26, 199.50)]
        [TestCase(100000, .06, 12, 500)]
        [Category("Test_InterestPayment")]
        public void Test_PaymentOfInterestCalculation(double principle, double interestRate, int periodicPayments, double expected)
        {
            Loan newLoan = new Loan(principle, interestRate, 1, periodicPayments);

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
            Loan newLoan = new Loan(principle, interestRate, loanLength);
            newLoan.calcBasicLoan();
            double totalRepayment = newLoan.TotalRepayment;

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
            Loan loan = new Loan(principle, interestRate, loanLengthInYears);
            
            loan.calcMonthlyPayment();
            double actual = loan.MonthlyPayment;

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(1000, .05, 1, 12)]
        [TestCase(13412, .08, 7, 84)]
        [TestCase(193748, .075, 15, 180)]
        [Category("Test_AmoritazationEntryCount")]
        public void Test_AmmoritazationCreatesCorrectNumberOfEntries(double principle, double interestRate, double loanLengthInYears, double expected)
        {
            Loan loan = new Loan(principle, interestRate, loanLengthInYears);

            loan.calculateAmmoritazation();

            Assert.That(loan.AmortizationList.Count, Is.EqualTo(expected));
        }

        [TestCase(1000, .05, 1, 27.29)]
        [TestCase(13412, .08, 7, 4147.55)]
        [TestCase(193748, .075, 15, 129544.22)]
        [Category("Test_AmoritazationTotalInterest")]
        public void Test_AmmoritazationHasCorrectInterestTotal(double principle, double interestRate, double loanLengthInYears, double expected)
        {
            Loan loan = new Loan(principle, interestRate, loanLengthInYears);

            loan.calculateAmmoritazation();
            double calculatedTotalInterest = loan.TotalInterest;

            Assert.That(calculatedTotalInterest, Is.EqualTo(expected).Within(.5));
        }

        [TestCase(1000, .05, 1, 27.29)]
        [Category("Test_AdditionalPayments")]
        public void Test_AnyAdditionalPaymentsIsNull(double principle, double interestRate, double loanLengthInYears, double expected)
        {
            Loan loan = new Loan(principle, interestRate, loanLengthInYears);

            Assert.IsEmpty(loan.AdditionalPayments);

        }

        [TestCase(1000, .05, 1, 27.29)]
        [Category("Test_AdditionalPayments")]
        public void Test_AnyAdditionalPaymentsIsNotNull(double principle, double interestRate, double loanLengthInYears, double expected)
        {
            Loan loan = new Loan(principle, interestRate, loanLengthInYears);
            loan.AdditionalPayments = new System.Collections.Generic.List<AdditionalPayment>();

            OneTimePayment oneTimePayment = new OneTimePayment();
            loan.AdditionalPayments.Add(oneTimePayment);

            Assert.IsNotNull(loan.AdditionalPayments);

        }

        [TestCase(1000, .05, 1, 2, 800, 8.15)]
        [TestCase(13412, .08, 7, 12, 723.27, 3721.41)]
        [TestCase(193748, .075, 15, 120, 8000, 126112.40)]
        [Category("Test_AdditionalPayments")]
        public void Test_OneTimePaymentHasCorrectInterestTotal(double principle, double interestRate, double loanLengthInYears, int months, double payment, double expected)
        {
            Loan loan = new Loan(principle, interestRate, loanLengthInYears);

            loan.AdditionalPayments = new System.Collections.Generic.List<AdditionalPayment>();

            OneTimePayment oneTimePayment = new OneTimePayment(DateTime.Now.AddMonths(months), payment);
            loan.AdditionalPayments.Add(oneTimePayment);

            loan.calculateAmmoritazation();
            double calculatedTotalInterest = loan.TotalInterest;
            Assert.That(calculatedTotalInterest, Is.EqualTo(expected).Within(.5));
        }
    }
}
