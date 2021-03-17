using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchoolBankProject.Domain.AccountModels;
using SchoolBankProject.DTOs.AccountDTOs.Request;
using SchoolBankProject.DTOs.AccountDTOs.Response;
using SchoolBankProject.Services.Interfaces;
using SchoolBankProjet.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace SchoolBankProject.Test.ControllerTests
{
    [TestClass]
    public class AccountControllerTest
    {

        private readonly Mock<IRepositoryWrapper> mockService;
        private readonly AccountController accountController;

        public AccountControllerTest()
        {
            mockService = new Mock<IRepositoryWrapper>();
            accountController = new AccountController(mockService.Object);
        }

        private IEnumerable<BankAccount> bankAccounts = new List<BankAccount>
        {
            new BankAccount() { Id = Guid.NewGuid(),
                AccountNumber = "1234562",
                IBANNumber = "1234412344123123",
                Balance = 0,
                ClearingNumber = "1234123",
                CustomerId  = Guid.NewGuid(),
                AccountTypeId = 1  }
        };

        private BankAccount bankAccountById = new BankAccount()
        {
            Id = Guid.Parse("CF3A6B4E-9D3D-45E9-AF0B-40DB338930EF"),
            AccountNumber = "1234562",
            IBANNumber = "1234412344123123",
            Balance = 0,
            ClearingNumber = "1234123",
            CustomerId = Guid.NewGuid(),
            AccountTypeId = 1
        };

        private BankAccount bankAccount = new BankAccount()
        {
            Id = Guid.Parse("CF3A6B4E-9D3D-45E9-AF0B-40DB338930EF"),
            AccountNumber = "1234562",
            IBANNumber = "1234412344123123",
            Balance = 0,
            ClearingNumber = "1234123",
            CustomerId = Guid.NewGuid(),
            AccountTypeId = 1
        };




        [TestMethod]
        public async Task TestGetAccount_ShouldReturnOkAndContainOneAccountInList()
        {
            //Arrange
            mockService.Setup(x => x.BankAccount.GetAccounts()).Returns(Task.Run(() => bankAccounts));

            //Act
            var response = await accountController.GetBankAccounts();
            //Assert
            var result = response.Should().BeOfType<JsonResult<IEnumerable<BankAccount>>>().Subject;
            var account = result.Content.Should().BeAssignableTo<IEnumerable<BankAccount>>().Subject;
            account.Count().Should().Be(1);
        }

        [TestMethod]
        public async Task TestGetAccountById_ShouldReturnCorrectId()
        {
            //Arrange
            mockService.Setup(x => x.BankAccount.GetAccountById(It.IsAny<Guid>())).Returns(Task.Run(() => bankAccountById));
            //Act
            var response = await accountController.GetBankAccount(Guid.Parse("CF3A6B4E-9D3D-45E9-AF0B-40DB338930EF"));
            //Assert
            var result = response.Should().BeOfType<JsonResult<BankAccount>>().Subject;
            var account = result.Content.Should().BeAssignableTo<BankAccount>().Subject;
            account.Id.Should().Be(bankAccountById.Id);
        }

        [TestMethod]
        public async Task TestDepsosit_BalanceShouldIncreaseCorrectly()
        {
            //Arrange
            var DepositRequest = new CreateDepositRequest() {Id = Guid.NewGuid(),Balance = 0, IBANNumber = "123451235", 
                AccountNumber = "123451235", Amount = 2000, ClearingNumber = "2134123" };
            mockService.Setup(y => y.BankAccount.GetAccountById(It.IsAny<Guid>())).Returns(Task.Run(() => bankAccountById));
            mockService.Setup(x => x.BankAccount.Deposit(It.IsAny<Guid>(), It.IsAny<BankAccount>(), It.IsAny<int>())).ReturnsAsync(true);
            mockService.Setup(x => x.Transactions.AddTransaction(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<string>()));


            //Act
            var response = await accountController.Deposit(DepositRequest);

            //Assert
            mockService.Verify(x => x.BankAccount.GetAccountById(It.IsAny<Guid>()), Times.Once());
            mockService.Verify(x => x.BankAccount.Deposit(It.IsAny<Guid>(), It.IsAny<BankAccount>(), It.IsAny<int>()), Times.Once());
            mockService.Verify(x => x.Transactions.AddTransaction(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<string>()));
            var result = response.Should().BeOfType<JsonResult<depositResponse>>().Subject;
            var account = result.Content.Should().BeAssignableTo<depositResponse>().Subject;
            account.Balance.Should().Be(2000);
        }

        [TestMethod]
        public async Task TestWithDraw_BalanceShouldDecreaseCorrectly()
        {
            //Arrange
            var DepositRequest = new CreateWithdrawRequest() { Amount = 2000};
            mockService.Setup(y => y.BankAccount.GetAccountById(It.IsAny<Guid>())).Returns(Task.Run(() => bankAccountById));
            mockService.Setup(x => x.BankAccount.Withdraw(It.IsAny<Guid>(), It.IsAny<BankAccount>(), It.IsAny<int>())).ReturnsAsync(true);
            mockService.Verify(x => x.Transactions.AddTransaction(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<string>()));

            //Act
            var response = await accountController.Withdraw(DepositRequest);

            //Assert
            mockService.Verify(x => x.BankAccount.GetAccountById(It.IsAny<Guid>()), Times.Once());
            mockService.Verify(x => x.BankAccount.Deposit(It.IsAny<Guid>(), It.IsAny<BankAccount>(), It.IsAny<int>()), Times.Once());
            mockService.Verify(x => x.Transactions.AddTransaction(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<string>()));
        
        }
    }
}
