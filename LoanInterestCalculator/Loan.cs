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

        public double TotalInterest {
            get { return totalInterest; }
            private set { totalInterest = value; }
        }
        public List<AmortizationItem> AmortizationList
        {
            get { return amortizationList; }
            set { amortizationList = value; }
        }

        public List<AdditionalPayment> AddPayments
        {
            get { return addPayments; }
            set { addPayments = value; }
        }

        public double MoneySaved { get; set; }

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

        /// <summary>
        ///Essentially checks inputs to see if there is any additional payments. 
        ///If so, it adds them to the List<AdditionalPayment> addPayments.
        /// </summary>
        public void AnyAdditionalPayments(out DateTime oneTimePayDate, out double oneTimePayAmount)
        {
            //Check inputs for none null/default values. 
            //That is what would happen to populate the values.
            AddPayments.Add(new OneTimePayment(DateTime.Now.AddMonths(12), 723.27));

            oneTimePayDate = DateTime.MinValue;
            oneTimePayAmount = 0;

            if (AddPayments.OfType<OneTimePayment>().Any())
            {
                oneTimePayDate = Convert.ToDateTime(AddPayments.OfType<OneTimePayment>().
                                            First().DatePayment.ToShortDateString());
                oneTimePayAmount = AddPayments.OfType<OneTimePayment>().
                                        First().AmountPayment;
            }
            //Code Ready for monthly payments.
            else if (AddPayments.OfType<MonthlyPayments>().Any())
            {

            }
        }

        public void calculateAmmoritazation()
        {
            double interest;
            double totalAmount;
            double payment;
            DateTime paymentDate;
            DateTime oneTimePayDate;
            double oneTimePayAmount;
            double principlePaid = 0;
            amortizationList = new List<AmortizationItem>();

            totalAmount = calcTotalRepayment();
            payment = calcMonthlyPayment();

            AnyAdditionalPayments(out oneTimePayDate, out oneTimePayAmount);

            do
            {
                paymentDate = StartDate.AddMonths(amortizationList.Count).Date;  
                interest = calcPaymentOfInterest();
                totalInterest += Math.Round(interest, 3);
                if(payment >= PrincipleAmount)
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

            MoneySaved = totalAmount - InitialPrinciple - totalInterest;
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
