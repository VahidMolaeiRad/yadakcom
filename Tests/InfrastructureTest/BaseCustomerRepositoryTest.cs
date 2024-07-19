

using Domain.Test.Builders;
using FluentAssertions;
using Infrastructure.Repository;

namespace Infrastructure_Test
{
    public class BaseCustomerRepositoryTest
    {
        private readonly BaseCustomerRepository repository;
        private readonly CustomerTestBuilder customerBuilder;
        public BaseCustomerRepositoryTest()
        {
            this.repository = new BaseCustomerRepository();
            customerBuilder = new CustomerTestBuilder();
        }
        [Fact]
        public void Should_Return_ListOf_Customer()
        {
            // arrange
            //act
            var customer = repository.GetAll();

            //assert
            customer.Should().HaveCountGreaterThanOrEqualTo(0);

        }

        [Fact]
        public void Should_Add_New_Customer_To_CustomersList()
        {
            // arrange

            var customer = customerBuilder.Build();

            //act 
            repository.Create(customer);

            //Assert
            repository.Customers.Should().Contain(customer);
        }

        [Fact]
        public void Should_Return_CustomerByID()
        {
            // arrange
            const long Id = 3;
            var expectedCustomer = customerBuilder.withId(Id).Build();
            repository.Create(expectedCustomer);


            //act

            var actual = repository.GetById(Id);

            //assert
            actual.Should().Be(expectedCustomer);

        }
        [Fact]
        public void Should_Return_Null_When_IdNotExist()
        {
            // arrange
            const long Id = 53;
            //act
            var actual = repository.GetById(Id);
            //assert
            actual.Should().BeNull();

        }


        [Fact]
        public void Should_Delete_Customer_From_Customers()
        {
            // arrange
            const long Id = 3;
            var expectedCustomer = customerBuilder.withId(Id).Build();
            repository.Create(expectedCustomer);

            //act

            repository.Delete(Id);

            //assert
            repository.Customers.Should().NotContain(expectedCustomer);

        }

    }

}