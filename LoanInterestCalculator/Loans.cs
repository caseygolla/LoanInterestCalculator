using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanInterestCalculator
{
    public class Loans
    {
        List<Loan> loans;

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

        public void PrintLoanAmmortizations()
        {

            foreach (Loan loan in loans)
            {
                loan.PrintAmortization();
            }
            //TODO: This is essentially a place holder for when 
            //the output is needed.
        }
    }
}
