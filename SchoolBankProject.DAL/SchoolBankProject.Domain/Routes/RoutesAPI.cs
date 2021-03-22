using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolBankProject.Domain.Routes
{
    public static class RoutesAPI
    {
        public const string Root = "api";
        public const string Base = Root + "/";
        public const string gatewayCustomer = "Customer/";
        public const string gatewayAccount = "Account/";

        public static class Accounts
        {
            public const string GetAllAccounts = Base + "Accounts";
            public const string CreateAccount = Base + "Account";
            public const string GetAccount = Base + "Account/{id}";
            public const string Withdraw = Base + "Account/Withdraw";
            public const string TransferMoney = Base + "Account/TransferMoney";
            public const string Deposit = Base + "Account/Deposit";
        }

        public static class Customers
        {
            public const string GetAllCustomers = Base + "Customers";
            public const string GetAllCustomersBankAccounts = Base + "Customers" + "/BankAccounts/{id}";
            public const string CreateCustomer = Base + "Customer";
            public const string GetCustomerById = Base + "Customer/{id}";
            public const string UpdateCustomer = Base + "Customer/{id}";
            public const string DeleteCustomer = Base + "Customer/{id}";
        }


        public static class GatewayCustomer
        {
            public const string GetCustomers = gatewayCustomer + "Get";
            public const string CreateCustomer = gatewayCustomer +"Create";
            public const string GetCustomerById = gatewayCustomer + "Get/{id}";
            public const string UpdateCustomer = gatewayCustomer + "Update/{id}";
            public const string DeleteCustomer = gatewayCustomer + "Delete/{id}";
        }
        public static class GatewayAccount
        {
            public const string GetAccount = gatewayAccount + "Get";
            public const string CreateAccount = gatewayAccount + "Create";
            public const string GetAccountById = gatewayAccount + "Get/{id}";
            public const string UpdateAccount = gatewayAccount + "Update/{id}";
            public const string DeleteAccount = gatewayAccount + "Delete/{id}";
            public const string Withdraw = gatewayAccount + "Withdraw";
            public const string Deposit = gatewayAccount + "Deposit";
            public const string Transfer = gatewayAccount + "Transfer";
        }
    }
}