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
        private DateTime paymentDate;
        private double payment;

        public AmortizationItem(DateTime date, double principle, double interest, double totalInterest, double balance)
        {
            paymentDate = date.Date;
            PrinciplePaid = principle;
            InterestPaid = interest;
            this.TotalInterest = totalInterest;
            RemainingBalance = balance;
            Payment = principle + interest;
        }
        public double PrinciplePaid
        {
            get { return Math.Round(principlePaid, 2); }
            set { principlePaid = value; }
        }

        public double InterestPaid
        {
            get { return Math.Round(interestPaid, 2); }
            set { interestPaid = value; }
        }

        public double TotalInterest
        {
            get { return Math.Round(totalInterest, 2); }
            set { totalInterest = value; }
        }

        public double RemainingBalance
        {
            get { return Math.Round(remainingBalance, 2); }
            set { remainingBalance = value; }
        }

        public double Payment { get; set; }

        public void AmmortitizeThis(int paymentCycle)
        {
            Console.WriteLine("Payment " + paymentDate.ToString("MMMM, yyyy"));
            Console.WriteLine("\tTotal Paid: " + LoanHelper.FormatNumberToCurrency(payment) +
                                " | Principle Paid: " + LoanHelper.FormatNumberToCurrency(principlePaid) +
                                " | Interest Paid: " + LoanHelper.FormatNumberToCurrency(interestPaid) +
                                " | Total Interest: " + LoanHelper.FormatNumberToCurrency(totalInterest) +
                                " | Amount Remaining: " + LoanHelper.FormatNumberToCurrency(remainingBalance));

        }
    }
}
