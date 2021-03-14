using SchoolBankProject.DTOs.AccountDTOs.Request;
using SchoolBankProject.DTOs.CustomerDTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Gateway.Services.Interfaces
{
   public interface IAccountGateway
    {
        Task<HttpResponseMessage> TransferMoney(CreateDepositRequest request);
        Task<HttpResponseMessage> DepositMoney(CreateDepositRequest request);
        Task<HttpResponseMessage> WithdrawMoney(CreateWithdrawRequest request);

    }
}
