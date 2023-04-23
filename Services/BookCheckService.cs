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
        Task<List<BookCheckDTO>> GetCheckingListByStatus(Status status);
        Task Checkout(int id, BookCheckDTO bookCheckDTO);
        Task Checkin(int id,BookCheckDTO bookCheckDTO);
        Task Reservebook(ReserveRequest bookCheckDTO);
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
                     LibraryBookId = mdl.LibraryBookId,
                     CardId = mdl.CardId,
                     Status = mdl.Status,                 
                     Since = mdl.Since,
                     Until = mdl.Until,
                     CheckIn = mdl.CheckIn,
                     Checkout = mdl.Checkout,
                 }).ToList();

                return vlibbooks.ToPagedList(pageNumber, pageSize);         
        }


        public async Task<List<BookCheckDTO>> GetCheckingListByStatus(Status status)
        {
            var vlibbooks =
               (from mdl in await _unitOfWork.checkOutRepository.GetList(x => x.Status == status)
                select new BookCheckDTO
                {
                    Id = mdl.Id,
                    LibraryBookId = mdl.LibraryBookId,
                    CardId = mdl.CardId,
                    Status = mdl.Status,
                }).ToList();

            return vlibbooks.ToList();
        }

        public async Task Checkout(int id, BookCheckDTO bookCheckDTO)
        {
            var lbookcheck = _unitOfWork.checkOutRepository.FindSingleAsync(x => x.Id == id).Result;

            if (lbookcheck == null)
            {
                throw new LMEGenericException(ErrorConstants.LBOOKINFO_NOT_EXIST);
            }

            lbookcheck.Checkout = bookCheckDTO.Checkout;

            //make the book status not available after its been checkout by a customer
            var lbookdata = _unitOfWork.libraryBookRepository.FindSingleAsync(x => x.Id == id).Result;
            lbookdata.Status = Status.NotAvailable;

            if (!await _unitOfWork.SaveChangesAsync())
            {
                throw new LMEGenericException("update not successfully");
            }
        }

        public async Task Checkin(int id, BookCheckDTO bookCheckDTO)
        {
            var lbookcheck = _unitOfWork.checkOutRepository.FindSingleAsync(x => x.Id == id).Result;

            if (lbookcheck == null)
            {
                throw new LMEGenericException(ErrorConstants.LBOOKINFO_NOT_EXIST);
            }

            lbookcheck.CheckIn = bookCheckDTO.CheckIn;

            //make the book status to be available after its been checkin by a customer
            var lbookdata = _unitOfWork.libraryBookRepository.FindSingleAsync(x => x.Id == id).Result;
            lbookdata.Status = Status.Available;


            if (!await _unitOfWork.SaveChangesAsync())
            {
                throw new LMEGenericException("update not successfully");
            }
        }
        public async Task Reservebook(ReserveRequest bookCheckDTO)
        {
            //checked if book is exist in the library
            var lbookdata = _unitOfWork.libraryBookRepository.FindSingleAsync(x => x.Id == bookCheckDTO.LibraryBookId).Result;

            if (lbookdata == null){ throw new LMEGenericException("This Book is not available in our library");}

            //checked if book has been borrow or reserved by a customer and get the returned date of the book to the library.
            var lbookCheck = _unitOfWork.checkOutRepository
                            .FindSingleAsync(x => x.LibraryBookId == bookCheckDTO.LibraryBookId).Result;
           
            
            if (lbookCheck == null)
            {
                var savedata = _unitOfWork.checkOutRepository.CreateAsync(new CheckOut
                {
                    LibraryBookId = bookCheckDTO.LibraryBookId,
                    CardId = 1,
                    Status = bookCheckDTO.Status,
                    ReserveUntil = DateTime.Now.AddDays(1),
                    Since = bookCheckDTO.Since,
                    Until = bookCheckDTO.Until,
                });

                //make the book status not available after its been checkout by a customer              
                lbookdata.Status = Status.NotAvailable;
            }
            else
            {
                if (lbookCheck?.Id != 0 || lbookCheck?.Id != null)
                {
                    throw new LMEGenericException("this Book was " + lbookCheck?.Status.ToString() + " by a customer and will be returned on this date " + lbookCheck?.Until + "");
                }
            };

    

            if (!await _unitOfWork.SaveChangesAsync())
            {
                throw new LMEGenericException("Not saved successfully");
            }
        }


    }
}
