using Contracts.Collections;
using Contracts.Dto;
using Contracts.Extensions;
using IPagedList;
using Microsoft.AspNetCore.Mvc;
using Services;
using static Contracts.Utils.CoreConstants;

namespace LMSWebApi.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }


        [Route("GetAll")]
        [HttpGet]
        public async Task<IServiceResponse<IList<ClientDTO>>> GetAll()
        {
            return await HandleApiOperationAsync(async () =>
            {
                var datalist = await GetClients(1, WebConstants.DefaultPageSize, null);

                return new ServiceResponse<IList<ClientDTO>>
                {
                    Object = datalist.Object.ToList()
                };
            });
        }

        [HttpGet]
        [Route("GetClients/{pageNumber}/{pageSize}/{search}")]
        public async Task<IServiceResponse<IPagedList<ClientDTO>>> GetClients(int pageNumber = 1, int pageSize = WebConstants.DefaultPageSize, string search = null)
        {
            return await HandleApiOperationAsync(async () =>
            {
                var employees = await _clientService.GetClients(pageNumber, pageSize, search);

                return new ServiceResponse<IPagedList<ClientDTO>>
                {
                    Object = employees
                };
            });
        }


        [Route("Add")]
        [HttpPost]
        public async Task<IServiceResponse<ClientDTO>> Add(ClientDTO clientDTO)
        {
            return await HandleApiOperationAsync(async () =>
            {
                var data = await _clientService.Add(clientDTO);
                return new ServiceResponse<ClientDTO>
                {
                    Object = data,
                };
            });
        }


        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IServiceResponse<ClientDTO>> GetClientById(int id)
        {
            return await HandleApiOperationAsync(async () => {
                var data = await _clientService.GetClientById(id);

                return new ServiceResponse<ClientDTO>
                {
                    Object = data
                };
            });
        }


        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IServiceResponse<bool>> UpdateCient(int id, ClientDTO clientDTO)
        { 
            return await HandleApiOperationAsync(async () =>
            {
                await _clientService.UpdateClient(id, clientDTO);

                return new ServiceResponse<bool>(true);
            });
        }




    }
}