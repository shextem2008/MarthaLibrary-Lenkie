using Contracts.Dto;
using Contracts.Exceptions;
using Domain.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using StakeHoldersWebApi.Models.IdentityModels;
using System.Security.Claims;
using Domain.Context;

namespace Services
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAll();
        Task<ApplicationUser> Add(UserDTO userDTO);
        Task<UserDTO> GetUserById(int id);
        Task<UserDTO?> GetProfile(string username);
        Task<UserDTO> GetUserByEmail(string email);
        Task<bool> UpdateUser(int id, UserDTO userdata);


    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private ApplicationDbContext _context;
        public UserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager
                                ,ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var datalist = from usr in await _unitOfWork.userRepository.GetList()
                           select new UserDTO
                           {
                               Id = usr.Id,
                               FirstName = usr.FirstName,
                               Email = usr.Email,
                           };
            return datalist.ToList();
        }

        public async Task<ApplicationUser> Add(UserDTO userDTO)
        {
            var data = new ApplicationUser()
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Id = userDTO.Id,
            };

            var savedata = _unitOfWork.userRepository.CreateAsync(data);

            if (!await _unitOfWork.SaveChangesAsync())
            {
                throw new LMEGenericException("Not successfully");
                
            }

            return data;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var data = await _unitOfWork.userRepository.FindSingleAsync(x => x.Id == id);

            if (data is null)
            {
                throw new LMEGenericException(ErrorConstants.USER_ACCOUNT_NOT_EXIST);
            }
            var userdata = _userManager.FindByIdAsync(id.ToString());
            return new UserDTO
            {
                Id = userdata.Id,
            };
        }

        public async Task<UserDTO?> GetProfile(string username)
        {
            var userdata = await _userManager.FindByNameAsync(username);
            return userdata is null ? null : new UserDTO
            {
                Id = userdata.Id
            };
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            var data = await _unitOfWork.userRepository.FindSingleAsync(x => x.Email == email);

            if (data is null)
            {
                throw new LMEGenericException(ErrorConstants.USER_ACCOUNT_NOT_EXIST);
            }

            var userdata = await _userManager.FindByEmailAsync(email);
            var rolesList = await _userManager.GetRolesAsync(userdata).ConfigureAwait(false);

            IList<Claim> roleclaimdata = new List<Claim>();
            IList<RoleDto> RolesList = new List<RoleDto>();

            

            foreach (var item  in rolesList)
            {
                var role = await _roleManager.FindByNameAsync(rolesList.First());

                RoleDto rol = new RoleDto() { Id = role.Id, Name = role.Name };
                RolesList.Add(rol);

                roleclaimdata = await _roleManager.GetClaimsAsync(role);
            }

           


            return new UserDTO
            {
                Id = userdata.Id,
                AccessFailedCount = userdata.AccessFailedCount,
                AccountConfirmationCode = userdata.AccountConfirmationCode,
                Address = userdata.Address,
                CreationTime = userdata.CreationTime,
                DateOfBirth = userdata.DateOfBirth,
                DeletionTime = userdata.DeletionTime,
                DeviceToken = userdata.DeviceToken,
                DeviceType = userdata.DeviceType,
                Email = userdata.Email,
                EmailConfirmed = userdata.EmailConfirmed,
                FirstName = userdata.FirstName,
                LastName = userdata.LastName,
                Gender = userdata.Gender,
                IdentificationId = userdata.IdentificationId,
                IsDeleted = userdata.IsDeleted,
                IsFirstTimeLogin = userdata.IsFirstTimeLogin,
                LockoutEnabled = userdata.LockoutEnabled,
                MiddleName = userdata.MiddleName,
                NextOfKinName = userdata.NextOfKinName,
                NextOfKinPhone = userdata.NextOfKinPhone,
                OptionalPhoneNumber = userdata.OptionalPhoneNumber,
                OTP = userdata.OTP,
                PasswordHash = userdata.PasswordHash,
                PhoneNumber = userdata.PhoneNumber,
                PhoneNumberConfirmed = userdata.PhoneNumberConfirmed,
                ReferralCode = userdata.ReferralCode,
                UserName = userdata.UserName,
                Referrer = userdata.Referrer,
                RefreshToken = userdata.RefreshToken,
                Title = userdata.Title,
                UserPhotoId = userdata.ImageFileId,
                UserType = userdata.UserType,
                WalletId = userdata.WalletId,
                TenantId = userdata.TenantId,
                LocationId = userdata.LocationId,
                UserRoles = RolesList,
                RoleClaims = roleclaimdata,
            };
        }

        public async Task<bool> UpdateUser(int id, UserDTO userdata)
        {
            //var hh = _context.ApplicationUser.FindAsync(id);
            //hh.Result.RefreshToken = userdata.RefreshToken;

            //var gg = _context.ApplicationUser.Update(hh.Result);
            //await _unitOfWork.SaveChangesAsync();

            //return gg.Entity.Id == 0 ? false : true;

            var datas = _unitOfWork.userRepository.GetList().Result.Where(x => x.Id == id).FirstOrDefault();

            if (datas?.Id == 0)
            {
                throw new LMEGenericException(ErrorConstants.USER_ACCOUNT_NOT_EXIST);
            };


            datas.IdentificationId = datas.IdentificationId;
            datas.WalletId = datas.WalletId;
            datas.LocationId = datas.LocationId;
            datas.TenantId = datas.TenantId;

            datas.RefreshToken = userdata.RefreshToken;

            //datas.AccessFailedCount = userdata.AccessFailedCount == 0 && !bool.Equals(datas.AccessFailedCount, userdata.AccessFailedCount) ? userdata.AccessFailedCount : datas.AccessFailedCount;
            //datas.Address = userdata.Address == string.Empty && !string.Equals(datas.Address, userdata.Address) ? userdata.Address : datas.Address;
            //datas.DateOfBirth = userdata.DateOfBirth ;
            //datas.DeviceToken = userdata.DeviceToken;
            //datas.DeviceType = userdata.DeviceType;
            //datas.FirstName = userdata.FirstName == string.Empty && !string.Equals(datas.FirstName, userdata.FirstName) ? userdata.FirstName : datas.FirstName;
            //datas.LastName = userdata.LastName;
            //datas.Gender = userdata.Gender;
            //datas.IsFirstTimeLogin = userdata.IsFirstTimeLogin;
            //datas.LockoutEnabled = userdata.LockoutEnabled;
            //datas.MiddleName = userdata.MiddleName;
            //datas.NextOfKinName = userdata.NextOfKinName;
            //datas.NextOfKinPhone = userdata.NextOfKinPhone;
            //datas.OptionalPhoneNumber = userdata.OptionalPhoneNumber;
            //datas.PhoneNumber = userdata.PhoneNumber;
            //datas.PhoneNumberConfirmed = userdata.PhoneNumberConfirmed;          
            //datas.Title = userdata.Title;

            _unitOfWork.userRepository.Update(datas);
            var rst = await _unitOfWork.SaveChangesAsync();

            return rst ? true : false;
        }

        
    }


}
