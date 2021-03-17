using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchoolBankProject.CustomerAPI.Controllers;
using SchoolBankProject.Domain.CustomerModels;
using SchoolBankProject.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FluentAssertions;
using System;
using System.Linq;
using SchoolBankProject.Domain.AccountModels;
using SchoolBankProject.DTOs.CustomerDTOs.Request;
using SchoolBankProject.DTOs.CustomerDTOs.Response;

namespace SchoolBankProject.CustomerTest
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
            Id = Guid.NewGuid(),
            Address = "Gothenburg",
            Country = "Sweden",
            FirstName = "Jörgen",
            LastName = "Svensson",
            PersonalNumber = "750304-2322",
            PhoneNumber = 043203213},
            new Customer() {Id = Guid.NewGuid(),
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
            Id = Guid.Parse("CF3A6B4E-9D3D-45E9-AF0B-40DB338930EF"),
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
            PersonalNumber = "750304-2322",
            PhoneNumber = 043203213,
            Type = "SavingsAccount"
        };



        [TestMethod]
        public async Task TestGetAllCustomers_WithNoCustomers_ShouldReturnNotFound()
        {
            //Arrange          
            mockService.Setup(x => x.Customers.GetAllCustomers()).Returns(Task.Run(() => emptyListofCustomers));
            //Act
            var response = await customerController.GetAllCustomers();
            //Assert
            response.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task TestGetAllCustomers_WithTwoCustomersInList_ShouldReturnCorrectNumbersOfCustomers()
        {
            //Arrange
            mockService.Setup(x => x.Customers.GetAllCustomers()).Returns(Task.Run(() => AlistOfCustomers));
            //Act
            var response = await customerController.GetAllCustomers();
            //Assert
            var result = response.Should().BeOfType<JsonResult<IEnumerable<Customer>>>().Subject;
            var customer = result.Content.Should().BeAssignableTo<IEnumerable<Customer>>().Subject;
            customer.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task TestGetCustomerById_WithCorrectId_ShouldReturnCorrectCustomerWithSameId()
        {
            //Arrange
            mockService.Setup(x => x.Customers.GetCustomerById(It.IsAny<Guid>())).Returns(Task.Run(() => customerById));
            //Act
            var response = await customerController.GetById(Guid.Parse("CF3A6B4E-9D3D-45E9-AF0B-40DB338930EF"));
            //Assert
            var result = response.Should().BeOfType<JsonResult<Customer>>().Subject;
            var customer = result.Content.Should().BeAssignableTo<Customer>().Subject;
            Assert.AreEqual(customerById.Id, customer.Id);
        }

        [TestMethod]
        public async Task TestGetCustomerById_WithBadId_ShouldReturnNotFoundResult()
        {
            //Arrange
            mockService.Setup(x => x.Customers.GetCustomerById(It.IsAny<Guid>()));
            mockService.Setup(y => y.Customers.GetAllCustomers()).Returns(Task.Run(() => emptyListofCustomers));
            //Act
            var response = await customerController.GetById(Guid.NewGuid());
            //Assert
            mockService.Verify(x => x.Customers.GetCustomerById(It.IsAny<Guid>()), Times.Once());
            response.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task TestCreateCustomer_ReturnsCorrectAccountType()
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

            mockService.Setup(x => x.Customers.CreateCustomer(It.IsAny<Customer>())).ReturnsAsync(true);
            mockService.Setup(y => y.BankAccount.GetAccountTypeByName(It.IsAny<string>()))
                                    .ReturnsAsync(() => new AccountType { Type = "SavingsAccount" });
            mockService.Setup(e => e.BankAccount.CreateBankAccount(It.IsAny<BankAccount>())).ReturnsAsync(() => new BankAccount
            {
                Id = (Guid.Parse("CF3A6B4E-9D3D-45E9-AF0B-40DB338930EF")),
                AccountNumber = "1234567",
                Balance = 0,
                ClearingNumber = "8430",
                IBANNumber = "2134512345123",
                AccountTypeId = 1,
                CustomerId = (Guid.Parse("CF3A6B4E-8DEF-45E9-AF0B-40DB338930EF")),
            });


            //Act
            var response = await customerController.CreateCustomer(customer);


            //Assert
            mockService.Verify(x => x.Customers.CreateCustomer(It.IsAny<Customer>()), Times.Once());
            mockService.Verify(x => x.BankAccount.GetAccountTypeByName(It.IsAny<string>()), Times.Once());
            mockService.Verify(x => x.BankAccount.CreateBankAccount(It.IsAny<BankAccount>()), Times.Once());
            var result = response.Should().BeOfType<JsonResult<CreatedCustomerResponse>>().Subject;
            var customerResponseResult = result.Content.Should().BeAssignableTo<CreatedCustomerResponse>().Subject;
            customerResponseResult.ClearingNumber.Should().Be("8430");
        }


        [TestMethod]
        public async Task TestCreateCustomer_WithEmptyCustomerRequest_ShouldReturnBadRequest()
        {
            //Arrange
            mockService.Setup(x => x.Customers.CreateCustomer(It.IsAny<Customer>())).ReturnsAsync(true);
            customerController.ModelState.AddModelError("key", "error message");
            //Act
            var response = await customerController.CreateCustomer(null);
            //Assert
            var result = response.Should().BeOfType<BadRequestErrorMessageResult>().Subject;
            result.Message.Should().Be("Some fields was not inputed");
        }

        [TestMethod]
        public async Task TestCreateCustomer_NotCreatingCustomerCorrectyl_ShouldReturnBadRequest()
        {
            //ARrange
            mockService.Setup(x => x.Customers.CreateCustomer(It.IsAny<Customer>())).ReturnsAsync(false);
            //Act
            var response = await customerController.CreateCustomer(customer);
            //assert
            var result = response.Should().BeOfType<BadRequestErrorMessageResult>().Subject;
            result.Message.Should().Be("Something went wrong in creating customer");
        }

        [TestMethod]
        public async Task TestCreateCustomer_ReturningEmptyAccountType_ShouldReturnBadRequest()
        {
            //Arrange
            mockService.Setup(x => x.BankAccount.GetAccountTypeByName(null));
            mockService.Setup(y => y.Customers.CreateCustomer(It.IsAny<Customer>())).ReturnsAsync(true);
            //Act
            var response = await customerController.CreateCustomer(customer);
            //Assert
            mockService.Verify(x => x.Customers.CreateCustomer(It.IsAny<Customer>()), Times.Once());
            mockService.Verify(x => x.BankAccount.GetAccountTypeByName(It.IsAny<string>()), Times.Once());
            var result = response.Should().BeOfType<BadRequestErrorMessageResult>().Subject;
            result.Message.Should().Be("No AccountType exist with that name");
        }
    }
}
