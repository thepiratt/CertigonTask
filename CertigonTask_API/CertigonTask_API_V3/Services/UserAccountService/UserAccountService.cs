using CertigonTask_API_V3.Data;
using CertigonTask_API_V3.Entities;
using CertigonTask_API_V3.Helpers;
using CertigonTask_API_V3.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace CertigonTask_API_V3.Services.UserAccountService
{
    public class UserAccountService : IUserAccountService
    {
        private readonly ApplicationDbContext _dbContext;
        public UserAccountService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserAccount?> DeleteUser(int id)
        {
            var user = await _dbContext.UserAccount.FindAsync(id);
            if (user == null)
                return null;

            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<List<UserAccount>> GetAllUsers()
        {
            var users = await _dbContext.UserAccount.ToListAsync();
            return users;
        }

        public async Task<UserAccount?> GetSingleUser(int id)
        {
            var user = await _dbContext.UserAccount.FindAsync(id);
            if (user == null)
                return null;
            return user;
        }

        public async Task<UserAccount?> UpdateUser(int id, UserAccountUpdateVM req)
        {
            UserAccount user;

            if (id == 0)
            {
                user = new UserAccount
                {
                    Created_time = DateTime.Now
                };
                await _dbContext.AddAsync(user);
            }
            else
            {
                user = await _dbContext.UserAccount.FindAsync(id);
                if (user == null)
                    return null;
            }

            user.UserName = req.UserName.RemoveTags();
            user.Email = req.Email.RemoveTags();
            user.isManager = req.isManager;
            user.isAdmin = req.isAdmin;


            await _dbContext.SaveChangesAsync();

            return await _dbContext.UserAccount.FindAsync(id);
        }
    }
}
