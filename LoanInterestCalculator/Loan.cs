using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanInterestCalculator
{
    public class Loan
    {
        private double principleAmount;
        private double interestRate;
        private double lengthOfRepayment;
        private const int compoundFrequency = 365;
        private const int one = 1;
        private const int periodicPayments = 12;
        private List<AmortizationItem> amortizationList;

        public Loan()
        {
            principleAmount = 1000;
            interestRate = .06;
            lengthOfRepayment = .5;
            amortizationList = new List<AmortizationItem>();
        }

        public Loan(double principle, double rate, double length)
        {
            principleAmount = principle;
            interestRate = rate;
            lengthOfRepayment = length;
        }

        public double PrincipleAmount { get { return principleAmount; } set { principleAmount = value; } }
        public double InterestRate { get { return interestRate; } set { interestRate = value; } }
        public double LengthOfRepaymentInYears { get { return lengthOfRepayment; } set { lengthOfRepayment = value; } }

        public List<AmortizationItem> AmortizationList
        {
            get { return amortizationList; }
            set { amortizationList = value; }
        }

        private double calcPaymentOfInterest()
        {
            double interestPayment = principleAmount * interestRate / periodicPayments;
            return Math.Round(interestPayment,2);
        }

        private double calcTotalRepayment()
        {
            double total = principleAmount * Math.Pow(1 + (interestRate / 365), (365 * lengthOfRepayment));

            return Math.Round(total, 2);
        }

        private double calcMonthlyPayment()
        {
            double i = interestRate / periodicPayments;
            double n = lengthOfRepayment * periodicPayments;
            double discountFactor = (Math.Pow((one + i), n) - one) / (i * Math.Pow(one + i, n));
            double payment = principleAmount / discountFactor;
            return Math.Round(payment,2);
        }

        public string displayMonthlyPayment()
        {
            return LoanHelper.FormatNumberToCurrency(calcMonthlyPayment());
        }

        public string displayTotalRepayment()
        {
            return LoanHelper.FormatNumberToCurrency(calcTotalRepayment());
        }

        public void calculateAmmoritazation()
        {
            double interest;
            amortizationList = new List<AmortizationItem>();
            double totalInterest = 0;
            double principlePaid = 0;
            double amount = calcTotalRepayment();
            double payment = calcMonthlyPayment();

            do
            {
                interest = calcPaymentOfInterest();
                totalInterest += Math.Round(interest, 7);
                if(payment >= principleAmount)
                {
                    principleAmount -= interest;
                    principlePaid = principleAmount;
                    principleAmount = 0;
                    amortizationList.Add(new AmortizationItem(principlePaid, interest, totalInterest, principleAmount));
                    break;
                }
                principlePaid = (payment - interest);
                principleAmount -= principlePaid;
                if (principleAmount < 0)
                    principleAmount = 0;
                if (principleAmount < payment)
                    payment = principleAmount;
                amortizationList.Add(new AmortizationItem(principlePaid, interest, totalInterest, principleAmount));
            }
            while (principleAmount > 0);
        }

        public void printAmortization()
        {
            foreach(AmortizationItem item in amortizationList)
            {
                item.amortitizeThis(amortizationList.IndexOf(item));
            }
        }
    }
}
