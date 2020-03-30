using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanInterestCalculator
{
    public class Loans : IEnumerable
    {
        List<Loan> loans;
        List<LoanDetails> details;
        MonthlyPayments monthlyPayment;
        OneTimePayment oneTimePayment;

        public Loans()
        {
            loans = new List<Loan>();
            details = new List<LoanDetails>();
        }

        public void CalculateLoans()
        {
            foreach (Loan loan in loans)
            {
                loan.CalculateAmmoritazation();
            }
        }

        public void Add(Loan loan)
        {
            loans.Add(loan);
        }

        public void AddMonthlyPayment(double amount)
        {
            monthlyPayment = new MonthlyPayments(amount);
            foreach (Loan loan in loans)
            {
                loan.AdditionalPayments.Add(monthlyPayment);
            }
        }

        public void AddOneTimePayment(DateTime date, double amount)
        {
            oneTimePayment = new OneTimePayment(date, amount);
            foreach (Loan loan in loans)
            {
                loan.AdditionalPayments.Add(oneTimePayment);
            }
        }

        public List<double> MoneySavedPerLoan()
        {
            List<double> loanMoneySaved = new List<double>();
            foreach (Loan loan in loans)
            {
                loanMoneySaved.Add(loan.MoneySaved);
            }

            return loanMoneySaved;
        }

        public void DetailsPerLoan()
        {
            foreach (Loan loan in loans)
            {
                DateTime endDate = loan.StartDate;
                endDate = endDate.AddMonths(loan.AmortizationList.Count);

                details.Add(new LoanDetails(loan.MonthlyPayment, loan.TotalRepayment,
                    loan.MoneySaved, loan.TotalInterest, endDate));
            }
        }

        public void ShowLoanDetails()
        {
            if (!details.Any())
            {
                DetailsPerLoan();
            }
            foreach (LoanDetails detail in details)
            {
                if(detail.MoneySaved < .5)
                {
                    Console.WriteLine("Payoff Date: {0} -- Monthly Payment: {1} -- Total Repayment: {2} " +
                                "-- Total Interest {3}",
                    detail.PayoffDate.ToString("MMMM, yyyy"), detail.MonthlyPayment, detail.TotalRepayment, detail.TotalInterest);
                }
                else
                Console.WriteLine("Payoff Date: {0} -- Monthly Payment: {1} -- Total Repayment: {2} " +
                                "-- Total Interest {3} -- Money Saved: {4}",
                    detail.PayoffDate.ToString("MMMM, yyyy"), detail.MonthlyPayment, detail.TotalRepayment, detail.TotalInterest, detail.MoneySaved);
            }
        }

        public void PrintLoanAmmortizations()
        {
            foreach (Loan loan in loans)
            {
                loan.PrintAmortization();
            }
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)loans).GetEnumerator();
        }
    }
}
