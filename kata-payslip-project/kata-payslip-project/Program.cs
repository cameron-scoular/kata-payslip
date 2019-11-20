using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace kata_payslip_project
{
    class Program
    {
        
        private static Dictionary<string, int> _monthDictionary = new Dictionary<string, int>()
        {
            { "January", 1},
            {"February", 2},
            {"March", 3},
            {"April", 4},
            {"May", 5},
            {"June", 6},
            {"July", 7},
            {"August", 8},
            {"September", 9},
            {"October", 10},
            {"November", 11},
            {"December", 12}
        };
        
        private static UserInformationParser _userInformationParser = new UserInformationParser(_monthDictionary);
        private static PayslipGenerator _payslipGenerator = new PayslipGenerator();
        private static PayslipPresenter _payslipPresenter = new PayslipPresenter(_monthDictionary);
        
        private static UserInputInformation _userInputInformation = new UserInputInformation();
        private static PayslipInformation _payslipInformation = new PayslipInformation();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the payslip generator!");

            _userInformationParser.PromptPayslipInformation(_userInputInformation);

            _payslipInformation = _payslipGenerator.GeneratePayslip(_userInputInformation);

            _payslipPresenter.PresentPayslip(_payslipInformation);

        }
        
    }
}


