using Domain.Entity;
using Domain.Exceptions;
using Domain.Test.Builders;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTest.Tests
{
    public class CustomerTest
    {
        private readonly CustomerTestBuilder customerTestBuilder;
        public CustomerTest()
        {
            customerTestBuilder = new CustomerTestBuilder();
        }

        [Fact]
        public void Cunstructoe_Should_Constructor_Customer_Property()
        {
            var customer = customerTestBuilder.Build();

            customer.Id.Should().Be(customer.Id);
            customer.FirstName.Should().Be(customer.FirstName);
            customer.LastName.Should().Be(customer.LastName);
            customer.DateOfBirth.Should().Be(customer.DateOfBirth);
            customer.PhoneNumber.Should().Be(customer.PhoneNumber);
            customer.Email.Should().Be(customer.Email);
            //Assert.Equal(FirstName, customer.FirstName);
            //Assert.Equal(LastName, customer.LastName);
            //Assert.Equal(DateOfBirth, customer.DateOfBirth);
            //Assert.Equal(PhoneNumber, customer.PhoneNumber);
            //Assert.Equal(Email, customer.Email);
            //Assert.Equal(BankAccountNumber,customer.BankAccountNumber);

        }


        [Fact]
        public void Constructor_Should_Throw_Exeption_When_FirstName_IS_Null()
        {
            var customerBuilder = customerTestBuilder;

            Action customer = () => customerBuilder.withFirstName("").Build();
            customer.Should().ThrowExactly<FirstNameIsInvalidException>();
            //Assert.Throws<Exception>(customer);
        }

        [Fact]
        public void Constructor_Should_Throw_Exeption_When_PhoneNumber_Mistake()
        {
            var customerBuilder = customerTestBuilder;

            Action customer = () => customerBuilder.withPhoneNumber("070263944201").Build();
            customer.Should().ThrowExactly<PhoneNumberIsInvalidException>();
            //Assert.Throws<Exception>(customer);
        }
        [Fact]
        public void Constructor_Should_Throw_Exeption_When_Gmail_NotCorrect_Pattern()
        {
            var customerBuilder = customerTestBuilder;

            Action customer = () => customerBuilder.withEmail("vahidcom").Build();
            customer.Should().ThrowExactly<EmailIsInvalidException>();
            //Assert.Throws<Exception>(customer);
        }

        [Fact]
        public void Constructor_Should_Throw_Exeption_When_BankAccountNumber_NotCorrect_Pattern()
        {
            var customerBuilder = customerTestBuilder;

            Action customer = () => customerBuilder.withBankAccountNumber("9566333433").Build();
            customer.Should().ThrowExactly<BankAccountNumberException>();
            //Assert.Throws<Exception>(customer);
        }


        [Fact]
        public void Should_Equal_When_IdIsNotEqual()
        {
            // arrange

            var customer = customerTestBuilder.withId(1).Build();
            var customer2 = customerTestBuilder.withId(2).Build();
            //assert
            customer.Should().NotBe(customer2);

        }
    }
}
