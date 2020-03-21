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

        public AmortizationItem (DateTime date, double principle, double interest, double totalInterest, double balance)
        {
            paymentDate = date.Date;
            principlePaid = principle;
            interestPaid = interest;
            this.totalInterest = totalInterest;
            remainingBalance = balance;
        }
        public double PrinciplePaid { get; set; }

        public double InterestPaid { get; set; }

        public double TotalInterest { get; set; }

        public double RemainingBalance { get; set; }

        public void amortitizeThis(int paymentCycle)
        {
            Console.WriteLine("Payment " + paymentDate.ToString("MMMM, yyyy") );
            Console.WriteLine("\tPrinciple Paid: " + LoanHelper.FormatNumberToCurrency(principlePaid) + 
                                " | Interest Paid: " + LoanHelper.FormatNumberToCurrency(interestPaid) +
                                " | Total Interest: " + LoanHelper.FormatNumberToCurrency(totalInterest) +
                                " | Amount Remaining: " + LoanHelper.FormatNumberToCurrency(remainingBalance));

        }
    }
}
