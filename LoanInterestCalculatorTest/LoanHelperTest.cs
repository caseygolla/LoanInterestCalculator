using System;
using NUnit.Framework;
using LoanInterestCalculator;
using System.Collections;

namespace LoanInterestCalculatorTest
{
    [TestFixture]
    public class LoanHelperTest
    {
        [TestCase(12312, "12,312.00")]
        [TestCase(98, "98.00")]
        [TestCase(3766192856, "3,766,192,856.00")]
        [Category("Format_CurrencyCommas")]
        public void TestAddCurrencyCommas_LargeNumbers(double input, string expected)
        {

            string actual = LoanHelper.AddCurrencyCommas(input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(12312,"12312.00")]
        [TestCase(98,"98.00")]
        [TestCase(3766192856, "3766192856.00")]
        [Category("Format_2DecimalPlaces")]
        public void TestFormattingAllNumbersToHaveTwoDecimalPlaces(double input, string expected)
        {
            string actual = LoanHelper.FormatToTwoDecimalPlaces(input);

            Assert.That(actual, Is.EqualTo( expected ));
        }
    }

    public class ExampleTestCaseSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
