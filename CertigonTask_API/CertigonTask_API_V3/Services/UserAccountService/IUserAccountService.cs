using CertigonTask_API_V3.Entities;
using CertigonTask_API_V3.Models.Accounts;
using CertigonTask_API_V3.Models.Items;

namespace CertigonTask_API_V3.Services.UserAccountService
{
    public interface IUserAccountService
    {
        Task<List<UserAccount>> GetAllUsers();
        Task<UserAccount?> GetSingleUser(int id);
        Task<UserAccount?> UpdateUser(int id, UserAccountUpdateVM request);
        Task<UserAccount?> DeleteUser(int id);
    }
}
