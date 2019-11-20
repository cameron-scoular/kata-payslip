using System;
using System.Collections.Generic;

namespace kata_payslip_project
{
    public class PayslipGenerator
    {

        List<TaxRateBracket> TaxRateBracketList = new List<TaxRateBracket>()
        {
            new TaxRateBracket(0, 18200, 0, 0),
            new TaxRateBracket(18201, 37000, 0.19, 0),
            new TaxRateBracket(37001, 87000, 0.325, 3572),
            new TaxRateBracket(87001, 180000, 0.37, 19822),
            new TaxRateBracket(180001, int.MaxValue, 0.45, 54232)
        };
        
        public PayslipInformation GeneratePayslip(UserInputInformation userInputInformation)
        {
            
            PayslipInformation payslipInformation = new PayslipInformation();
            
            payslipInformation.Fullname = userInputInformation.Name + " " + userInputInformation.Surname;
            
            payslipInformation.GrossIncome = Convert.ToUInt32(Math.Round(userInputInformation.Salary / 12.0)); // Gross income is monthly, so divide by 12

            payslipInformation.IncomeTax = CalculateIncomeTax(payslipInformation.GrossIncome * 12); // Income tax calculated on annual pay, then made monthly within method

            payslipInformation.NetIncome = payslipInformation.GrossIncome - payslipInformation.IncomeTax;

            payslipInformation.Super = checked((uint)Math.Round(payslipInformation.GrossIncome * (userInputInformation.SuperRate / 100.0)));

            payslipInformation.PaymentStartDate = userInputInformation.PaymentStartDate;

            payslipInformation.PaymentEndDate = userInputInformation.PaymentEndDate;

            return payslipInformation;

        }

        private uint CalculateIncomeTax(uint grossIncome)
        {
            foreach (var bracket in TaxRateBracketList)
            {
                if(bracket.IsInBracket(grossIncome))
                {
                    var residual = checked((uint) Math.Round((grossIncome - bracket.LowerBound) * bracket.Rate));
                    return checked((uint) Math.Round((bracket.FixedAmount + residual) / 12.0));
                }
            }

            throw new Exception();
        }
        
        
        
    }

    class TaxRateBracket
    {
        
        public readonly uint LowerBound; 
        public readonly uint UpperBound;
        public readonly double Rate;
        public readonly uint FixedAmount;

        public TaxRateBracket(uint lowerBound, uint upperBound, double rate, uint fixedAmount)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
            Rate = rate;
            FixedAmount = fixedAmount;
        }

        public bool IsInBracket(uint amount)
        {
            if (amount >= LowerBound && amount <= UpperBound)
            {
                return true;
            }

            return false;
        }
    }
    
}