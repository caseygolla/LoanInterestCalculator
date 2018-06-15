using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanInterestCalculator
{
    public class AmortizationItem
    {
        private double principlePaid;
        private double interestPaid;
        private double totalInterest;
        private double remainingBalance;
        private int ammortIndex;
        private string paymentDate;


        public AmortizationItem(double principle, double interest, double totalInterest, double balance, int index, DateOfPayment payDate)
        {
            principlePaid = principle;
            interestPaid = interest;
            this.totalInterest = totalInterest;
            remainingBalance = balance;
            ammortIndex = index;
            paymentDate = payDate.PaymentDate.ToShortDateString();
        }
        public double PrinciplePaid
        {
            get { return principlePaid; }
            set { principlePaid = value; }
        }

        public double InterestPaid
        {
            get { return interestPaid; }
            set { interestPaid = value; }
        }

        public double TotalInterest
        {
            get { return totalInterest; }
            set { totalInterest = value; }
        }

        public double RemainingBalance
        {
            get { return remainingBalance; }
            set { remainingBalance = value; }
        }

        public int AmmortIndex
        {
            get { return ammortIndex; }
            set { ammortIndex = value; }
        }

        public string PaymentDate()
        {
            return paymentDate;
        }

        public string PrinciplePaidString()
        {
            return LoanHelper.FormatNumberToCurrency(PrinciplePaid);
        }

        public string InterestPaidString()
        {
            return LoanHelper.FormatNumberToCurrency(InterestPaid);
        }

        public string TotalInterestString()
        {
            return LoanHelper.FormatNumberToCurrency(TotalInterest);
        }

        public string RemainingBalanceString()
        {
            return LoanHelper.FormatNumberToCurrency(RemainingBalance);
        }
    }

    public class DateOfPayment
    {
        DateTime dateOfPayment;

        public DateOfPayment(DateTime loanStart, int paymentIndex)
        {
            dateOfPayment = DeterminePaymentDate(loanStart, paymentIndex);
        }

        private DateTime DeterminePaymentDate(DateTime loanStart, int paymentIndex)
        {
            return loanStart.AddMonths(paymentIndex);
        }

        public DateTime PaymentDate
        {
            get { return dateOfPayment; }
            set { dateOfPayment = value; }
        }
    }
}
