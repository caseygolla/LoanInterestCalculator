using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LoanInterestCalculator;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LoanApplication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SingleLoan : Page
    {
        public SingleLoan()
        {
            this.InitializeComponent();
        }

        private void btnCalculateBasedOnInput_Click(object sender, RoutedEventArgs e)
        {
            int paymentFrequency = GetPaymentFrequency();
            CreateLoanInformation(paymentFrequency);
        }

        private int GetPaymentFrequency()
        {
            if ((bool)rdoPaymentFrequency_26.IsChecked)
            {
                return LoanHelper.twentySix;
            }
            return LoanHelper.twelve;
        }

        private void CreateLoanInformation(int paymentFrequency)
        {

            double principle = tbLoanAmount.Text.Contains("$") ? 
                                Convert.ToDouble(tbLoanAmount.Text.Remove(0, 1)) : Convert.ToDouble(tbLoanAmount.Text);
            double loanLength = Convert.ToDouble(tbLoanLengthInYears.Text);
            double loanInterestRate = Convert.ToDouble(tbInterestRate.Text) / 100;

            Loan loan = new Loan(principle, loanInterestRate, loanLength, paymentFrequency);

            string monthlyPayments = loan.displayMonthlyPayment();
            string totalPaid = loan.displayTotalRepayment();

            tbMonthlyPayments.Text = monthlyPayments;
            tbTotalPaid.Text = totalPaid;
            tbTotalInterest.Text = "Not Completed"; // LoanHelper.FormatNumberToCurrency(loan.TotalInterest);
        }

        private void UpdateLoanLengthField_YearToMonth(object sender, RoutedEventArgs e)
        {

            double timeInYears = Convert.ToDouble(tbLoanLengthInYears.Text);
            string yearsToMonths = Convert.ToString(timeInYears * 12);
            tbLoanLengthInMonths.Text = yearsToMonths;
        }

        private void UpdateLoanLengthField_MonthToYear(object sender, RoutedEventArgs e)
        {

            double timeInMonths = Convert.ToDouble(tbLoanLengthInMonths.Text);
            string monthsToYears = Convert.ToString(timeInMonths / 12);
            tbLoanLengthInYears.Text = monthsToYears;
        }

        //private async void button_Click(object sender, RoutedEventArgs e)
        //{
        //    MediaElement mediaElement = new MediaElement();
        //    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
        //    Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Hello, World!");
        //    mediaElement.SetSource(stream, stream.ContentType);
        //    mediaElement.Play();
        //}
    }
}
