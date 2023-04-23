using Contracts.Dto;
using Contracts.Exceptions;
using Domain.UnitOfWork;
using Contracts.Entities;
using IPagedList;
using Contracts.Utils;

namespace Services
{
    public interface ILibraryBookService
    {
        Task<IPagedList<LibraryBookDTO>> GetLibraryBooks(int pageNumber, int pageSize, string query);
        Task Add(LibraryBookDTO LibraryBookDTO);
        Task<LibraryBookDTO> GetLibraryBookById(int id);
        Task<LibraryBookDTO> GetLibraryBookByName(string bookname);
        Task UpdateLibraryBook(int id, LibraryBookDTO model);
    }

    public class LibraryBookService : ILibraryBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceHelper _serviceHelper;
        public LibraryBookService(IUnitOfWork unitOfWork, IServiceHelper serviceHelper)
        {
            _unitOfWork = unitOfWork;
            _serviceHelper = serviceHelper;

        }

        public async Task<IPagedList<LibraryBookDTO>> GetLibraryBooks(int pageNumber, int pageSize, string query)
        {
         
                var vlibbooks =
                (
                 from mdl in await _unitOfWork.libraryBookRepository.GetList()

                 select new LibraryBookDTO
                 {
                     Id = mdl.Id,
                     Status = mdl.Status,
                     Cost = mdl.Cost,
                     Author = mdl.Author,
                     ImageUrl = mdl.ImageUrl,
                     DeweyIndex = mdl.DeweyIndex,
                     ISBN = mdl.ISBN,
                     LocationId = mdl.LocationId,
                     NumberOfCopies = mdl.NumberOfCopies,
                     Title = mdl.Title,
                     Year = mdl.Year,

                 }).ToList();

                return vlibbooks.ToPagedList(pageNumber, pageSize);
          
        }

        public async Task Add(LibraryBookDTO libraryBookDTO)
        {
            var savedata = _unitOfWork.libraryBookRepository.CreateAsync(new LibraryBook
            {
                Id = libraryBookDTO.Id,
                 Status = libraryBookDTO.Status,
                  Cost = libraryBookDTO.Cost,
                   Author = libraryBookDTO.Author,
                    ImageUrl = libraryBookDTO.ImageUrl,
                     DeweyIndex =  libraryBookDTO.DeweyIndex,
                      ISBN = libraryBookDTO.ISBN,
                       LocationId   = 1,
                        NumberOfCopies = libraryBookDTO.NumberOfCopies,
                         Title = libraryBookDTO.Title,
                          Year = libraryBookDTO.Year,
                        
            });

            if (!await _unitOfWork.SaveChangesAsync())
            {
                throw new LMEGenericException("Not successfully");
            }
        }

        public async Task<LibraryBookDTO> GetLibraryBookById(int id)
        {
            var data = await _unitOfWork.libraryBookRepository.FindSingleAsync(x => x.Id == id);

            if (data is null)
            {
                throw new LMEGenericException(ErrorConstants.LBOOKINFO_NOT_EXIST);
            }

            return new LibraryBookDTO
            {
                Title = data.Title,
            };
        }

        public async Task<LibraryBookDTO> GetLibraryBookByName(string bookname)
        {
            var data = await _unitOfWork.libraryBookRepository.FindSingleAsync(x => x.Title == "bookname");

            if (data is null)
            {
                throw new LMEGenericException(ErrorConstants.LBOOKINFO_NOT_EXIST);
            }

            return new LibraryBookDTO
            {
                Title = data.Title,
            };
        }


        public async Task UpdateLibraryBook(int id, LibraryBookDTO model)
        {
            var lbookdata = _unitOfWork.libraryBookRepository.GetList().Result.Where(x => x.Id == id).FirstOrDefault();

            if (lbookdata?.Id != 0)
            {
                throw new LMEGenericException(ErrorConstants.LBOOKINFO_NOT_EXIST);
            }

            lbookdata.Author = model.Author;

             await _unitOfWork.SaveChangesAsync();

        }


    }
}
