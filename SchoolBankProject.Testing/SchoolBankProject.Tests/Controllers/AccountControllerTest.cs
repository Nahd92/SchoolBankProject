using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchoolBankProject.DTOs.AccountDTOs.Request;
using SchoolBankProject.DTOs.AccountDTOs.Response;
using SchoolBankProject.LinqSql.Data;
using SchoolBankProject.Services.Interfaces;
using SchoolBankProjet.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

namespace SchoolBankProject.Tests.Controllers
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
            new BankAccount() { Id = 1,
                AccountNumber = "1234562",
                IBANNumber = "1234412344123123",
                Balance = 0,
                ClearingNumber = "1234123",
                CustomerId  = 2,
                AccountTypeId = 1  }
        };

        private BankAccount bankAccountById = new BankAccount()
        {
            Id = 1,
            AccountNumber = "1234562",
            IBANNumber = "1234412344123123",
            Balance = 2500,
            ClearingNumber = "1234123",
            CustomerId = 2,
            AccountTypeId = 1
        };


        [TestMethod]
        public void TestGetAccount_ShouldReturnOkAndContainOneAccountInList()
        {
            //Arrange
            mockService.Setup(x => x.BankAccount.GetAccounts()).Returns(bankAccounts);

            //Act
            var response = accountController.GetBankAccounts();
            //Assert
            var result = response.Should().BeOfType<JsonResult<IEnumerable<BankAccount>>>().Subject;
            var account = result.Content.Should().BeAssignableTo<IEnumerable<BankAccount>>().Subject;
            account.Count().Should().Be(1);
        }

        [TestMethod]
        public void TestGetAccountById_ShouldReturnCorrectId()
        {
            //Arrange
            mockService.Setup(x => x.BankAccount.GetAccountById(It.IsAny<int>())).Returns(bankAccountById);
            //Act
            var response = accountController.GetBankAccount(1);
            //Assert
            var result = response.Should().BeOfType<JsonResult<BankAccount>>().Subject;
            var account = result.Content.Should().BeAssignableTo<BankAccount>().Subject;
            account.Id.Should().Be(bankAccountById.Id);
        }

        [TestMethod]
        public void TestDepsosit_BalanceShouldIncreaseCorrectly()
        {
            //Arrange
            var DepositRequest = new CreateDepositRequest()
            {
                Id = 1,
                Balance = 0,
                IBANNumber = "123451235",
                AccountNumber = "123451235",
                Amount = 2000,
                ClearingNumber = "2134123"
            };
            mockService.Setup(y => y.BankAccount.GetAccountById(It.IsAny<int>())).Returns(bankAccountById);
            mockService.Setup(x => x.BankAccount.Deposit(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            mockService.Setup(x => x.Transactions.AddTransaction(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>())).Returns(new Transaction { AccountNumber = "123451235", Date = DateTime.Now, BankAccountId = 1, Amount = 2500,
            Action = "Deposit"});


            //Act
                var response = accountController.Deposit(DepositRequest);

            //Assert
            mockService.Verify(x => x.BankAccount.GetAccountById(It.IsAny<int>()), Times.Once());
            mockService.Verify(x => x.BankAccount.Deposit(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            mockService.Verify(x => x.Transactions.AddTransaction(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>()));
            var result = response.Should().BeOfType<JsonResult<TransactionReponse>>().Subject;
            var account = result.Content.Should().BeAssignableTo<TransactionReponse>().Subject;
            account.Balance.Should().Be(2500);
        }

        [TestMethod]
        public void TestWithdraw_BalanceShouldDecreaseCorrectly()
        {
            //Arrange
            var DepositRequest = new CreateWithdrawRequest() { Id = 1, Amount = 2000 };
            var BankAccount = new BankAccount() { Id = 1 };
            var withdrawAmount = -2000;
            var fee = -100;
            mockService.Setup(y => y.BankAccount.GetAccountById(It.IsAny<int>())).Returns(bankAccountById);
            mockService.Setup(x => x.BankAccount.Withdraw(bankAccountById.Id, withdrawAmount)).Returns(true);
            mockService.Setup(x => x.BankAccountService.WithdrawIsPossible(BankAccount, withdrawAmount)).Returns(true);
            mockService.Setup(x => x.BankAccountService.CalculateWithdrawFee(BankAccount)).Returns(fee);
            mockService.Setup(x => x.Transactions.AddTransaction(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>())).Returns(new Transaction
            {
                AccountNumber = "123451235",
                Date = DateTime.Now,
                BankAccountId = 1,
                Amount = 500,
                Action = "Withdraw"
            });

            //Act
            var response = accountController.Withdraw(DepositRequest);

            //Assert
            mockService.Verify(x => x.BankAccount.GetAccountById(It.IsAny<int>()), Times.Once());
            mockService.Verify(x => x.BankAccount.Withdraw(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            mockService.Verify(x => x.Transactions.AddTransaction(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>()), Times.Once());
            var result = response.Should().BeOfType<JsonResult<TransactionReponse>>().Subject;
            var account = result.Content.Should().BeAssignableTo<TransactionReponse>().Subject;
            account.Balance.Should().Be(500);
        }
    }
}
