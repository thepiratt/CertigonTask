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
    public class AutentifikacijaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AutentifikacijaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult Register([FromBody] AccountRegisterVM model)
        {
            if (_dbContext.KorisnickiNalog.Any(k => k.korisnickoIme == model.username && k.email == model.email))
                return BadRequest("User already registered");

            KorisnickiNalog user = new()
            {
                korisnickoIme = model.username,
                PasswordHash = model.password.HashPassword(),
                email = model.email
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

            return Ok(_dbContext.KorisnickiNalog.FirstOrDefault(i => i.id == id)); ;
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] UpdateVM x)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("You are not logged in!");

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
                user = _dbContext.KorisnickiNalog.FirstOrDefault(k => k.id == id);
                if (user == null)
                    return BadRequest("No user with that ID!");
            }

            user.korisnickoIme = x.username.RemoveTags();
            user.PasswordHash = x.password.RemoveTags().HashPassword();
            user.email = x.email.RemoveTags();
            user.isAdmin = x.isAdmin;
            user.isManager = x.isManager;

            _dbContext.SaveChanges();
            return Get(user.id);
        }

        [HttpPost]
        public ActionResult<LoginInformacije> Login([FromBody] AccountLoginVM x)
        {
            //1- provjera logina
            KorisnickiNalog logiraniKorisnik = _dbContext.KorisnickiNalog
                .FirstOrDefault(k => k.korisnickoIme != null && k.korisnickoIme == x.korisnickoIme && k.PasswordHash == x.lozinka.HashPassword());

            if (logiraniKorisnik == null)
            {
                //pogresan username i password
                return new LoginInformacije(null);
            }

            //2- generisati random string
            string randomString = TokenGenerator.Generate(10);

            //3- dodati novi zapis u tabelu AutentifikacijaToken za logiraniKorisnikId i randomString
            var noviToken = new AutentifikacijaToken()
            {
                ipAdresa = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                vrijednost = randomString,
                korisnickiNalog = logiraniKorisnik,
                vrijemeEvidentiranja = DateTime.Now
            };

            _dbContext.Add(noviToken);
            _dbContext.SaveChanges();

            //4- vratiti token string
            return new LoginInformacije(noviToken);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();

            if (autentifikacijaToken == null)
                return Ok();

            _dbContext.Remove(autentifikacijaToken);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<AutentifikacijaToken> Get()
        {
            AutentifikacijaToken autentifikacijaToken = HttpContext.GetAuthToken();

            return autentifikacijaToken;
        }
    }
}