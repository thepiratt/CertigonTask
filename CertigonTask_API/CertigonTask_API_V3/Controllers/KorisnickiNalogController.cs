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
    public class KorisnickiNalogController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KorisnickiNalogController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return Forbid();

            return Ok(_dbContext.KorisnickiNalog.FirstOrDefault(i => i.id == id)); ;
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] KorisnickiNalogUpdateVM x)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaAdmin)
                return BadRequest("You are not Admin!");

            KorisnickiNalog user;

            if (id == 0)
            {
                user = new KorisnickiNalog
                {
                    created_time = DateTime.Now
                };
                _dbContext.Add(user);
            }
            else
            {
                user = _dbContext.KorisnickiNalog.FirstOrDefault(i => i.id == id);
                if (user == null)
                    return BadRequest("Unknown ID");
            }

            user.korisnickoIme = x.korisnickoIme.RemoveTags();
            user.email = x.email.RemoveTags();
            user.isManager = x.isManager;
            user.isAdmin = x.isAdmin;
            

            _dbContext.SaveChanges();
            return Get(user.id);
        }

        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaAdmin)
                return BadRequest("You are not Admin!");

            KorisnickiNalog user = _dbContext.KorisnickiNalog.Find(id);

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

            var data = _dbContext.KorisnickiNalog.ToList().AsQueryable();
            return Ok(data.Take(100).ToList());
        }


    }
}
