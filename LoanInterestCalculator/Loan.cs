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
        private const int ONE = 1;
        private const int TWNETYSIX = 26;
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
        public double BiWeeklyPayment { get; set; }
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
            double total = MonthlyPayment * 12 * LengthOfRepaymentInYears;
            TotalRepayment = Math.Round(total, 2);
        }

        public void CalcMonthlyPayment()
        {
            MonthlyPayment = CalcPayment(12);
        }

        public void CalcBiWeeklyPayment()
        {
            double payment = CalcPayment(TWNETYSIX);

            payment *= (ONE + (ONE / 12f));

            BiWeeklyPayment = Math.Round(payment, 2);
        }

        public double CalcPayment(int numberOfPayments)
        {
            double i = InterestRate / numberOfPayments;
            double n = LengthOfRepaymentInYears * numberOfPayments;
            double z = Math.Pow((ONE + i), n);
            double discountFactor = (z - ONE) / (i * z);
            double payment = PrincipleAmount / discountFactor;

            return Math.Round(payment, 2);
        }

        public void CalcBasicLoan()
        {
            CalcMonthlyPayment();
            CalcTotalRepayment();
            if (periodicPayments == TWNETYSIX)
            {
                CalcBiWeeklyPayment();
            }
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
            bool oneTimePayFlag = false;

            CalcBasicLoan();

            totalAmount = TotalRepayment;

            if (periodicPayments == TWNETYSIX)
                payment = BiWeeklyPayment;
            else
                payment = MonthlyPayment;


            AnyAdditionalPayments(out oneTimePayDate, out oneTimePayAmount, ref payment);

            if (oneTimePayDate.ToString() != DateTime.MinValue.ToString())
            {
                oneTimePayFlag = true;
            }

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
                principlePaid = Math.Round(payment - interest, 2);
                if (oneTimePayFlag == true && paymentDate.ToString("MMMM, yyyy") == oneTimePayDate.ToString("MMMM, yyyy"))
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

            InterestSaved = totalAmount - InitialPrinciple - totalInterest;
            MoneySaved = Math.Round(InterestSaved, 2);
        }

        public void PrintAmortization()
        {
            if (periodicPayments == TWNETYSIX)
            {
                double principlePaid = 0;
                double yearlyInterest = 0;
                double remainingBalance = 0;
                int biweek;
                for (int i = 0; i < amortizationList.Count; i++)
                {
                    biweek = i + 1; 
                    principlePaid += amortizationList.ElementAt(i).PrinciplePaid;
                    yearlyInterest += amortizationList.ElementAt(i).InterestPaid;
                    remainingBalance = amortizationList.ElementAt(i).RemainingBalance;
                    if (biweek % 26 == 0 || remainingBalance == 0)
                    {
                        int year = (i + 1) / 26;
                        Console.WriteLine("Year " + year +
                                            " | PrinciplePaid " + principlePaid +
                                            " | Interest Paid " + yearlyInterest +
                                            " | Remaining Balance " + remainingBalance);
                        principlePaid = 0;
                        yearlyInterest = 0;
                    }
                }
            }
            else
                foreach (AmortizationItem item in amortizationList)
                {
                    item.AmmortitizeThis(amortizationList.IndexOf(item));
                }
        }
    }
}
