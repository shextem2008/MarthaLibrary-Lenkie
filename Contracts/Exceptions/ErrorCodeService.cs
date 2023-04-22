using Contracts.Collections.Repository;
using Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Exceptions
{
    public interface IErrorCodeService
    {
        Task<ErrorCode> GetErrorByCodeAsync(string errorCode);
    }

    public class ErrorCodeService : IErrorCodeService
    {
        readonly IRepository<ErrorCode> _repository;

        public ErrorCodeService(IRepository<ErrorCode> repository)
        {
            _repository = repository;
        }

        public Task<ErrorCode> GetErrorByCodeAsync(string errorCode)
        {
            return _repository.FirstOrDefaultAsync(e => e.Code.ToLower() == errorCode.ToLower());
        }
    }
}
