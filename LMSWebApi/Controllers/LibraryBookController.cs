using Contracts.Collections;
using Contracts.Dto;
using Contracts.Extensions;
using IPagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using static Contracts.Utils.CoreConstants;

namespace LMSWebApi.Controllers
{
    [Authorize()]
    public class LibraryBookController : BaseController
    {
        private readonly ILibraryBookService _libraryBookService;
        public LibraryBookController(ILibraryBookService libraryBookService)
        {
            _libraryBookService = libraryBookService;
        }


        [Route("GetAll")]
        [HttpGet]
        public async Task<IServiceResponse<IList<LibraryBookDTO>>> GetAll()
        {
            return await HandleApiOperationAsync(async () =>
            {
                var datalist = await GetLibraryBooks(1, WebConstants.DefaultPageSize, null);

                return new ServiceResponse<IList<LibraryBookDTO>>
                {
                    Object = datalist.Object.ToList()
                };
            });
        }

        [HttpGet]
        [Route("GetLibraryBooks/{pageNumber}/{pageSize}/{search}")]
        public async Task<IServiceResponse<IPagedList<LibraryBookDTO>>> GetLibraryBooks(int pageNumber = 1, int pageSize = WebConstants.DefaultPageSize, string search = null)
        {
            return await HandleApiOperationAsync(async () =>
            {
                var libdata = await _libraryBookService.GetLibraryBooks(pageNumber, pageSize, search);

                return new ServiceResponse<IPagedList<LibraryBookDTO>>
                {
                    Object = libdata
                };
            });
        }


        [Route("Add")]
        [HttpPost]
        public async Task<IServiceResponse<bool>> Add(LibraryBookDTO libraryBookDTO)
        {
            return await HandleApiOperationAsync(async () =>
            {
                await _libraryBookService.Add(libraryBookDTO);
                return new ServiceResponse<bool>(true);
            });
        }


        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IServiceResponse<LibraryBookDTO>> GetLibraryBookById(int id)
        {
            return await HandleApiOperationAsync(async () => {
                var data = await _libraryBookService.GetLibraryBookById(id);

                return new ServiceResponse<LibraryBookDTO>
                {
                    Object = data
                };
            });
        }

        [HttpGet]
        [Route("Get/{name}")]
        public async Task<IServiceResponse<LibraryBookDTO>> GetLibraryBookByName(string name)
        {
            return await HandleApiOperationAsync(async () => {
                var data = await _libraryBookService.GetLibraryBookByName(name);

                return new ServiceResponse<LibraryBookDTO>
                {
                    Object = data
                };
            });
        }


        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IServiceResponse<bool>> UpdateCient(int id, LibraryBookDTO libraryBookDTO)
        { 
            return await HandleApiOperationAsync(async () =>
            {
                await _libraryBookService.UpdateLibraryBook(id, libraryBookDTO);

                return new ServiceResponse<bool>(true);
            });
        }




    }
}