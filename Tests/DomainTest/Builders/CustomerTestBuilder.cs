using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.Builders
{
    public class CustomerTestBuilder
    {
        private long _id = 1;
        private string _firstName = "Vahid";
        private string _lastName = "MolaeiRad";
        private DateTime _dateOfBirth = DateTime.Now;
        private string _phoneNumber = "09026394420";
        private string _email = "Molaeirad1992@gmail.com";
        private string _bankAccountNumber = "1234567895698421";


        public CustomerTestBuilder withId(long Id)
        {
            _id=Id;
            return this;
        }

        public CustomerTestBuilder withFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public CustomerTestBuilder withPhoneNumber(string phoneNumber)
        {
            _phoneNumber=phoneNumber;
            return this;
        }
        public CustomerTestBuilder withEmail(string email)
        {
            _email = email;
            return this;
        }


        public CustomerTestBuilder withBankAccountNumber(string bankAccountNumber)
        {
            _bankAccountNumber = bankAccountNumber;
            return this;
        }


        public Customer Build()
        {
            return new Customer(_id, _firstName, _lastName, _dateOfBirth, _phoneNumber, _email, _bankAccountNumber);
        }

    }
}
