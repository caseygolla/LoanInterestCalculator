﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanInterestCalculator
{
    interface IPayment
    {
        double PaymentAmount { get; set; }

        double calculateNewLoanPayment(Loan loan);
    }
}