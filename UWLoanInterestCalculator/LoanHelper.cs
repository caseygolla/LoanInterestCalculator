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

        public static string AddCurrencyCommas(double value)
        {
            string valueStr = FormatToTwoDecimalPlaces(value);
            int indexOfDecimal = valueStr.IndexOf('.');
            if (indexOfDecimal > 3)
            {
                for (int position = 1; position < indexOfDecimal; position++)
                {
                    if(position % 3 == 0)
                    {
                        valueStr = valueStr.Insert((indexOfDecimal - position), ",");
                    }
                }
            }
            return valueStr;
        }

        public static string FormatToTwoDecimalPlaces(double value)
        {
            return value.ToString("0.00");
        }
    }
}
