using Contracts.Dto;
using Contracts.Exceptions;
using Domain.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using StakeHoldersWebApi.Models.IdentityModels;
using Contracts.Entities;
using Domain.Context;
using Contracts.Entities.Enums;
using Contracts.Utils;

namespace Services
{
    public interface IVendorService
    {
        Task<List<VendorDTO>> GetAll();
        Task<VendorDTO> Add(VendorDTO vendorDTO);
        Task<VendorDTO> GetVendorById(int id);
        Task<bool> UpdateVendor(int id, VendorDTO vendorDTO);

    }

    public class VendorService : IVendorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IServiceHelper _serviceHelper;
        private readonly IWalletService _walletService;

        private ApplicationDbContext _context;
        public VendorService(IUnitOfWork unitOfWork, IServiceHelper serviceHelper, UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager, ApplicationDbContext context, IWalletService walletService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _serviceHelper = serviceHelper;
            _walletService = walletService;
        }

        public async Task<List<VendorDTO>> GetAll()
        {
            var datalist = from mdl in await _unitOfWork.vendorRepository.GetList()
                           select new VendorDTO
                           {
                               Id = mdl.Id,
                               Email = mdl.User?.Email,
                           };
            return datalist.ToList();
        }

        public async Task<VendorDTO> Add(VendorDTO vendorDTO)
        {

            if (vendorDTO == null) { throw new LMEGenericException("invalid parameter");}

            var vend = new VendorDTO();

            var _repoUser = await _userManager.FindByEmailAsync(vendorDTO.Email);
            if ( _repoUser != null)
            {
                throw new LMEGenericException(ErrorConstants.VENDOR_EXIST);
            }

            var vendor = new Vendor
            {
                VendorCode = vendorDTO.VendorCode,
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
                    TenantId = vendorDTO.TenantId,
                };
                var walletresp = await _walletService.Add(wallet);

                var imgcod = "";
                // insert Image unique key for userPhoto gallery
                if (vendorDTO.VendorPhoto == null) 
                {
                    imgcod = CommonHelper.GenereateRandonAlphaNumeric();
                    var img = new ImageFile() { TenantId = (int)vendorDTO.TenantId, FileName = "ByPassImage", Code = imgcod, UploadType = UploadType.Image };
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
                    FirstName = vendorDTO.FirstName,
                    LastName = vendorDTO.LastName,
                    MiddleName = vendorDTO.MiddleName,
                    Gender = vendorDTO.Gender,
                    Email = vendorDTO.Email,
                    PhoneNumber = vendorDTO?.PhoneNumber,
                    Address = vendorDTO.VendorAddress,
                    NextOfKinName = vendorDTO.NextOfKin,
                    NextOfKinPhone = vendorDTO.NextOfKinPhone,
                    EmailConfirmed = vendorDTO.EmailConfirmed,
                    PhoneNumberConfirmed = vendorDTO.PhoneNumberConfirmed,
                    UserName = vendorDTO.UserName,
                    ReferralCode = CommonHelper.GenereateRandonAlphaNumeric(),
                    TenantId = vendorDTO.TenantId,
                    LocationId = vendorDTO.Location,
                    UserType = UserType.Customer,
                    IsFirstTimeLogin = true,
                    WalletId = walletresp.Id,
                    ImageFileId = imgcod,
                    IdentificationId =  1 
      
                };

                var creationStatus = await _userManager.CreateAsync(user, "123456");
                if (creationStatus.Succeeded)
                {
                   
                    await _userManager.AddToRoleAsync(user, vendorDTO.RoleName == null ? CoreConstants.Roles.Customer : vendorDTO.RoleName);
                    vend.UserId = user.Id;  

                    ctx.Add(vendor);
                    await _unitOfWork.SaveChangesAsync();

                    //await SendAccountEmail(user);
                }

                vend = new VendorDTO() { Id = vendor.Id, PhoneNumber = user.PhoneNumber  , UserId = user.Id, WalletId = user.WalletId,IsActive = vendor.IsActive, Email=user.Email,FirstName = user.FirstName , Location = user.LocationId};
            }

           return vend;
        }

        public async Task<VendorDTO> GetVendorById(int id)
        {
            var data = await _unitOfWork.vendorRepository.FindSingleAsync(x => x.Id == id);

            if (data is null)
            {
                throw new LMEGenericException(ErrorConstants.USER_ACCOUNT_NOT_EXIST);
            }
            var userdata = _userManager.FindByIdAsync(id.ToString());
            return new VendorDTO
            {
                Id = userdata.Id,
            };
        }

        public async Task<bool> UpdateVendor(int id, VendorDTO vendorDTO)
        {
            var Vendor = _unitOfWork.vendorRepository.GetList().Result.Where(x => x.UserId == id).FirstOrDefault();

            if (Vendor?.UserId != 0)
            {
                throw new LMEGenericException(ErrorConstants.USER_ACCOUNT_NOT_EXIST);
            }

            Vendor.VendorCode = vendorDTO.VendorCode;
            Vendor.User.Address = vendorDTO.VendorAddress;
            Vendor.Location = vendorDTO.Location;
            Vendor.City = vendorDTO.City;
            Vendor.State = vendorDTO.State;
            Vendor.Country = vendorDTO.Country;
            Vendor.User.PhoneNumber = vendorDTO.PhoneNumber;
            Vendor.Fax = vendorDTO.VendorFax;
            Vendor.User.FirstName = vendorDTO.FirstName;
            Vendor.User.LastName = vendorDTO.LastName;
            Vendor.User.MiddleName = vendorDTO.MiddleName;
            Vendor.Salutation = vendorDTO.VendorSalutation;
            Vendor.User.NextOfKinName = vendorDTO.NextOfKin;
            Vendor.User.NextOfKinPhone = vendorDTO.NextOfKinPhone; 
            Vendor.OtpIsUsed = vendorDTO.OtpIsUsed;
            Vendor.DeviceType = vendorDTO.DeviceType;
            Vendor.User.IdentificationId = vendorDTO.IdentificationId;
            Vendor.User.Gender = vendorDTO.Gender;
            Vendor.User.DateOfBirth = vendorDTO.DateOfBirth.ToString();
            Vendor.VendorAddress = vendorDTO.VendorAddress;
            Vendor.LGA = vendorDTO.LGA;
            Vendor.Landmark = vendorDTO.Landmark;

           var rst = await _unitOfWork.SaveChangesAsync();

            return rst.Equals(true);
        }



    }


}
