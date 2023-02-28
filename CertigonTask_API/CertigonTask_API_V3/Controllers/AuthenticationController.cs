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

namespace CertigonTask_API_V3.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthenticationController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult Register([FromBody] AccountRegisterVM model)
        {
            if (_dbContext.UserAccount.Any(u => u.UserName == model.Username && u.Email == model.Email))
                return BadRequest("User already registered");

            UserAccount user = new()
            {
                UserName = model.Username,
                PasswordHash = model.Password.HashPassword(),
                Email = model.Email
            };

            _dbContext.Add(user);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return Forbid();

            return Ok(_dbContext.UserAccount.FirstOrDefault(i => i.Id == id)); ;
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] AccountUpdateVM x)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("You are not logged in!");

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
                user = _dbContext.UserAccount.FirstOrDefault(k => k.Id == id);
                if (user == null)
                    return BadRequest("No user with that ID!");
            }

            user.UserName = x.Username.RemoveTags();
            user.PasswordHash = x.Password.RemoveTags().HashPassword();
            user.Email = x.Email.RemoveTags();
            user.isAdmin = x.isAdmin;
            user.isManager = x.isManager;

            _dbContext.SaveChanges();
            return Get(user.Id);
        }

        [HttpPost]
        public ActionResult<LoginInformation> Login([FromBody] AccountLoginVM x)
        {
            //1- provjera logina
            UserAccount loggedUser = _dbContext.UserAccount
                .FirstOrDefault(k => k.UserName != null && k.UserName == x.UserName && k.PasswordHash == x.Password.HashPassword());

            if (loggedUser == null)
            {
                //pogresan username i password
                return new LoginInformation(null);
            }

            //2- generisati random string
            string randomString = TokenGenerator.Generate(10);

            //3- dodati novi zapis u tabelu AutentifikacijaToken za logiraniKorisnikId i randomString
            var noviToken = new AuthenticationToken()
            {
                IpAdress = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                Value = randomString,
                UserAccount = loggedUser,
                Created_Time = DateTime.Now
            };

            _dbContext.Add(noviToken);
            _dbContext.SaveChanges();

            //4- vratiti token string
            return new LoginInformation(noviToken);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AuthenticationToken authenticationToken = HttpContext.GetAuthToken();

            if (authenticationToken == null)
                return Ok();

            _dbContext.Remove(authenticationToken);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<AuthenticationToken> Get()
        {
            AuthenticationToken authenticationToken = HttpContext.GetAuthToken();

            return authenticationToken;
        }
    }
}