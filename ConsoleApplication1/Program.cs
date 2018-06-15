using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanInterestCalculator;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region enterValues

            //Console.WriteLine("Enter Loan Amount");
            //double principle = Convert.ToDouble( Console.ReadLine() );

            //Console.WriteLine("Enter Interest Rate");
            //string rate = Console.ReadLine();
            //double interestRate;

            //if (!rate.Contains("."))
            //    interestRate = Convert.ToDouble(rate) / 100;
            //else
            //    interestRate = Convert.ToDouble(rate);
            

            //Console.WriteLine("Enter Length of Loan in Years:");
            //double length = Convert.ToDouble(Console.ReadLine());

            //Loan a = new Loan(principle, interestRate, length);

            #endregion
            Loan loan1 = new Loan();

            Console.WriteLine("Monthly Payment: " + loan1.displayMonthlyPayment());
            Console.WriteLine("Total to be repaid: " + loan1.displayTotalRepayment());

            loan1.calculateAmmoritazation();
            loan1.PrintAmmortizationToConsole();
            loan1.SaveAmmortizationToFile();

        }
    }
}
