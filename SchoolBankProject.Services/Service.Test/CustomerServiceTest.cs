using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchoolBankProject.Data;
using SchoolBankProject.Services.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Test
{
    [TestClass]
    public class CustomerServiceTest
    {

        private List<Customer> AlistOfCustomers = new List<Customer>
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


        //Do not work... WHYYYYY :(
        [TestMethod]
        public void GetCustomerById_ReturnsCustomer()
        {
            //Arrange
            var dbContextMock = new Mock<SchoolBankContext>();
            var dbSetMock = new Mock<DbSet<Customer>>();

            dbSetMock.Setup(s => s.FindAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new Customer()));
            dbContextMock.Setup(y => y.Set<Customer>()).Returns(dbSetMock.Object);

            //Act
            var customerRepo = new CustomerServices(dbContextMock.Object);
            var customer = customerRepo.GetCustomerById(1).Result;
            //Assert
            customer.Should().NotBeNull();
            customer.Should().BeOfType<Customer>();
        }
      
    }
}



