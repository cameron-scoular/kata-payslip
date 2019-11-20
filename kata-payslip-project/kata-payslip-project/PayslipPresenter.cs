using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace kata_payslip_project
{
    public class PayslipPresenter
    {
        
        private Dictionary<string, int> _monthDictionary;

        public PayslipPresenter(Dictionary<string, int> monthDictionary)
        {
            _monthDictionary = monthDictionary;
        }
        
        public void PresentPayslipConsole(PayslipInformation payslipInformation)
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

        public void PresentPayslipCsv(List<PayslipInformation> payslipInformationList, string outputFilepath)
        {
            var csv = new StringBuilder();

            csv.AppendLine("name, pay period, gross income, income tax, net income, super");

            foreach (var payslip in payslipInformationList)
            {
                var startMonth =  _monthDictionary.FirstOrDefault(x => x.Value == payslip.PaymentStartDate.Month).Key;
                var endMonth =  _monthDictionary.FirstOrDefault(x => x.Value == payslip.PaymentEndDate.Month).Key;

                var payPeriod = string.Format("{0} {1} - {2} {3}", payslip.PaymentStartDate.Day, startMonth, payslip.PaymentEndDate.Day, endMonth);
                
                
                csv.AppendLine(string.Format("{0}, {1}, {2}, {3}, {4}, {5}", payslip.Fullname, payPeriod, payslip.GrossIncome,
                    payslip.IncomeTax, payslip.NetIncome, payslip.Super));
            }
            
            File.WriteAllText(outputFilepath, csv.ToString());
        }
    }
}