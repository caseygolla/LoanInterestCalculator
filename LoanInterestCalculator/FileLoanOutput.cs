using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LoanInterestCalculator
{
    public class FileLoanOutput : IOutput
    {
        private string payments = "";
        string directoryPath = @"C:\LoanProgram";
        string fileName = @"\LoanAmmortization.txt";

        public string DirectoryPath { get { return directoryPath; } }

        public string FileName { get { return fileName; } }

        public void PrintAmmortization(List<AmortizationItem> ammortList)
        {

            CreateDirectoryForOutputFile(directoryPath);

            foreach (AmortizationItem item in ammortList)
            {
                CreateFileOutputStringOfPayments(item);
            }

            File.WriteAllText(directoryPath + fileName, payments);
        }

        public void CreateFileOutputStringOfPayments(AmortizationItem item)
        {
            try
            {
                payments += String.Format("Payment {0} on {1} - Principle Paid: {2} | " +
                    "Interest: {3} | Total Interest: {4} | Amount Remaining: {5}" + Environment.NewLine,
                    item.AmmortIndex, item.PaymentDate(), item.PrinciplePaidString(),
                    item.InterestPaidString(), item.TotalInterestString(), item.RemainingBalanceString());
            }
            catch (NullReferenceException nre)
            {
                Console.WriteLine("Ammortization item null while attempting to output.");
            }
        }

        public void CreateDirectoryForOutputFile(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
