using Contracts.Dto;
using Contracts.Exceptions;
using Domain.UnitOfWork;
using Contracts.Entities;
using IPagedList;
using Contracts.Utils;
using Contracts.Entities.Enums;

namespace Services
{
    public interface IBookCheckService
    {
        Task<IPagedList<BookCheckDTO>> GetCheckingList(int pageNumber, int pageSize, string query);
        Task<BookCheckDTO> GetCheckingListByStatus(int status);
        Task Checkout(BookCheckDTO bookCheckDTO);
        Task Checkin(int id,BookCheckDTO bookCheckDTO);
        Task Reservebook(BookCheckDTO bookCheckDTO);
    }

    public class BookCheckService : IBookCheckService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceHelper _serviceHelper;
        public BookCheckService(IUnitOfWork unitOfWork, IServiceHelper serviceHelper)
        {
            _unitOfWork = unitOfWork;
            _serviceHelper = serviceHelper;

        }

        public async Task<IPagedList<BookCheckDTO>> GetCheckingList(int pageNumber, int pageSize, string query)
        {      
                var vlibbooks =
                (
                 from mdl in await _unitOfWork.checkOutRepository.GetList()
                 select new BookCheckDTO
                 {
                     Id = mdl.Id,

                 }).ToList();

                return vlibbooks.ToPagedList(pageNumber, pageSize);
          
        }


        public async Task<BookCheckDTO> GetCheckingListByStatus(int status)
        {
            var data = await _unitOfWork.checkOutRepository.FindSingleAsync( x => x.Status == Status.Reserve);

            if (data is null)
            {
                throw new LMEGenericException(ErrorConstants.LBOOKINFO_NOT_EXIST);
            }

            return new BookCheckDTO
            {
                CardId = data.CardId,
            };
        }

        public async Task Checkout(BookCheckDTO bookCheckDTO)
        {
            //checked if book exist

            var savedata = _unitOfWork.checkOutRepository.CreateAsync(new CheckOut
            {
                Since = bookCheckDTO.Since,
            });

            if (!await _unitOfWork.SaveChangesAsync())
            {
                throw new LMEGenericException("Not successfully");
            }
        }

        public async Task Checkin(int id, BookCheckDTO bookCheckDTO)
        {
            var lbookdata = _unitOfWork.checkOutRepository.GetList().Result.Where(x => x.Id == id).FirstOrDefault();

            if (lbookdata?.Id != 0)
            {
                throw new LMEGenericException(ErrorConstants.LBOOKINFO_NOT_EXIST);
            }

            lbookdata.Since = bookCheckDTO.Since;

            await _unitOfWork.SaveChangesAsync();        }
        public async Task Reservebook(BookCheckDTO bookCheckDTO)
        {
            //checked if book exist
            //checked if book has been borrow and return date book will  be returned

            var savedata = _unitOfWork.checkOutRepository.CreateAsync(new CheckOut
            {
                Since = bookCheckDTO.Since,
            });

            if (!await _unitOfWork.SaveChangesAsync())
            {
                throw new LMEGenericException("Not successfully");
            }
        }


    }
}
