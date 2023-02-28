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
        private readonly ApplicationDbContext _dbContext;

        public UserAccountController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return Forbid();

            return Ok(_dbContext.UserAccount.FirstOrDefault(i => i.Id == id)); ;
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] UserAccountUpdateVM x)
        {
            if (!HttpContext.GetLoginInfo().isPermissionAdmin)
                return BadRequest("You are not Admin!");

            UserAccount user;

            if (id == 0)
            {
                user = new UserAccount
                {
                    Created_time = DateTime.Now
                };
                _dbContext.Add(user);
            }
            else
            {
                user = _dbContext.UserAccount.FirstOrDefault(i => i.Id == id);
                if (user == null)
                    return BadRequest("Unknown ID");
            }

            user.UserName = x.UserName.RemoveTags();
            user.Email = x.Email.RemoveTags();
            user.isManager = x.isManager;
            user.isAdmin = x.isAdmin;
            

            _dbContext.SaveChanges();
            return Get(user.Id);
        }

        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermissionAdmin)
                return BadRequest("You are not Admin!");

            UserAccount user = _dbContext.UserAccount.Find(id);

            if (user == null || id == 1)
                return BadRequest("Incorrect ID");

            _dbContext.Remove(user);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("You are not logged in!");

            var data = _dbContext.UserAccount.ToList().AsQueryable();
            return Ok(data.Take(100).ToList());
        }


    }
}
