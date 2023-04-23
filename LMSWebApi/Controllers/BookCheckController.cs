using Contracts.Collections;
using Contracts.Dto;
using Contracts.Entities.Enums;
using Contracts.Extensions;
using IPagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using static Contracts.Utils.CoreConstants;

namespace LMSWebApi.Controllers
{
    [Authorize()]
    public class BookCheckController : BaseController
    {
        private readonly IBookCheckService _bookCheckService;
        public BookCheckController(IBookCheckService bookCheckService)
        {
            _bookCheckService = bookCheckService;
        }

        
        [Route("GetAll")]
        [HttpGet]
        public async Task<IServiceResponse<IList<BookCheckDTO>>> GetAll()
        {
            return await HandleApiOperationAsync(async () =>
            {
                var datalist = await GetCheckingList(1, WebConstants.DefaultPageSize, null);

                return new ServiceResponse<IList<BookCheckDTO>>
                {
                    Object = datalist.Object.ToList()
                };
            });
        }

        [HttpGet]
        [Route("GetBookChecks/{pageNumber}/{pageSize}/{search}")]
        public async Task<IServiceResponse<IPagedList<BookCheckDTO>>> GetCheckingList(int pageNumber = 1, int pageSize = WebConstants.DefaultPageSize, string search = null)
        {
            return await HandleApiOperationAsync(async () =>
            {
                var libdata = await _bookCheckService.GetCheckingList(pageNumber, pageSize, search);

                return new ServiceResponse<IPagedList<BookCheckDTO>>
                {
                    Object = libdata
                };
            });
        }

        [HttpGet]
        [Route("Get/{status}")]
        public async Task<IServiceResponse<List<BookCheckDTO>>> GetBookCheckById(Status status)
        {
            return await HandleApiOperationAsync(async () => {
                var data = await _bookCheckService.GetCheckingListByStatus(status);

                return new ServiceResponse<List<BookCheckDTO>>
                {
                    Object = data
                };
            });
        }

        [Route("checkout/{id}")]
        [HttpPost]
        public async Task<IServiceResponse<bool>> Checkout(int id, BookCheckDTO BookCheckDTO)
        {
            return await HandleApiOperationAsync(async () =>
            {
                await _bookCheckService.Checkout(id,BookCheckDTO);
                return new ServiceResponse<bool>(true);
            });
        }
        
        [Route("checkin/{id}")]
        [HttpPut]
        public async Task<IServiceResponse<bool>> Checkin(int id, BookCheckDTO BookCheckDTO)
        {
            return await HandleApiOperationAsync(async () =>
            {
                await _bookCheckService.Checkin(id, BookCheckDTO);

                return new ServiceResponse<bool>(true);
            });
        }


        [Route("reservebook")]
        [HttpPost]
        public async Task<IServiceResponse<bool>> Reservebook(BookCheckDTO BookCheckDTO)
        {
            return await HandleApiOperationAsync(async () =>
            {
                await _bookCheckService.Reservebook(BookCheckDTO);
                return new ServiceResponse<bool>(true);
            });
        }


    }
}