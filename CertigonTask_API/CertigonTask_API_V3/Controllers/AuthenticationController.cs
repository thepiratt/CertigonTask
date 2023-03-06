using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CertigonTask_API_V3.Helpers.AuthenticationAuthorization.MyAuthTokenExtension;
using System.Security.Cryptography;
using CertigonTask_API_V3.Entities;
using CertigonTask_API_V3.Data;
using CertigonTask_API_V3.Models.Accounts;
using CertigonTask_API_V3.Helpers;
using CertigonTask_API_V3.Services.AuthenticationService;

namespace CertigonTask_API_V3.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] AccountRegisterVM request)
        {
            var result = await _authenticationService.Register(request);
            if (result is null)
                return BadRequest("User already registered");

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<LoginInformation>> Login([FromBody] AccountLoginVM loginVM)
        {
            var result = await _authenticationService.Login(loginVM, Request);
            if (result is null)
                return NotFound("User is not found");

            return new LoginInformation(result);
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            AuthenticationToken authenticationToken = HttpContext.GetAuthToken();

            if (authenticationToken == null)
                return Ok();

            var result = await _authenticationService.Logout(authenticationToken);
            return Ok();
        }
    }
}