using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CertigonTask_API_V3.Data;
using CertigonTask_API_V3.Entities;
using CertigonTask_API_V3.Helpers;
using CertigonTask_API_V3.Helpers.AuthenticationAuthorization;
using CertigonTask_API_V3.Models.Accounts;
using CertigonTask_API_V3.Models.Items;
using CertigonTask_API_V3.Services.UserAccountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CertigonTask_API_V3.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;

        public UserAccountController(IUserAccountService userAccountService)
        {
            this._userAccountService = userAccountService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return Forbid();

            var result = await _userAccountService.GetSingleUser(id);
            if (result is null)
                return NotFound("User is not found");

            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UserAccountUpdateVM request)
        {
            if (!HttpContext.GetLoginInfo().isPermissionAdmin)
                return BadRequest("You are not Admin!");

            var result = await _userAccountService.UpdateUser(id, request);
            if (result is null)
                return NotFound("User not found!");

            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermissionAdmin)
                return BadRequest("You are not Admin!");

            var result = await _userAccountService.DeleteUser(id);
            if (result is null)
                return NotFound("Item not found!");

            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserAccount>>> GetAll()
        {
            if (!HttpContext.GetLoginInfo().isPermissionAdmin)
                return BadRequest("You are not Admin!");

            /*var data = _dbContext.UserAccount.ToList().AsQueryable();
            return Ok(data.Take(100).ToList());*/

            return await _userAccountService.GetAllUsers();
        }


    }
}
