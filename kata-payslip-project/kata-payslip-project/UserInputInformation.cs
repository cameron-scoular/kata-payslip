using System;

namespace kata_payslip_project
{
    public class UserInputInformation
    {
    
        public string Name { get; set; }

        public string Surname { get; set; }

        public uint Salary { get; set; }

        public uint SuperRate { get; set; }

        public DateTime PaymentStartDate { get; set; }

        public DateTime PaymentEndDate { get; set; }
    }
}