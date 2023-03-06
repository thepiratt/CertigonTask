using CertigonTask_API_V3.Entities;
using CertigonTask_API_V3.Models.Accounts;
using CertigonTask_API_V3.Models.Items;
using static CertigonTask_API_V3.Helpers.AuthenticationAuthorization.MyAuthTokenExtension;

namespace CertigonTask_API_V3.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<AuthenticationToken?> Login(AccountLoginVM request, HttpRequest httpRequest);
        Task<AuthenticationToken?> Logout(AuthenticationToken authenticationToken);
        Task<UserAccount?> Register(AccountRegisterVM request);

    }
}
