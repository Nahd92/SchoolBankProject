using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchoolBankProject.CustomerAPI.Controllers;
using SchoolBankProject.DTOs.CustomerDTOs.Request;
using SchoolBankProject.DTOs.CustomerDTOs.Response;
using SchoolBankProject.LinqSql.Data;
using SchoolBankProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace SchoolBankProject.Tests.Controllers
{
    [TestClass]
    public class CustomerControllerTest
    {

        private readonly Mock<IRepositoryWrapper> mockService;
        private readonly CustomerController customerController;

        public CustomerControllerTest()
        {
            mockService = new Mock<IRepositoryWrapper>();
            customerController = new CustomerController(mockService.Object);
        }

        private IEnumerable<Customer> AlistOfCustomers = new List<Customer>
        {
            new Customer() {
            Id = 1,
            Address = "Gothenburg",
            Country = "Sweden",
            FirstName = "Jörgen",
            LastName = "Svensson",
            PersonalNumber = "750304-2322",
            PhoneNumber = 043203213},
            new Customer() {
            Id =1,
            Address = "Gothenburg",
            Country = "Sweden",
            FirstName = "Jörgen",
            LastName = "Svensson",
            PersonalNumber = "750304-2322",
            PhoneNumber = 043203213}
        };


        private IEnumerable<Customer> emptyListofCustomers = null;
        private Customer customerById = new Customer()
        {
            Id = 1,
            Address = "Gothenburg",
            Country = "Sweden",
            FirstName = "Jörgen",
            LastName = "Svensson",
            PersonalNumber = "750304-2322",
            PhoneNumber = 043203213
        };

        private CreateCustomerRequest customer = new CreateCustomerRequest()
        {
            Address = "Gothenburg",
            Country = "Sweden",
            FirstName = "Jörgen",
            LastName = "Svensson",
            PhoneNumber = 043203213,
            Type = "SavingsAccount"
        };

        [TestMethod]
        public void TestGetAllCustomers_WithNoCustomers_ShouldReturnNotFound()
        {
            //Arrange          
            mockService.Setup(x => x.Customers.GetAllCustomers()).Returns(emptyListofCustomers);
            //Act
            var response = customerController.GetAllCustomers();
            //Assert
            response.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void TestGetAllCustomers_WithTwoCustomersInList_ShouldReturnCorrectNumbersOfCustomers()
        {
            //Arrange
            mockService.Setup(x => x.Customers.GetAllCustomers()).Returns(AlistOfCustomers);
            //Act
            var response = customerController.GetAllCustomers();
            //Assert
            var result = response.Should().BeOfType<JsonResult<IEnumerable<Customer>>>().Subject;
            var customer = result.Content.Should().BeAssignableTo<IEnumerable<Customer>>().Subject;
            customer.Count().Should().Be(2);
        }

        [TestMethod]
        public void TestGetCustomerById_WithCorrectId_ShouldReturnCorrectCustomerWithSameId()
        {
            //Arrange
            mockService.Setup(x => x.Customers.GetCustomerById(It.IsAny<int>())).Returns(customerById);
            //Act
            var response = customerController.GetById(1);
            //Assert
            var result = response.Should().BeOfType<JsonResult<Customer>>().Subject;
            var customer = result.Content.Should().BeAssignableTo<Customer>().Subject;
            Assert.AreEqual(customerById.Id, customer.Id);
        }

        [TestMethod]
        public void TestGetCustomerById_WithBadId_ShouldReturnNotFoundResult()
        {
            //Arrange
            mockService.Setup(x => x.Customers.GetCustomerById(It.IsAny<int>()));
            mockService.Setup(y => y.Customers.GetAllCustomers()).Returns(emptyListofCustomers);
            //Act
            var response = customerController.GetById(1);
            //Assert
            mockService.Verify(x => x.Customers.GetCustomerById(It.IsAny<int>()), Times.Once());
            response.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void TestCreateCustomer_ReturnsCorrectAccountType()
        {
            //Arrange
            var customer = new CreateCustomerRequest()
            {
               
                Address = "Gothenburg",
                Country = "Sweden",
                FirstName = "Jörgen",
                LastName = "Svensson",
                PersonalNumber = "750304-2322",
                PhoneNumber = 043203213,
                Type = "SavingsAccount"
            };

            mockService.Setup(x => x.Customers.CreateCustomer(It.IsAny<CreateCustomerRequest>())).Returns(customerById); 
            mockService.Setup(y => y.BankAccount.GetAccountTypeByName(It.IsAny<string>()))
                                    .Returns(() => new AccountType { Type = "SavingsAccount" });
            mockService.Setup(e => e.BankAccount.CreateBankAccount(It.IsAny<BankAccount>())).Returns(new BankAccount 
            {
                Id = 1,
                AccountNumber = "1234562",
                IBANNumber = "1234412344123123",
                Balance = 2500,
                ClearingNumber = "1234123",
                CustomerId = 2,
                AccountTypeId = 1
            });


            //Act
            var response = customerController.CreateCustomer(customer);


            //Assert
            mockService.Verify(x => x.Customers.CreateCustomer(It.IsAny<CreateCustomerRequest>()), Times.Once());
            mockService.Verify(x => x.BankAccount.GetAccountTypeByName(It.IsAny<string>()), Times.Once());
            mockService.Verify(x => x.BankAccount.CreateBankAccount(It.IsAny<BankAccount>()), Times.Once());
            var result = response.Should().BeOfType<JsonResult<CreatedCustomerResponse>>().Subject;
            var customerResponseResult = result.Content.Should().BeAssignableTo<CreatedCustomerResponse>().Subject;
            customerResponseResult.ClearingNumber.Should().Be("8430");
        }


        [TestMethod]
        public void TestCreateCustomer_WithEmptyCustomerRequest_ShouldReturnBadRequest()
        {
            //Arrange
            mockService.Setup(x => x.Customers.CreateCustomer(It.IsAny<CreateCustomerRequest>()));
            customerController.ModelState.AddModelError("key", "error message");
            //Act
            var response = customerController.CreateCustomer(null);
            //Assert
            var result = response.Should().BeOfType<BadRequestErrorMessageResult>().Subject;
            result.Message.Should().Be("Not all fields were inputed");
        }

        [TestMethod]
        public void TestCreateCustomer_NotCreatingCustomerCorrecty_ShouldReturnBadRequest()
        {
            //ARrange
            mockService.Setup(x => x.Customers.CreateCustomer(It.IsAny<CreateCustomerRequest>()));
            //Act
            var response = customerController.CreateCustomer(customer);
            //assert
            var result = response.Should().BeOfType<BadRequestErrorMessageResult>().Subject;
            result.Message.Should().Be("Not all fields were inputed");
        }

        [TestMethod]
        public void TestCreateCustomer_ReturningEmptyAccountType_ShouldReturnBadRequest()
        {
            //Arrange
            mockService.Setup(x => x.BankAccount.GetAccountTypeByName(null));
            mockService.Setup(y => y.Customers.CreateCustomer(It.IsAny<CreateCustomerRequest>())).Returns(customerById);
            //Act
            var response = customerController.CreateCustomer(customer);
            //Assert
            mockService.Verify(x => x.Customers.CreateCustomer(It.IsAny<CreateCustomerRequest>()), Times.Once());
            mockService.Verify(x => x.BankAccount.GetAccountTypeByName(It.IsAny<string>()), Times.Once());
            var result = response.Should().BeOfType<BadRequestErrorMessageResult>().Subject;
            result.Message.Should().Be("No AccountType exist with that name");
        }
    }
}
