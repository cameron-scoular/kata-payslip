using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;

namespace kata_payslip_project
{
    public class UserInformationParser
    {

        private Dictionary<string, int> _monthDictionary;
        
        public UserInformationParser(Dictionary<string, int> monthDictionary)
        {
            _monthDictionary = monthDictionary;
        }
        
        public void PromptPayslipInformation(UserInputInformation userInputInformation)
        {
            userInputInformation.Name = PromptString("Please input your name:", 30);

            userInputInformation.Surname = PromptString("Please input your surname:", 30);

            userInputInformation.Salary = PromptUnsignedInteger("Please enter your annual salary:");

            userInputInformation.SuperRate = PromptUnsignedInteger("Please enter your super rate:");

            bool validPaymentPeriod = false;
            while (!validPaymentPeriod)
            {
                userInputInformation.PaymentStartDate = PromptDateTime("Please enter your payment start date (e.g. '17 March'):");

                userInputInformation.PaymentEndDate = PromptDateTime("Please enter your payment end date:");

                TimeSpan span = userInputInformation.PaymentEndDate.Subtract(userInputInformation.PaymentStartDate);
                
                if (span.Days >= 27 || span.Days <= 31) // Ensuring time period is monthly
                {
                    validPaymentPeriod = true;
                }
                else
                {
                    Console.WriteLine("The given payment period is not monthly, please enter a monthly payment period");
                }
            }
            

        }

        public List<UserInputInformation> ParseCsvPayslipInformation(string filepath)
        {
            List<UserInputInformation> userInputList = new List<UserInputInformation>();
            
            using (TextFieldParser parser = new TextFieldParser(filepath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                parser.ReadFields(); // To skip first lines
                
                while (!parser.EndOfData)
                {
                    UserInputInformation userInputInformation = new UserInputInformation();

                    //Processing user input
                    string[] fields = parser.ReadFields();

                    userInputInformation.Name = fields[0];
                    userInputInformation.Surname = fields[1];
                    userInputInformation.Salary = uint.Parse(fields[2]);
                    userInputInformation.SuperRate = uint.Parse(fields[3].Replace("%", ""));

                    var dateInput = fields[4].Split("-");
                    
                    var dateInputs = dateInput[0].Split(" ");
                    dateInputs.ToList().Remove("-");
                    var dayString = dateInputs[0];
                    var monthString = dateInputs[1];
                    var monthInteger = _monthDictionary[FirstCharToUpperCase(monthString)];
                    var dayInteger = uint.Parse(dayString);
                    userInputInformation.PaymentStartDate = new DateTime(1, monthInteger, checked((int) dayInteger));

                    dayString = dateInputs[3];
                    monthString = dateInputs[4];
                    monthInteger = _monthDictionary[FirstCharToUpperCase(monthString)];
                    dayInteger = uint.Parse(dayString);
                    userInputInformation.PaymentEndDate = new DateTime(1, monthInteger, checked((int) dayInteger));

                    userInputList.Add(userInputInformation);

                }
            }

            return userInputList;
        }
        
        private uint PromptUnsignedInteger(string message){
            Console.WriteLine(message);

            string userInput = Console.ReadLine();
            uint parsedInput;
            
            // Ensuring input is an unsigned integer
            while (!uint.TryParse(userInput, out parsedInput))
            {
                Console.WriteLine("The value you entered is not valid, please enter an unsigned integer:");
                userInput = Console.ReadLine();
            }

            return parsedInput;
        }
        
        private string PromptString(string message, int maxLength){
            Console.WriteLine(message);
            string userInput = Console.ReadLine();

            // Ensuring input is nonempty, less than max length, and uses alphabet characters only before proceeding
            while (userInput == string.Empty || userInput.Length > maxLength || !Regex.IsMatch(userInput, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine("The string you entered is not valid, please enter a valid string without any numbers or special characters:");
                userInput = Console.ReadLine();
            }

            return userInput;
        }
        
        private DateTime PromptDateTime(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
            
                var userInputArray = Console.ReadLine().ToLower().Split(" ");

                try
                {
                    var dayString = userInputArray[0];
                    var monthString = userInputArray[1];
                    var monthInteger = _monthDictionary[FirstCharToUpperCase(monthString)];
                    var dayInteger = uint.Parse(dayString);
                    return new DateTime(1, monthInteger, checked((int)dayInteger));
                
                }
                catch (Exception e)
                {
                    if (e is FormatException)
                    {
                        Console.WriteLine("The date given is not in the correct format (e.g. 17 March)");
                    }else if(e is KeyNotFoundException){
                        Console.WriteLine("The given month is not valid");
                    }else if (e is ArgumentOutOfRangeException || e is IndexOutOfRangeException)
                        Console.WriteLine("The date given is not a valid date");
                }
            }
        }
        
        
        
        public static string FirstCharToUpperCase(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

    }
}