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
                if (loan.BiWeeklyPayment != 0)
                {
                    int totalYears = (loan.AmortizationList.Count / 26);

                    int remainingMonths = Convert.ToInt32(
                    //double x = 
                    Math.Round(((loan.AmortizationList.Count / 26f) - totalYears) * 12));

                    endDate = endDate.AddYears(totalYears);
                    endDate = endDate.AddMonths(remainingMonths);
                    double actualRepaid = Math.Round(loan.InitialPrinciple + loan.TotalInterest);
                    details.Add(new LoanDetails(loan.BiWeeklyPayment, loan.TotalRepayment,
                        loan.MoneySaved, loan.TotalInterest, endDate, actualRepaid));
                }
                else
                {
                    endDate = endDate.AddMonths(loan.AmortizationList.Count);
                    details.Add(new LoanDetails(loan.MonthlyPayment, loan.TotalRepayment,
                        loan.MoneySaved, loan.TotalInterest, endDate));
                }
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
                if (detail.MoneySaved < .5)
                {
                    Console.WriteLine("Payoff Date: {0} -- Monthly Payment: {1} -- Standard Repayment: {2} " +
                                "-- Total Interest {3}",
                    detail.PayoffDate.ToString("MMMM, yyyy"), detail.MonthlyPayment, detail.ExpectedRepayment, detail.TotalInterest);
                }
                else
                    Console.WriteLine("\nPayoff Date: {0} -- Bi-Weekly Payment: {1} -- Standard Repayment: {2} \n" +
                                    "\tActual Repayment: {5} -- Total Interest Paid: {3} -- Interest Saved: {4}",
                        detail.PayoffDate.ToString("MMMM, yyyy"), detail.MonthlyPayment, detail.ExpectedRepayment, detail.TotalInterest, detail.MoneySaved, detail.ActualRepayment);
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
