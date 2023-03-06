using CertigonTask_API_V3.Data;
using CertigonTask_API_V3.Entities;
using CertigonTask_API_V3.Helpers;
using CertigonTask_API_V3.Helpers.AuthenticationAuthorization;
using CertigonTask_API_V3.Models.Accounts;
using Microsoft.EntityFrameworkCore;
using static CertigonTask_API_V3.Helpers.AuthenticationAuthorization.MyAuthTokenExtension;

namespace CertigonTask_API_V3.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationDbContext _dbContext;
        public AuthenticationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuthenticationToken?> Login(AccountLoginVM req, HttpRequest httpRequest)
        {
            UserAccount loggedUser = await _dbContext.UserAccount
               .FirstOrDefaultAsync(k => k.UserName != null && k.UserName == req.UserName && k.PasswordHash == req.Password.HashPassword());

            if (loggedUser == null)
            {
                //pogresan username i password
                return null;
            }

            //2- generisati random string
            string randomString = TokenGenerator.Generate(10);

            //3- dodati novi zapis u tabelu AutentifikacijaToken za logiraniKorisnikId i randomString
            var noviToken = new AuthenticationToken()
            {
                IpAdress = httpRequest.HttpContext.Connection.RemoteIpAddress?.ToString(),
                Value = randomString,
                UserAccount = loggedUser,
                Created_Time = DateTime.Now
            };

            _dbContext.Add(noviToken);
            await _dbContext.SaveChangesAsync();

            //4- vratiti token string
            return noviToken;
        }

        public async Task<AuthenticationToken?> Logout(AuthenticationToken authenticationToken)
        {
            _dbContext.Remove(authenticationToken);
            await _dbContext.SaveChangesAsync();

            return authenticationToken;
        }

        public async Task<UserAccount?> Register(AccountRegisterVM req)
        {
            if (await _dbContext.UserAccount.AnyAsync(u => u.UserName == req.Username && u.Email == req.Email))
                return null;

            UserAccount user = new()
            {
                UserName = req.Username,
                PasswordHash = req.Password.HashPassword(),
                Email = req.Email
            };

            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}
