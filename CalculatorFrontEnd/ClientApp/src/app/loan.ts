export class Loan {
  constructor(principle: number,
    rate: number,
    length: number,
    interestSaved: number = 0,
    totalRepayment: number = 0,
    startDate: Date = new Date(Date.now())) { }
}
