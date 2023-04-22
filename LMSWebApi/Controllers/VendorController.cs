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
    //public class VendorController : BaseController
    //{
    //    private readonly IVendorService _vendorService;
    //    public VendorController(IVendorService vendorService)
    //    {
    //        _vendorService = vendorService;
    //    }

    //    [Route("GetAll")]
    //    [HttpGet]
    //    public async Task<IServiceResponse <List<VendorDTO>>> GetAll()
    //    {
    //       return await HandleApiOperationAsync(async () =>
    //       {
    //           var datalist = await _vendorService.GetAll();
    //           return new ServiceResponse<List<VendorDTO>>
    //           {
    //               Object = datalist,
    //           };
    //       });
    //    }


    //    [Route("Add")]
    //    [HttpPost]
    //    public async Task<IServiceResponse<VendorDTO>> Add(VendorDTO vendorDTO)
    //    {
    //        return await HandleApiOperationAsync(async () =>
    //        {
    //            var data = await _vendorService.Add(vendorDTO);
    //            return new ServiceResponse<VendorDTO>
    //            {
    //                Object = data,
    //            };
    //        });
    //    }


    //    [HttpGet]
    //    [Route("Get/{id}")]
    //    public async Task<IServiceResponse<VendorDTO>> GetClientById(int id)
    //    {
    //        return await HandleApiOperationAsync(async () => {
    //            var data = await _vendorService.GetVendorById(id);

    //            return new ServiceResponse<VendorDTO>
    //            {
    //                Object = data
    //            };
    //        });
    //    }


    //    [HttpPut]
    //    [Route("Update/{id}")]
    //    public async Task<IServiceResponse<bool>> UpdateCient(int id, VendorDTO vendorDTO)
    //    { 
    //        return await HandleApiOperationAsync(async () =>
    //        {
    //            await _vendorService.UpdateVendor(id, vendorDTO);

    //            return new ServiceResponse<bool>(true);
    //        });
    //    }




    //}
}