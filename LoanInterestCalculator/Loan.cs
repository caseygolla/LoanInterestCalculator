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
        List<AdditionalPayment> addPayments = new List<AdditionalPayment>();

        public Loan()
        {
            InitialPrinciple = 1000;
            PrincipleAmount = InitialPrinciple;
            InterestRate = .06;
            LengthOfRepaymentInYears = .5;
            periodicPayments = 12;
        }

        public Loan(double principle, double rate, double length = 1, int periodicPayments = 12)
        {
            InitialPrinciple = principle;
            PrincipleAmount = InitialPrinciple;
            InterestRate = rate;
            LengthOfRepaymentInYears = length;
            this.periodicPayments = periodicPayments;
        }
        public double InitialPrinciple { get; set; }
        public double PrincipleAmount { get; set; }
        public double InterestRate { get; set; }
        public double LengthOfRepaymentInYears { get; set; }
        public double InterestSaved { get; set; }
        public double MonthlyPayment { get; set; }
        public double TotalRepayment { get; set; }

        public DateTime StartDate
        {
            get
            {
                if (startDate == DateTime.MinValue || startDate == null)
                    startDate = DateTime.Today.Date.AddMonths(1);
                return startDate;
            }
            set { startDate = value; }
        }

        public double TotalInterest
        {
            get { return totalInterest; }
            private set { totalInterest = value; }
        }
        public List<AmortizationItem> AmortizationList
        {
            get { return amortizationList; }
            set { amortizationList = value; }
        }

        public List<AdditionalPayment> AdditionalPayments
        {
            get { return addPayments; }
            set { addPayments = value; }
        }

        public double MoneySaved { get; set; }

        public double CalcPaymentOfInterest()
        {
            double interestPayment = PrincipleAmount * InterestRate / periodicPayments;
            return Math.Round(interestPayment, 2);
        }

        public void CalcTotalRepayment()
        {
            double total = MonthlyPayment * periodicPayments * LengthOfRepaymentInYears;
            TotalRepayment = Math.Round(total, 2);
        }

        public void CalcMonthlyPayment()
        {
            double i = InterestRate / periodicPayments;
            double n = LengthOfRepaymentInYears * periodicPayments;
            double discountFactor = (Math.Pow((one + i), n) - one) /
                                    (i * Math.Pow(one + i, n));
            double payment = PrincipleAmount / discountFactor;

            MonthlyPayment = Math.Round(payment, 2);
        }

        public void CalcBasicLoan()
        {
            CalcMonthlyPayment();
            CalcTotalRepayment();
        }

        public string DisplayMonthlyPayment()
        {
            return LoanHelper.FormatNumberToCurrency(MonthlyPayment);
        }

        public string DisplayTotalRepayment()
        {
            return LoanHelper.FormatNumberToCurrency(TotalRepayment);
        }

        /// <summary>
        ///Essentially checks inputs to see if there is any additional payments. 
        ///If so, it adds them to the List<AdditionalPayment> AdditionalPayments.
        /// </summary>
        public void AnyAdditionalPayments(out DateTime oneTimePayDate, out double oneTimePayAmount, ref double payment)
        {
            //Check inputs for none null/default values. 
            //That is what would happen to populate the values.

            oneTimePayDate = DateTime.MinValue;
            oneTimePayAmount = 0;

            if (AdditionalPayments.OfType<OneTimePayment>().Any())
            {
                oneTimePayDate = Convert.ToDateTime(AdditionalPayments.OfType<OneTimePayment>().
                                            First().DatePayment.ToShortDateString());
                oneTimePayAmount = AdditionalPayments.OfType<OneTimePayment>().
                                        First().AmountPayment;
            }
            if (AdditionalPayments.OfType<MonthlyPayments>().Any())
            {
                payment += AdditionalPayments.OfType<MonthlyPayments>().First<MonthlyPayments>().Amount;
            }
        }

        public void CalculateAmmoritazation()
        {
            double interest;
            DateTime oneTimePayDate;
            double oneTimePayAmount;
            DateTime paymentDate;
            amortizationList = new List<AmortizationItem>();
            double principlePaid = 0;
            double totalAmount;
            double payment;

            CalcBasicLoan();

            totalAmount = TotalRepayment;
            payment = MonthlyPayment;

            AnyAdditionalPayments(out oneTimePayDate, out oneTimePayAmount, ref payment);

            do
            {
                paymentDate = StartDate.AddMonths(amortizationList.Count).Date;
                interest = CalcPaymentOfInterest();
                totalInterest += Math.Round(interest, 3);
                if (payment >= PrincipleAmount)
                {
                    principlePaid = PrincipleAmount;
                    PrincipleAmount = 0;
                    amortizationList.Add(new AmortizationItem(paymentDate, principlePaid, interest, totalInterest, PrincipleAmount));
                    break;
                }
                principlePaid = (payment - interest);
                if (paymentDate.ToString("MMMM, yyyy") == oneTimePayDate.ToString("MMMM, yyyy"))
                {
                    principlePaid += oneTimePayAmount;
                    if (principlePaid >= PrincipleAmount)
                        principlePaid = PrincipleAmount;
                }
                PrincipleAmount -= principlePaid;
                if (PrincipleAmount < 0)
                    PrincipleAmount = 0;
                if (PrincipleAmount < payment)
                    payment = PrincipleAmount;
                amortizationList.Add(new AmortizationItem(paymentDate, principlePaid, interest, totalInterest, PrincipleAmount));
            }
            while (PrincipleAmount > 0);

            double savedAmount = totalAmount - InitialPrinciple - totalInterest;
            MoneySaved = Math.Round(savedAmount, 2);
        }

        public void PrintAmortization()
        {
            foreach (AmortizationItem item in amortizationList)
            {
                item.AmmortitizeThis(amortizationList.IndexOf(item));
            }
        }
    }
}
