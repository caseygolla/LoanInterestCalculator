using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanInterestCalculator
{
    public class MonthlyPayments : AdditionalPayment
    {
        public MonthlyPayments(double amount)
        {
            Amount = amount;
        }

        public double Amount { get; set; }

    }
}
