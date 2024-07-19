using Domain.Exceptions;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Customer
    {
        public long Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string BankAccountNumber { get; set; }

        public Customer(long Id, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
        {
            GuardAgaintsInvalidFirstName(firstName);

            bool isValidPhoneNumber = IsValidPhoneNumber(phoneNumber);
            if (!isValidPhoneNumber)
                throw new PhoneNumberIsInvalidException();

            bool isValidBankAccountNumber = IsValidBankAccountNumber(bankAccountNumber);
            if (!isValidBankAccountNumber)
                throw new BankAccountNumberException();

            bool isValidEmail = IsValidEmail(email);
            if (!isValidEmail)
                throw new EmailIsInvalidException();

            this.Id = Id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.BankAccountNumber = bankAccountNumber;


        }


        public static Customer CreateNew(long Id, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
        {

            bool isValidPhoneNumber = IsValidPhoneNumber(phoneNumber);
            if (!isValidPhoneNumber)
                throw new PhoneNumberIsInvalidException();


            bool isValidEmail = IsValidEmail(email);
            if (!isValidEmail)
                throw new EmailIsInvalidException();

            bool isValidBankAccountNumber = IsValidBankAccountNumber(bankAccountNumber);
            if (!isValidBankAccountNumber)
                throw new BankAccountNumberException();

            return new Customer(Id, firstName, lastName, dateOfBirth, phoneNumber, email, bankAccountNumber);


        }


        private static void GuardAgaintsInvalidFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new FirstNameIsInvalidException();
        }
        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            var newPhoneNumber = phoneNumberUtil.Parse($"{phoneNumber}", "IR");
            var isValid = phoneNumberUtil.IsValidNumber(newPhoneNumber);
            return isValid;
        }


        private static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsValidBankAccountNumber(string bankAccountNumber)
        {
            if (bankAccountNumber.Length != 16)
                return false;

            return true;

         
        } 

    }
}
