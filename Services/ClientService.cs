using Contracts.Dto;
using Contracts.Exceptions;
using Domain.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using StakeHoldersWebApi.Models.IdentityModels;
using Contracts.Entities;
using Domain.Context;
using Contracts.Entities.Enums;
using Contracts.Utils;
using IPagedList;

namespace Services
{
    public interface IClientService
    {
        Task<IPagedList<ClientDTO>> GetClients(int pageNumber, int pageSize, string query);
        Task<ClientDTO> Add(ClientDTO clientDTO);
        Task<ClientDTO> GetClientById(int id);
        Task<bool> UpdateClient(int id, ClientDTO clientDTO);

    }

    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IServiceHelper _serviceHelper;
        private readonly IWalletService _walletService;

        private ApplicationDbContext _context;
        public ClientService(IUnitOfWork unitOfWork, IServiceHelper serviceHelper, UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager, ApplicationDbContext context, IWalletService walletService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _serviceHelper = serviceHelper;
            _walletService = walletService;
        }

        public async Task<IPagedList<ClientDTO>> GetClients(int pageNumber, int pageSize, string query)
        {
            var ts = _serviceHelper.GetTenantId();
     
                var vclients =
                (
                 from user in await _unitOfWork.userRepository.GetList()

                 join clients in await _unitOfWork.clientRepository.GetList() on user?.Id equals clients.UserId
                 into vclientss
                 from clients in vclientss.DefaultIfEmpty()

                 join location in await _unitOfWork.locationRepository.GetList() on user.LocationId equals location.Id
                 into locations
                 from location in locations.DefaultIfEmpty()

                  where (user.TenantId == 1 && user.UserType == UserType.Customer)
                 
                 select new ClientDTO
                 {
                     Id = clients.Id,
                     FirstName = user.FirstName,
                     LastName = user.LastName,
                     MiddleName = user.MiddleName,          
                     Email = user.Email,
                     PhoneNumber = user.PhoneNumber,
                     ClientAddress = user.Address,                    
                     NextOfKin = user.NextOfKinName,
                     NextOfKinPhone = user.NextOfKinPhone,
                     Location = location.Id,
                     ClientCode = clients.ClientCode,
                     UserId = user.Id,
                     IsActive = clients.IsActive,

                 }).ToList();

                return vclients.ToPagedList(pageNumber, pageSize);

        }



