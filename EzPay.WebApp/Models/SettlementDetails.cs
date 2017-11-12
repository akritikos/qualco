using EzPay.Model;
using EzPay.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzPay.WebApp.Models
{
    public class SettlementDetails
    {
        public decimal DownpaymentAmount { get; set; }
        public decimal MonthlyAmount { get; set; }
        public Guid SettlementID { get; set; }

        public SettlementDetails(Settlement settlement)
        {
            SettlementType settlementType;
            decimal TotalAmount = 0;

            using (var ctx = new EzPaySqlServerContext())
            {
                settlementType = ctx.GetSet<SettlementType>().Where(c => c.Id == settlement.TypeId).FirstOrDefault();
                TotalAmount = ctx.GetSet<Bill>().Where(c => c.SettlementId == settlement.Id).Sum(b => b.Amount);
            }

            DownpaymentAmount = SettlementDownpaymentAmount(TotalAmount, settlementType);

            MonthlyAmount = SettlementMonthAmount(TotalAmount, settlementType, settlement.Installments);

            SettlementID = settlement.Id;
             
        }

        public decimal SettlementDownpaymentAmount(decimal TotalAmount, SettlementType settlementType)
        {
            DownpaymentAmount = (TotalAmount * settlementType.Downpayment) / (decimal)100;
            return (DownpaymentAmount);

        }

        public decimal SettlementMonthAmount(decimal TotalAmount,SettlementType settlementType, int N/*installments*/)
        {
            /* Amount =[P x R x(1 + R) ^ N] /[(1 + R) ^ N - 1]
               P = loan amount - downpayment
               R = interest rate per month
                 N = number of installments

               example:
                           initial amount = 500e
               downpayment = 40 % ->Type 4
               downpayment amount = 500 * 40 %= 200e
               the user selects from 3 to 36 installements
               Installments selected = N = 36
               P = 500 - 200 = 300e
               R = 3,2 / (12x100)
               =>
               Amount = 8,75e*/


            decimal P = TotalAmount - DownpaymentAmount;

            decimal R = settlementType.Interest / (decimal) (12 * 100);

            MonthlyAmount = (P * R * ((decimal)Math.Pow((double)(1 + R), (double)N))) / ((decimal)(Math.Pow((double)(1 + R),(double)N) - 1));

            return MonthlyAmount;
        }
    }
}
