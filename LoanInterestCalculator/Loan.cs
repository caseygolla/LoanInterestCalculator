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
        private double principleAmount;
        private double interestRate;
        private double lengthOfRepaymentInYears;
        private IOutput output;
        private DateTime loanStart;
        private List<AmortizationItem> ammortizationList;
        private double totalInterest = 0;
        private const int compoundFrequency = 365;
        private const int one = 1;

        public Loan()
        {
            principleAmount = 5000;
            interestRate = .06;
            lengthOfRepaymentInYears = 5;
            periodicPayments = 12;
            ammortizationList = new List<AmortizationItem>();
            loanStart = DateTime.Today;
        }

        public Loan(double principle, double rate, DateTime loanStart, double length = 1, int periodicPayments = 12)
        {
            principleAmount = principle;
            interestRate = rate;
            lengthOfRepaymentInYears = length;
            this.periodicPayments = periodicPayments;
            CheckLoanStartDate(loanStart);
        }

        private void CheckLoanStartDate(DateTime loanStart)
        {
            if (DateTime.Today > loanStart)
            {
                this.loanStart = DateTime.Today;
            }
            else { this.loanStart = loanStart; }
        }

        public double PrincipleAmount { get { return principleAmount; } set { principleAmount = value; } }
        public double InterestRate { get { return interestRate; } set { interestRate = value; } }
        public double LengthOfRepaymentInYears { get { return lengthOfRepaymentInYears; } set { lengthOfRepaymentInYears = value; } }

        public double TotalInterest
        {
            get { return totalInterest; }
            private set { totalInterest = value; }
        }
        public List<AmortizationItem> AmortizationList
        {
            get { return ammortizationList; }
            set { ammortizationList = value; }
        }

        public double calcPaymentOfInterest()
        {
            double interestPayment = principleAmount * interestRate / periodicPayments;
            return Math.Round(interestPayment, 2);
        }

        public double calcTotalRepayment()
        {
            double total = calcMonthlyPayment() * periodicPayments * lengthOfRepaymentInYears;
            return Math.Round(total, 2);
        }

        public double calcMonthlyPayment()
        {
            double i = interestRate / periodicPayments;
            double n = lengthOfRepaymentInYears * periodicPayments;
            double discountFactor = (Math.Pow((one + i), n) - one) / (i * Math.Pow(one + i, n));
            double payment = principleAmount / discountFactor;
            return Math.Round(payment, 2);
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
            int index = 1;
            double interest;
            ammortizationList = new List<AmortizationItem>();
            double principlePaid = 0;
            double amount = calcTotalRepayment();
            double payment = calcMonthlyPayment();
            DateOfPayment paymentDate;

            do
            {
                interest = calcPaymentOfInterest();
                totalInterest += Math.Round(interest, 2);
                paymentDate = new DateOfPayment(loanStart, index);
                if (payment >= principleAmount)
                {
                    principleAmount -= interest;
                    principlePaid = principleAmount;
                    principleAmount = 0;
                    ammortizationList.Add(new AmortizationItem(principlePaid, interest, totalInterest, principleAmount, index, paymentDate));
                    index++;
                    break;
                }
                principlePaid = (payment - interest);
                principleAmount -= principlePaid;
                if (principleAmount < 0)
                    principleAmount = 0;
                if (principleAmount < payment)
                    payment = principleAmount;
                ammortizationList.Add(new AmortizationItem(principlePaid, interest, totalInterest, principleAmount, index, paymentDate));
                index++;
            }
            while (principleAmount > 0);
        }

        public void PrintAmmortizationToConsole()
        {
            output = new PrintLoanOutput();
            output.PrintAmmortization(ammortizationList);
        }

        public void SaveAmmortizationToFile()
        {
            output = new FileLoanOutput();
            output.PrintAmmortization(ammortizationList);
        }

        public void AdditionalPayment()
        {
            throw new NotImplementedException(); 
            //IPayment payment;
            //IPayment additionalPayment;
            //if (oneTimePymtChk.isTrue)
            //{
            //    payment = new OneTimePymt();
            //}
            //else if (payment == null && additionalPymtChk.IsTrue)
            //{
            //    payment = new MonthlyPymt();
            //}
            //else if(payment != null && additionalPayment.IsTrue)
            //{
            //    additionalPayment = new MonthlyPymt();
            //}

        }
    }
}
