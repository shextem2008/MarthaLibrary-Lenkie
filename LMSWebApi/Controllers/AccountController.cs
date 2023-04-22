using Contracts.Collections;
using Contracts.Dto;
using Contracts.Extensions;
using Contracts.Utils.Auth;
using IPagedList;
using Microsoft.AspNetCore.Mvc;
using Services;
using static Contracts.Utils.CoreConstants;

namespace LMSWebApi.Controllers
{
    public class AccountController : BaseController
    {

        private readonly JwtTokenHandler _jwtTokenHandler;

        public AccountController(JwtTokenHandler jwtTokenHandler)
        {
            _jwtTokenHandler = jwtTokenHandler;
        }

        [HttpPost]
        [Route("Authenticate")]
        public ActionResult<AuthenticateResponse?> Authenticate([FromBody] AuthenticateRequest authenticateRequest)
        {
            var authenticationResponse = _jwtTokenHandler.GeneraJwtToken(authenticateRequest);
            if (authenticateRequest == null) return Unauthorized();
            return authenticationResponse;
        }




    }
}