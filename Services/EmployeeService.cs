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
    public interface IEmployeeService
    {
        Task<IPagedList<EmployeeDTO>> GetEmployees(int pageNumber, int pageSize, string query);     
        Task<EmployeeDTO> Add(EmployeeDTO employeeDTO);
        Task<EmployeeDTO> GetEmployeeById(int id);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IServiceHelper _serviceHelper;
        private readonly IWalletService _walletService;
         
        private ApplicationDbContext _context;
        public EmployeeService(IUnitOfWork unitOfWork, IServiceHelper serviceHelper, UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager, ApplicationDbContext context, IWalletService walletService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _serviceHelper = serviceHelper;
            _walletService = walletService;
        }


        public async Task<IPagedList<EmployeeDTO>> GetEmployees(int pageNumber, int pageSize, string query)
        {
            var ts = _serviceHelper.GetTenantId();
        
                var employees =
                (
                 from user in await _unitOfWork.userRepository.GetList()

                 join employee in await _unitOfWork.employeeRepository.GetList() on user?.Id equals employee.UserId
                 into vemployees 
                 from employee in vemployees.DefaultIfEmpty()

                 join location in await _unitOfWork.locationRepository.GetList() on user.LocationId equals location.Id
                 into locations
                 from location in locations.DefaultIfEmpty()

                 join departm in await _unitOfWork.departmentRepository.GetList() on employee.DepartmentId equals departm.Id
                 into departms
                 from departm in departms.DefaultIfEmpty()

                 select new EmployeeDTO
                 {
                     Id = employee.Id,
                     DateOfEmployment = employee.DateOfEmployment,
                     FirstName = user.FirstName,
                     LastName = user.LastName,
                     MiddleName = user.MiddleName,
                 
                     Email = user.Email,
                     PhoneNumber = user.PhoneNumber,
                     Address = user.Address,
                   
                     NextOfKin = user.NextOfKinName,
                     NextOfKinPhone = user.NextOfKinPhone,
                     DepartmentName = departm.Name,
                     DepartmentId = departm.Id,
                     LocationId = location.Id,
                     locationName = location.Name,
                     EmployeeCode = employee.EmployeeCode,

                     UserId = user.Id,
                     IsActive = employee.IsActive,

                 }).ToList();

                return employees.ToPagedList(pageNumber, pageSize);
         
        }

        public async Task<List<EmployeeDTO>> GetAll()
        {
            var ts = _serviceHelper.GetTenantId();
            try
            {
                var employees =
                (
                 from user in await _unitOfWork.userRepository.GetList()

                 join employee in await _unitOfWork.employeeRepository.GetList() on user?.Id equals employee.UserId
                 into vemployees
                 from employee in vemployees.DefaultIfEmpty()

                 join location in await _unitOfWork.locationRepository.GetList() on user.LocationId equals location.Id
                 into locations
                 from location in locations.DefaultIfEmpty()

                 join departm in await _unitOfWork.departmentRepository.GetList() on employee.DepartmentId equals departm.Id
                 into departms
                 from departm in departms.DefaultIfEmpty()

                 select new EmployeeDTO
                 {
                     Id = employee.Id,
                     DateOfEmployment = employee.DateOfEmployment,
                     FirstName = user.FirstName,
                     LastName = user.LastName,
                     MiddleName = user.MiddleName,
                     //Gender = employee.User.Gender,
                     Email = user.Email,
                     PhoneNumber = user.PhoneNumber,
                     Address = user.Address,
                     //EmployeePhoto = employee.User.,
                     NextOfKin = user.NextOfKinName,
                     NextOfKinPhone = user.NextOfKinPhone,
                     DepartmentName = departm.Name,
                     DepartmentId = departm.Id,
                     LocationId = location.Id,
                     locationName = location.Name,
                     EmployeeCode = employee.EmployeeCode,

                     UserId = user.Id,
                     IsActive = employee.IsActive,

                 }).ToList();

                return employees.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<EmployeeDTO> Add(EmployeeDTO employeeDTO)
        {

            if (employeeDTO == null) { throw new LMEGenericException("invalid parameter");}

            employeeDTO.EmployeeCode = employeeDTO.EmployeeCode?.Trim();
            var emp = new EmployeeDTO();

            var _repoUser = await _userManager.FindByEmailAsync(employeeDTO.Email);
            if ( _repoUser != null)
            {
                throw new LMEGenericException(ErrorConstants.EMPLOYEE_EXIST);
            }

            var employee = new Employee
            {
                EmployeeCode = employeeDTO.EmployeeCode,
                DateOfEmployment = employeeDTO.DateOfEmployment,
                DepartmentId = employeeDTO.DepartmentId == 0 ? 1 : employeeDTO.DepartmentId,
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
                    TenantId = employeeDTO.TenantId,
                };
                var walletresp = await _walletService.Add(wallet);

                var imgcod = "";
                // insert Image unique key for userPhoto gallery
                if (employeeDTO.EmployeePhoto == null) 
                {
                    imgcod = CommonHelper.GenereateRandonAlphaNumeric();
                    var img = new ImageFile() { TenantId = (int)employeeDTO.TenantId, FileName = "ByPassImage", Code = imgcod, UploadType = UploadType.Image };
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
                    FirstName = employeeDTO.FirstName,
                    LastName = employeeDTO.LastName,
                    MiddleName = employeeDTO.MiddleName,
                    Gender = employeeDTO.Gender,
                    Email = employeeDTO.Email,
                    PhoneNumber = CommonHelper.ToNigeriaMobile(employeeDTO?.PhoneNumber),
                    Address = employeeDTO.Address,
                    NextOfKinName = employeeDTO.NextOfKin,
                    NextOfKinPhone = employeeDTO.NextOfKinPhone,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    UserName = employeeDTO.Email,
                    ReferralCode = CommonHelper.GenereateRandonAlphaNumeric(),
                    TenantId = employeeDTO.TenantId,
                    LocationId = employeeDTO.LocationId,
                    UserType = UserType.Employee,
                    IsFirstTimeLogin = true,
                    WalletId = walletresp.Id,
                    ImageFileId = imgcod,
                    IdentificationId = employeeDTO.DepartmentId == 0 ? 1 : employeeDTO.DepartmentId,
      
                };

                var creationStatus = await _userManager.CreateAsync(user, "123456");
                if (creationStatus.Succeeded)
                {
                   
                    await _userManager.AddToRoleAsync(user, employeeDTO.RoleName == null ? CoreConstants.Roles.Employee : employeeDTO.RoleName);
                    employee.UserId = user.Id;  

                    ctx.Add(employee);
                    await _unitOfWork.SaveChangesAsync();

                    //await SendAccountEmail(user);
                }

                 emp = new EmployeeDTO() { Id = employee.Id, PhoneNumber = user.PhoneNumber  , UserId = user.Id, WalletId = user.WalletId,IsActive = employee.IsActive, Email=user.Email,FirstName = user.FirstName , LocationId = user.LocationId};
            }

           return emp;
        }



        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            var data = await _unitOfWork.employeeRepository.FindSingleAsync(x => x.Id == id);

            if (data is null)
            {
                throw new LMEGenericException(ErrorConstants.USER_ACCOUNT_NOT_EXIST);
            }
            var userdata = _userManager.FindByIdAsync(id.ToString());
            return new EmployeeDTO
            {
                Id = userdata.Id,
            };
        }





    }


}
