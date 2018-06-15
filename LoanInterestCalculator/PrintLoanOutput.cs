using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanInterestCalculator
{
    class PrintLoanOutput : IOutput
    {
        public void PrintAmmortization(List<AmortizationItem> ammortList)
        {
            foreach(AmortizationItem item in ammortList)
            {
                ConsoleOutput(item);
            }
        }

        private void ConsoleOutput(AmortizationItem item)
        {
            string singlePayment = "";
            try
            {
                singlePayment = String.Format("Payment {0} on {1} - Principle Paid: {2} | " +
                    "Interest: {3} | Total Interest: {4} | Amount Remaining: {5}",
                    item.AmmortIndex, item.PaymentDate(), item.PrinciplePaidString(),
                    item.InterestPaidString(), item.TotalInterestString(), item.RemainingBalanceString());
            }catch(NullReferenceException nre)
            {
                Console.WriteLine("Ammortization item null while attempting to output.");
            }

            Console.WriteLine(singlePayment);
        }
    }
}
