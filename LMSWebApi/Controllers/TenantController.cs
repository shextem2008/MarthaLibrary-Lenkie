using Contracts.Collections;
using Contracts.Dto;
using Contracts.Entities;
using Contracts.Extensions;
using Microsoft.AspNetCore.Mvc;
using Services;
using StakeHoldersWebApi.Models.IdentityModels;
using System.Security.Claims;

namespace LMSWebApi.Controllers
{
    //public class TenantController : BaseController
    //{
    //    private readonly ITenantService _tenantService;
    //    public TenantController(ITenantService tenantService)
    //    {
    //        _tenantService = tenantService;
    //    }

    //    [Route("GetAll")]
    //    [HttpGet]
    //    public async Task<IServiceResponse <List<TenantDTO>>> GetAll()
    //    {
    //       return await HandleApiOperationAsync(async () =>
    //       {
    //           var datalist = await _tenantService.GetAll();
    //           return new ServiceResponse<List<TenantDTO>>
    //           {
    //               Object = datalist,
    //           };
    //       });
    //    }


    //    [Route("Add")]
    //    [HttpPost]
    //    public async Task<IServiceResponse<TenantDTO>> Add(TenantDTO tenantDTO)
    //    {
    //        return await HandleApiOperationAsync(async () =>
    //        {
    //            var data = await _tenantService.Add(tenantDTO);
    //            return new ServiceResponse<TenantDTO>
    //            {
    //                Object = data,
    //            };
    //        });
    //    }


    //    [HttpGet]
    //    [Route("Get/{id}")]
    //    public async Task<IServiceResponse<TenantDTO>> GetTenantById(int id)
    //    {
    //        return await HandleApiOperationAsync(async () => {
    //            var data = await _tenantService.GetTenantById(id);

    //            return new ServiceResponse<TenantDTO>
    //            {
    //                Object = data
    //            };
    //        });
    //    }



    //}
}