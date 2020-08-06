using System;

namespace LoanInterestCalculator
{
    public class LoanDetails
    {
        public double MonthlyPayment { get; set; }
        public double ExpectedRepayment { get; set; }
        public double MoneySaved { get; set; }
        public double TotalInterest { get; set; }
        public double ActualRepayment { get; set; }
        
        //To be used at a later time when months/years are used
        //as the length of loan.
        public DateTime PayoffDate { get; set; }

        public LoanDetails(double monthly, double totalRepayment,
            double moneySaved, double totalInterest, DateTime endDate, double actualRepayment = 0)
        {
            MonthlyPayment = monthly;
            ExpectedRepayment = totalRepayment;
            MoneySaved = moneySaved;
            TotalInterest = totalInterest;
            PayoffDate = endDate;
            ActualRepayment = actualRepayment;
        }
    }
}