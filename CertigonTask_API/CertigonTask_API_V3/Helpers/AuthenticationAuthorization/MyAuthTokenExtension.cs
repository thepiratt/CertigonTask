using System.Linq;
using System.Text.Json.Serialization;
using AutoMapper.QueryableExtensions;
using CertigonTask_API_V3.Data;
using CertigonTask_API_V3.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CertigonTask_API_V3.Helpers.AuthenticationAuthorization
{
    public static class MyAuthTokenExtension
    {
        public class LoginInformation
        {
            public LoginInformation(AuthenticationToken autentifikacijaToken)
            {
                this.authenticationToken = autentifikacijaToken;
            }

            [JsonIgnore]
            public UserAccount userAccount => authenticationToken?.UserAccount;
            public AuthenticationToken authenticationToken { get; set; }
            
            public bool isLogiran => userAccount != null;

            public bool isPermissionAdmin => isLogiran && userAccount.isAdmin;

            public bool isPermissionManager => isLogiran && userAccount.isManager;
        }


        public static LoginInformation GetLoginInfo(this HttpContext httpContext)
        {
            var token = httpContext.GetAuthToken();

            return new LoginInformation(token);
        }
    
        public static AuthenticationToken GetAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.GetMyAuthToken();
            ApplicationDbContext db = httpContext.RequestServices.GetService<ApplicationDbContext>();

            AuthenticationToken authToken = db.AuthenticationToken
                .Include(u=>u.UserAccount)
                .SingleOrDefault(x => token != null && x.Value == token);
            
            return authToken;
        }


        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["authentication-token"];
            return token;
        }
    }
}
