using Contracts.Collections;
using Contracts.Dto;
using Contracts.Extensions;
using IPagedList;
using Microsoft.AspNetCore.Mvc;
using Services;
using static Contracts.Utils.CoreConstants;

namespace LMSWebApi.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


     

        [HttpGet]    
        [Route("GetEmployees/{pageNumber}/{pageSize}/{search}")]
        public async Task<IServiceResponse<IPagedList<EmployeeDTO>>> GetEmployees(int pageNumber = 1, int pageSize = WebConstants.DefaultPageSize, string search = null)
        {
            return await HandleApiOperationAsync(async () =>
            {
                var employees = await _employeeService.GetEmployees(pageNumber, pageSize, search);

                return new ServiceResponse<IPagedList<EmployeeDTO>>
                {
                    Object = employees
                };
            });
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IServiceResponse<IList<EmployeeDTO>>> GetAll()
        {
           return await HandleApiOperationAsync(async () =>
           {
               var datalist = await GetEmployees(1, WebConstants.DefaultPageSize,null); 
         
               return new ServiceResponse<IList<EmployeeDTO>>
               {
                   Object = datalist.Object.ToList()
               };
           });
        }


        [Route("Add")]
        [HttpPost]
        public async Task<IServiceResponse<EmployeeDTO>> Add(EmployeeDTO employeeDTO)
        {
            return await HandleApiOperationAsync(async () =>
            {
                var data = await _employeeService.Add(employeeDTO);
                return new ServiceResponse<EmployeeDTO>
                {
                    Object = data,
                };
            });
        }

        //[HttpGet("{productId:int}")]
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IServiceResponse<EmployeeDTO>> GetEmployeeById(int id)
        {
            return await HandleApiOperationAsync(async () => {
                var data = await _employeeService.GetEmployeeById(id);
                return new ServiceResponse<EmployeeDTO>
                {
                    Object = data
                };
            });
        }






        //[HttpPut]
        //[Route("Update/{id}")]
        //public async Task<IServiceResponse<bool>> UpdateUser(int id, UserDTO region)
        //{
        //    return await HandleApiOperationAsync(async () => {
        //        await _userService.UpdateUser(id, region);

        //        return new ServiceResponse<bool>(true);
        //    });
        //}


        //[HttpDelete]
        //[Route("Delete/{id}")]
        //public async Task<IServiceResponse<bool>> DeleteUser(int id)
        //{
        //    return await HandleApiOperationAsync(async () => {
        //        await _userService.RemoveUser(id);

        //        return new ServiceResponse<bool>(true);
        //    });
        //}

    }
}