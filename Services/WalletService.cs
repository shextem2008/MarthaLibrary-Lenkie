using Contracts.Dto;
using Contracts.Exceptions;
using Domain.UnitOfWork;
using Contracts.Entities;

namespace Services
{
    public interface IWalletService
    {
        Task<List<WalletDTO>> GetAll();
        Task<Wallet> Add(WalletDTO walletDTO);
        Task<WalletDTO> GetWalletById(int id);


    }

    public class WalletService : IWalletService 
    {
        private readonly IUnitOfWork _unitOfWork;
        public WalletService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<List<WalletDTO>> GetAll()
        {
            var datalist = from mdl in await _unitOfWork.walletRepository.GetList()
                           select new WalletDTO
                           {
                               Id = mdl.Id,
                           };
            return datalist.ToList();
        }

        public async Task<Wallet> Add(WalletDTO walletDTO)
        {
            var data = new Wallet()
            {
                AccountNo = walletDTO.AccountNo,
                Balance = walletDTO.Balance,
                CreatorUserId = walletDTO.CreatorUserId,
                Status = walletDTO.Status,
                TenantId = walletDTO.TenantId,
                WalletNumber = walletDTO.WalletNumber,
                WalletVendor = walletDTO.WalletVendor,
                WalletVirAcctID = walletDTO.WalletVirAcctID,
                OldBalance = walletDTO.OldBalance,

            };
            var savedata = _unitOfWork.walletRepository.CreateAsync(data);

            if (!await _unitOfWork.SaveChangesAsync())
            {
                throw new LMEGenericException("Not successfully");
            }

            walletDTO.Id = savedata.Id;

            return data;
        }

        public async Task<WalletDTO> GetWalletById(int id)
        {
            var data = await _unitOfWork.walletRepository.FindSingleAsync(x => x.Id == id);

            if (data is null)
            {
                throw new LMEGenericException(ErrorConstants.WalletINFO_NOT_EXIST);
            }
            return null;
        }





    }


}
