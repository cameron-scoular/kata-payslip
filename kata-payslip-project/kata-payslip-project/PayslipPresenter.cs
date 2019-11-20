using System;
using System.Collections.Generic;
using System.Linq;

namespace kata_payslip_project
{
    public class PayslipPresenter
    {
        
        private Dictionary<string, int> _monthDictionary;

        public PayslipPresenter(Dictionary<string, int> monthDictionary)
        {
            _monthDictionary = monthDictionary;
        }
        
        public void PresentPayslip(PayslipInformation payslipInformation)
        {
            Console.WriteLine("\r\nYour payslip has been generated\r\n");
            
            Console.WriteLine("Name: " + payslipInformation.Fullname);
            
            
            string startMonth =  _monthDictionary.FirstOrDefault(x => x.Value == payslipInformation.PaymentStartDate.Month).Key;
            string endMonth =  _monthDictionary.FirstOrDefault(x => x.Value == payslipInformation.PaymentEndDate.Month).Key;

            Console.WriteLine("Pay Period: {0} {1} - {2} {3}", payslipInformation.PaymentStartDate.Day, startMonth, payslipInformation.PaymentEndDate.Day, endMonth);

            Console.WriteLine("Gross Income: " + payslipInformation.GrossIncome);
            
            Console.WriteLine("Income Tax: " + payslipInformation.IncomeTax);
            
            Console.WriteLine("Net Income: " + payslipInformation.NetIncome);
            
            Console.WriteLine("Super: " + payslipInformation.Super + "\r\n");
        }
    }
}