"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Loan = /** @class */ (function () {
    function Loan(principle, rate, length, interestSaved, totalRepayment, startDate) {
        if (interestSaved === void 0) { interestSaved = 0; }
        if (totalRepayment === void 0) { totalRepayment = 0; }
        if (startDate === void 0) { startDate = new Date(Date.now()); }
    }
    return Loan;
}());
exports.Loan = Loan;
//# sourceMappingURL=loan.js.map