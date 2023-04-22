using Contracts.Dto;
using Contracts.Exceptions;
using Domain.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using StakeHoldersWebApi.Models.IdentityModels;
using Contracts.Entities;
using Contracts.Utils;
using Domain.Context;
using Contracts.Collections;
using Contracts.Entities.Enums;

namespace Services
{
    public interface ITenantService
    {
        Task<List<TenantDTO>> GetAll();
        Task<TenantDTO> Add(TenantDTO tenantDTO);
        Task<TenantDTO> GetTenantById(int id);


    }

    public class TenantService : ITenantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeService _employeeService;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IServiceHelper _serviceHelper;
        private ApplicationDbContext _context;
        public TenantService(IUnitOfWork unitOfWork, ApplicationDbContext context, RoleManager<ApplicationRole> roleManager,
                                IEmployeeService employeeService, IServiceHelper serviceHelper)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _employeeService = employeeService;
            _serviceHelper = serviceHelper;
            _roleManager = roleManager;

        }

        public async Task<List<TenantDTO>> GetAll()
        {
            var datalist = from mdl in await _unitOfWork.tenantRepository.GetList()
                           select new TenantDTO
                           {
                               Id = mdl.Id,
                               TenantNameA = mdl.TenantNameA,
                               Email = mdl.Email,
                           };
            return datalist.ToList();
        }

        public async Task<TenantDTO> Add(TenantDTO tenantDTO)
        {
            if (tenantDTO == null){ throw new LMEGenericException("invalid parameter"); }

            var tnt = new TenantDTO();

            using (var ctx = _context)
            {
                var valUser = ctx.Tenant.ToList().Where(x => x.Email == tenantDTO.Email).FirstOrDefault();

                if (valUser?.Id != null)
                {
                    throw new LMEGenericException(ErrorConstants.EMPLOYEE_EXIST);
                }
                var models = new Tenant
                {
                    TenantNameA = tenantDTO.TenantNameA,
                    Email = tenantDTO.Email,
                    Address = tenantDTO.Address,
                    ContactPerson = tenantDTO.ContactPerson,
                    ContactPhoneNo = tenantDTO.ContactPhoneNo,
                    IsParentCompany = tenantDTO.IsParentCompany,
                    IsActive = true,
                    Tenantdesc = tenantDTO.Tenantdesc,
                    CompanySizeRange = tenantDTO.CompanySizeRange,


                    IsTranspRecieved = tenantDTO.IsTranspRecieved,
                    SmsUrl = tenantDTO.SmsUrl,
                    HostingUrl = tenantDTO.HostingUrl,
                    TenantUrl = tenantDTO.TenantUrl,

                    IsParentDate = tenantDTO.IsParentDate,
                    ImageUrlPath = tenantDTO.ImageUrlPath,

                    MJAPIKEYPRIVATE = tenantDTO.MJAPIKEYPRIVATE,
                    MJAPIKEYPUBLIC = tenantDTO.MJAPIKEYPUBLIC,
                    MJSenderEmail = tenantDTO.MJSenderEmail,
                    MJSenderName = tenantDTO.MJSenderName,
                    DOAccessKey = tenantDTO.DOAccessKey,
                    DOSecretKey = tenantDTO.DOSecretKey,
                    bucketName = tenantDTO.bucketName,
                    DOHostUploadURL = tenantDTO.DOHostUploadURL,
                    PYPublicKey = tenantDTO.PYPublicKey,
                    PYSecretKey = tenantDTO.PYSecretKey,
                    FlPublicKey = tenantDTO.FlPublicKey,
                    FlSecretKey = tenantDTO.FlSecretKey,

                    SmsEnableSSl = tenantDTO.SmsEnableSSl,
                    SmsPort = tenantDTO.SmsPort,
                    SmsServer = tenantDTO.SmsServer,
                    SmsPassword = tenantDTO.SmsPassword,
                    SmsUserName = tenantDTO.SmsUserName,
                    SmsUseDefaultCred = tenantDTO.SmsUseDefaultCred,

                };

                ctx.Tenant.Add(models);
                await _unitOfWork.SaveChangesAsync();

              //seed setup for tenant roles, department,
              var tseed =   await SeedAllTenantEntity(models.Id);

                // insert the SuperAdmin employee for tenant  
              var employee = new EmployeeDTO()
                {
                    EmployeeCode = CommonHelper.RandomDigits(5),
                    DateOfEmployment = DateTime.Now,
                    DepartmentId = tseed.DepartmentId,
                    Email = tenantDTO.Email,
                    FirstName = tenantDTO.TenantNameA,
                    LastName = tenantDTO.TenantNameB,
                    PhoneNumber = tenantDTO.ContactPhoneNo,
                    Address = tenantDTO.Address,
                    LocationId = tseed.locationvID,
                    TenantId = models.Id,
                    IsActive = true,
                    RoleName = tseed.RoleNames?[0],
                 
                };

               await _employeeService.Add(employee);

                tnt.Id = models.Id;
                tnt.Email = models.Email;   
                tnt.IsActive = models.IsActive;
                
            }

           
            return tnt;
        }

        public async Task<TenantSeedDTO> SeedAllTenantEntity(int tid)
        {
            var tseed = new TenantSeedDTO();
            IList<string>? rolestr = new List<string>();

            #region department
            var res = _context.Department.ToList().Where(x => x.Id == tid).FirstOrDefault();
            if(res?.Id == 0) { return null; }

            var departs = new Department()
            {
               Name = Contracts.Utils.CoreConstants.DepartmentName,
               TenantId = tid,
               Description = Contracts.Utils.CoreConstants.DepartmentName
            };

            _context.Department.Add(departs);
           await _unitOfWork.SaveChangesAsync();
            tseed.DepartmentId = departs.Id;

            #endregion

            #region Role
            var systemRoles = PermissionClaimsProvider.GetSystemDefaultRoles();
            var ky = "";
           


            foreach (var systemClaims in systemRoles)
            {
                 
                var role = _roleManager.FindByNameAsync(ky).Result;
                ky = systemClaims.Key + '-' + tid;

                if (role is null)
                {
                    role = new ApplicationRole
                    {
                        Name = ky,
                        IsActive = true,
                        IsDefaultRole = true,
                        TenantId = tid,
                        RolesDescription = systemClaims.Key,
                    };

                    var r = _roleManager.CreateAsync(role).Result;
                    rolestr.Add(ky);
                }

                var oldClaims = _roleManager.GetClaimsAsync(role).Result;

                foreach (var claim in systemClaims.Value)
                {
                    if (!oldClaims.Any(x => x.Value.Equals(claim.Value)))
                    {
                        var r = _roleManager.AddClaimAsync(role, claim).Result;
                    }
                }
            }
            #endregion

            #region Region
            var rid = _context.Region.ToList().Where(x => x.TenantId == tid).FirstOrDefault()?.Id;
            if (rid != null) return null;
            var regions = new Region[]
            {
                new Region{ Name = "South-West",  TenantId = tid },
                new Region{ Name = "South-East",  TenantId = tid },
                 new Region{ Name = "South-South",  TenantId = tid },
                  new Region{ Name = "North-East",  TenantId = tid },
                   new Region{ Name = "North-West",  TenantId = tid },
                    new Region{ Name = "North-Central",  TenantId = tid }
            };

            foreach (var model in regions)
             _context.Region.Add(model);
            _context.SaveChanges();

            var rmapid = _context.Region.ToList().Where(x => x.TenantId == tid).FirstOrDefault()?.Id;
            tseed.RegionvID = rmapid;
            #endregion

            #region State


            var sid = _context.State.ToList().Where(x => x.TenantId == tid).FirstOrDefault()?.Id;
            if (sid != null) return null;
            var states = new State(){  Name = "lagos",TenantId = tid, RegionId = (int)rmapid};
                _context.State.Add(states);
            _context.SaveChanges();

            tseed.StatevID = states.Id;
            #endregion

            #region city
            var cid = _context.City.ToList().Where(x => x.TenantId == tid).FirstOrDefault()?.Id;
            if (cid != null) return null;

            var citys = new City() { Name = "Lagos-Mainland", TenantId = tid, StateId = states.Id, Latitude = 0, Longitude = 0 };
            _context.City.Add(citys);
            _context.SaveChanges();

            tseed.cityvID = citys.Id;
            #endregion

            #region location
            var lid = _context.Location.ToList().Where(x => x.TenantId == tid).FirstOrDefault()?.Id;
            if (lid != null) return null;

            var locations = new Location() { Name = "Oshodi-Isolo", TenantId = tid, CityId = citys.Id, Latitude = 0, Longitude = 0 };
            _context.Location.Add(locations);
            _context.SaveChanges();

            tseed.locationvID = locations.Id;
            #endregion

            #region Identity
            var Iid = _context.Identification.ToList().Where(x => x.TenantId == tid).FirstOrDefault()?.Id;
            if (Iid != null) return null;

            var models = new Identification() { IdentificationCode = "111111111", IdentificationType = IdentificationType.Default, TenantId = tid };
            _context.Identification.Add(models);
            _context.SaveChanges();

            tseed.IdentityvID = models.Id;
            #endregion

            #region Image

            var Imid = _context.ImageFile.ToList().Where(x => x.TenantId == tid).FirstOrDefault()?.Id;
            if (Imid != null) return null;

            var modelImgs = new ImageFile { FileName = "ByPassImage", Code = "DFTOOO1", UploadType = UploadType.Image };
            _context.ImageFile.Add(modelImgs);
            _context.SaveChanges();

            tseed.ImagevID = modelImgs.Id;

            #endregion

            tseed.RoleNames = rolestr;
            return tseed;
        }
        public async Task<TenantDTO> GetTenantById(int id)
        {
            var userdata = await _unitOfWork.tenantRepository.FindSingleAsync(x => x.Id == id);

            if (userdata is null)
            {
                throw new LMEGenericException(ErrorConstants.TENANTINFO_NOT_EXIST);
            }

            return new TenantDTO
            {
                Id = userdata.Id,
            };
        }

    



    }


}
