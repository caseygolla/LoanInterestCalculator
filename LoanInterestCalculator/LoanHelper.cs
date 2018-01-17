using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanInterestCalculator
{
    public class LoanHelper
    {

        public static string FormatNumberToCurrency(double value)
        {
            value = Math.Round(value, 2);

            string valueStr = AddCurrencyCommas(value);
            valueStr = valueStr.Insert(0, "$");

            return valueStr;
        }

        private static string AddCurrencyCommas(double value)
        {
            string valueStr = value.ToString();



            return valueStr;
        }

    }
}
