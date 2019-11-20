using System;

namespace kata_payslip_project
{
    public class PayslipInformation
    {
        public string Fullname { get; set; }
        
        public DateTime PaymentStartDate { get; set; }

        public DateTime PaymentEndDate { get; set; }

        public uint GrossIncome { get; set; }

        public uint IncomeTax { get; set; }

        public uint NetIncome { get; set; }
        
        public uint Super { get; set; }
    }
}