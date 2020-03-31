import { Component, OnInit } from '@angular/core';
import { Loan } from '../loan';

@Component({
  selector: 'app-loans-component',
  templateUrl: './loans.component.html',
  styleUrls: ['./loans.component.css']
})
export class LoansComponent implements OnInit {

  loans: Loan[] = [];

  loan: Loan = {
    principle: 1000,
    rate: .05,
    length: 3
  }
  //constructor(principle: number, rate: number, length: number, periodicPayments: number = 12) {

  //}

  public Calculate() {

    do {

    } while (PrincipleAmount > 0);
  }

  public AddLoan() {
    this.loans.push(new Loan());
  }

  ngOnInit() {
  }

}