        public async Task<ClientDTO> Add(ClientDTO clientDTO)
        {

            if (clientDTO == null) { throw new LMEGenericException("invalid parameter");}

            clientDTO.ClientCode = clientDTO.ClientCode?.Trim();
            var clt = new ClientDTO();

            var _repoUser = await _userManager.FindByEmailAsync(clientDTO.Email);
            if ( _repoUser != null)
            {
                throw new LMEGenericException(ErrorConstants.CLIENT_EXIST);
            }

            var client = new Client
            {
                ClientCode = clientDTO.ClientCode,
                CreatorUserId = _serviceHelper.GetCurrentUserId(),
                IsActive = true
            };

            using (var ctx = _context)
            {
                
                var walletNumber = CommonHelper.RandomNumber(9);
 
                // insert for wallet
                var wallet = new WalletDTO
                {
                    WalletNumber = walletNumber,
                    CreatorUserId = _serviceHelper.GetCurrentUserId(),
                    Balance = 0.00M,
                    TenantId = clientDTO.TenantId,
                };
                var walletresp = await _walletService.Add(wallet);

                var imgcod = "";
                // insert Image unique key for userPhoto gallery
                if (clientDTO.ClientPhoto == null) 
                {
                    imgcod = CommonHelper.GenereateRandonAlphaNumeric();
                    var img = new ImageFile() { TenantId = (int)clientDTO.TenantId, FileName = "ByPassImage", Code = imgcod, UploadType = UploadType.Image };
                    ctx.ImageFile.Add(img);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    //update or insert list of photos and Identification for user
                    imgcod = CommonHelper.GenereateRandonAlphaNumeric();

                    //update or insert  Identification for user
                };


                // insert for User with generated walletID
                var user = new ApplicationUser
                {
                    FirstName = clientDTO.FirstName,
                    LastName = clientDTO.LastName,
                    MiddleName = clientDTO.MiddleName,
                    Gender = clientDTO.Gender,
                    Email = clientDTO.Email,
                    PhoneNumber = clientDTO?.PhoneNumber,
                    Address = clientDTO?.ClientAddress,
                    NextOfKinName = clientDTO.NextOfKin,
                    NextOfKinPhone = clientDTO.NextOfKinPhone,
                    EmailConfirmed = clientDTO.EmailConfirmed,
                    PhoneNumberConfirmed = clientDTO.PhoneNumberConfirmed,
                    UserName = clientDTO.UserName == null ? clientDTO.Email : clientDTO.UserName,
                    ReferralCode = CommonHelper.GenereateRandonAlphaNumeric(),
                    TenantId = clientDTO.TenantId,
                    LocationId = clientDTO.Location,
                    UserType = UserType.Customer,
                    IsFirstTimeLogin = true,
                    WalletId = walletresp.Id,
                    ImageFileId = imgcod,
                    IdentificationId =  1 
      
                };

                var creationStatus = await _userManager.CreateAsync(user, "123456");
                if (creationStatus.Succeeded)
                {
                   
                    await _userManager.AddToRoleAsync(user, clientDTO.RoleName == null ? CoreConstants.Roles.Customer : clientDTO.RoleName);
                    client.UserId = user.Id;  

                    ctx.Add(client);
                    await _unitOfWork.SaveChangesAsync();

                    //await SendAccountEmail(user);
                }

                clt = new ClientDTO() { Id = client.Id, PhoneNumber = user.PhoneNumber  , UserId = user.Id, WalletId = user.WalletId,IsActive = client.IsActive, Email=user.Email,FirstName = user.FirstName , Location = user.LocationId};
            }

           return clt;
        }

        public async Task<ClientDTO> GetClientById(int id)
        {
            var data = await _unitOfWork.clientRepository.FindSingleAsync(x => x.Id == id);

            if (data is null)
            {
                throw new LMEGenericException(ErrorConstants.USER_ACCOUNT_NOT_EXIST);
            }
            var userdata = _userManager.FindByIdAsync(id.ToString());
            return new ClientDTO
            {
                Id = userdata.Id,
            };
        }

        public async Task<bool> UpdateClient(int id, ClientDTO clientDTO)
        {
            var client = _unitOfWork.clientRepository.GetList().Result.Where(x => x.UserId == id).FirstOrDefault();

            if (client?.UserId != 0)
            {
                throw new LMEGenericException(ErrorConstants.USER_ACCOUNT_NOT_EXIST);
            }

            client.ClientCode = clientDTO.ClientCode;
            client.User.Address = clientDTO.ClientAddress;
            client.Location = clientDTO.Location;
            client.City = clientDTO.City;
            client.State = clientDTO.State;
            client.Country = clientDTO.Country;
            client.User.PhoneNumber = clientDTO.PhoneNumber;
            client.ClientFax = clientDTO.ClientFax;
            client.User.FirstName = clientDTO.FirstName;
            client.User.LastName = clientDTO.LastName;
            client.User.MiddleName = clientDTO.MiddleName;
            client.ClientSalutation = clientDTO.ClientSalutation;
            client.User.NextOfKinName = clientDTO.NextOfKin;
            client.User.NextOfKinPhone = clientDTO.NextOfKinPhone; 
            client.OtpIsUsed = clientDTO.OtpIsUsed;
            client.DeviceType = clientDTO.DeviceType;
            client.User.IdentificationId = clientDTO.IdentificationId;
            client.User.Gender = clientDTO.Gender;
            client.User.DateOfBirth = clientDTO.DateOfBirth.ToString();
            client.ClientAddress = clientDTO.ClientAddress;
            client.LGA = clientDTO.LGA;
            client.Landmark = clientDTO.Landmark;

           var rst = await _unitOfWork.SaveChangesAsync();

            return rst.Equals(true);
        }



    }


}
