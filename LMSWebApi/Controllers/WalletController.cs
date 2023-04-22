using Contracts.Collections;
using Contracts.Dto;
using Contracts.Entities;
using Contracts.Extensions;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace LMSWebApi.Controllers
{
    public class WalletController : BaseController
    {
        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IServiceResponse <List<WalletDTO>>> GetAll()
        {
           return await HandleApiOperationAsync(async () =>
           {
               var datalist = await _walletService.GetAll();
               return new ServiceResponse<List<WalletDTO>>
               {
                   Object = datalist,
               };
           });
        }


        [Route("Add")]
        [HttpPost]
        public async Task<IServiceResponse<Wallet>> Add(WalletDTO walletDTO)
        {
            return await HandleApiOperationAsync(async () =>
            {
                var data = await _walletService.Add(walletDTO);
                return new ServiceResponse<Wallet>
                {
                    Object = data,
                };
            });
        }


        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IServiceResponse<WalletDTO>> GetWalletById(int id)
        {
            return await HandleApiOperationAsync(async () => {
                var data = await _walletService.GetWalletById(id);

                return new ServiceResponse<WalletDTO>
                {
                    Object = data
                };
            });
        }


        //[HttpDelete]
        //[Route("Delete/{id}")]
        //public async Task<IServiceResponse<bool>> Delete(int id)
        //{
        //    return await HandleApiOperationAsync(async () => {
        //        await _userService.Remove(id);

        //        return new ServiceResponse<bool>(true);
        //    });
        //}

    }
}