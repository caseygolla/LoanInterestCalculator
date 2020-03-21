using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanInterestCalculator
{
    public class Loan
    {
        private int periodicPayments;
        private List<AmortizationItem> amortizationList;
        private double totalInterest = 0;
        private const int compoundFrequency = 365;
        private const int one = 1;
        DateTime startDate;

        public Loan()
        {
            PrincipleAmount = 1000;
            InterestRate = .06;
            LengthOfRepaymentInYears = .5;
            periodicPayments = 12;
        }

        public Loan(double principle, double rate, double length = 1, int periodicPayments = 12)
        {
            PrincipleAmount = principle;
            InterestRate = rate;
            LengthOfRepaymentInYears = length;
            this.periodicPayments = periodicPayments;
        }

        public double PrincipleAmount { get; set; }
        public double InterestRate { get; set; }
        public double LengthOfRepaymentInYears { get; set; }
        public DateTime StartDate
        {
            get
            {
                if (startDate == DateTime.MinValue || startDate == null)
                    startDate = DateTime.Today.Date;
                return startDate;
            }
            set { startDate = value; }
        }
        public double TotalInterest {
            get { return totalInterest; }
            private set { totalInterest = value; }
        }
        public List<AmortizationItem> AmortizationList
        {
            get { return amortizationList; }
            set { amortizationList = value; }
        }

        public double calcPaymentOfInterest()
        {
            double interestPayment = PrincipleAmount * InterestRate / periodicPayments;
            return Math.Round(interestPayment,2);
        }

        public double calcTotalRepayment()
        {
            double total = calcMonthlyPayment() * periodicPayments * LengthOfRepaymentInYears;
            return Math.Round(total, 2);
        }

        public double calcMonthlyPayment()
        {
            double i = InterestRate / periodicPayments;
            double n = LengthOfRepaymentInYears * periodicPayments;
            double discountFactor = (Math.Pow((one + i), n) - one) / 
                                    (i * Math.Pow(one + i, n));
            double payment = PrincipleAmount / discountFactor;
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
            double principlePaid = 0;
            double amount = calcTotalRepayment();
            double payment = calcMonthlyPayment();
            DateTime paymentDate;

            do
            {
                paymentDate = StartDate.AddMonths(amortizationList.Count).Date;
                interest = calcPaymentOfInterest();
                totalInterest += Math.Round(interest, 3);
                if(payment >= PrincipleAmount)
                {
                    PrincipleAmount -= interest;
                    principlePaid = PrincipleAmount;
                    PrincipleAmount = 0;
                    amortizationList.Add(new AmortizationItem(paymentDate, principlePaid, interest, totalInterest, PrincipleAmount));
                    break;
                }
                principlePaid = (payment - interest);
                PrincipleAmount -= principlePaid;
                if (PrincipleAmount < 0)
                    PrincipleAmount = 0;
                if (PrincipleAmount < payment)
                    payment = PrincipleAmount;
                amortizationList.Add(new AmortizationItem(paymentDate, principlePaid, interest, totalInterest, PrincipleAmount));
            }
            while (PrincipleAmount > 0);
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
