using System;
using NUnit.Framework;
using LoanInterestCalculator;

namespace LoanInterestCalculatorTest
{
    [TestFixture]
    public class LoanTest
    {
        [Test]
        public void TestCreationOfLoanIsNotNull() 
        {
            Loan loan = new Loan();

            Assert.That(loan, Is.Not.Null);
        }
    }
}
