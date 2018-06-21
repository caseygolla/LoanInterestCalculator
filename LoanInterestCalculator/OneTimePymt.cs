using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanInterestCalculator
{
    class OneTimePymt : IPayment
    {
        private double paymentAmount;
        private DateTime dateOfPayment;

        public OneTimePymt(double amount, DateTime date)
        {
            paymentAmount = amount;
            dateOfPayment = date;
        }

        public double PaymentAmount
        {
            get { return paymentAmount; }
            set { paymentAmount = value; }
        }

        public DateTime DateOfPayment
        {
            get { return dateOfPayment; }
            set { dateOfPayment = value; }
        }

        public double calculateNewLoanPayment(Loan loan)
        {
            throw new NotImplementedException();
        }
    }
}
