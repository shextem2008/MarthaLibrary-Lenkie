using Contracts.Collections;
using Contracts.Dto;
using Contracts.Extensions;
using Microsoft.AspNetCore.Mvc;
using Services;
using StakeHoldersWebApi.Models.IdentityModels;

namespace LMSWebApi.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IServiceResponse <List<UserDTO>>> GetAll()
        {
           return await HandleApiOperationAsync(async () =>
           {
               var datalist = await _userService.GetAll();
               return new ServiceResponse<List<UserDTO>>
               {
                   Object = datalist,
               };
           });
        }


        [Route("Add")]
        [HttpPost]
        public async Task<IServiceResponse<ApplicationUser>> Add(UserDTO userDTO)
        {
            return await HandleApiOperationAsync(async () =>
            {
                var data = await _userService.Add(userDTO);
                return new ServiceResponse<ApplicationUser>
                {
                    Object = data,
                };
            });
        }


        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IServiceResponse<UserDTO>> GetUserById(int id)
        {
            return await HandleApiOperationAsync(async () => {
                var data = await _userService.GetUserById(id);

                return new ServiceResponse<UserDTO>
                {
                    Object = data
                };
            });
        }



        [HttpGet]
        [Route("GetProfile")]
        public async Task<IServiceResponse<UserDTO>> GetProfile()
        {
            return await HandleApiOperationAsync(async () => {

                var response = new ServiceResponse<UserDTO>();
                var profile = await _userService.GetProfile(User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value);
               
                response.Object = profile;
                return response;
            });
        }

        [HttpGet]
        [Route("GetUserByEmail/{email}")]
        public async Task<IServiceResponse<UserDTO>> GetUserByEmail(string email)
        {
            return await HandleApiOperationAsync(async () => {
                var data = await _userService.GetUserByEmail(email);

                return new ServiceResponse<UserDTO>
                { 
                    Object = data
                };
            });
        }


        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<IServiceResponse<bool>> UpdateUser(int id, UserDTO  userDTO )
        {
            return await HandleApiOperationAsync(async () =>
            {
                await _userService.UpdateUser(id, userDTO);

                return new ServiceResponse<bool>(true);
            });
        }



    }
}