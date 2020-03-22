using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanInterestCalculator
{
    public class OneTimePayment : AdditionalPayment
    {
        public OneTimePayment()
        {
            DatePayment = DateTime.Today;
            AmountPayment = 100;
        }

        public OneTimePayment(DateTime date, double amount)
        {
            DatePayment = date;
            AmountPayment = amount;
        }

        public double AmountPayment { get; set; }
        public DateTime DatePayment { get; set; }
    }
}
