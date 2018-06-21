using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanInterestCalculator
{
    class MonthlyPymt : IPayment
    {
        public double PaymentAmount
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public double calculateNewLoanPayment(Loan loan)
        {
            throw new NotImplementedException();
        }
    }
}
