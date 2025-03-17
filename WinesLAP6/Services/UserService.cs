using Microsoft.EntityFrameworkCore;
using WinesLAP6.Data;
using WinesLAP6.Data.Models;

namespace WinesLAP6.Services
{
	public class UserService
	{
		private readonly AppDbContext dbContext;

		public UserService(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task AddUserAsync(User user)
		{
			dbContext.Users.Add(user);
			await dbContext.SaveChangesAsync();
		}

		public async Task<User?> GetUserByName (string userName)
		{
			return await dbContext.Users.FirstOrDefaultAsync(u=> u.UserName == userName);
		}

		public async Task<bool> UserAlreadyExists (string userName)
		{
			return await dbContext.Users.AnyAsync(u => u.UserName == userName);
		}
	}
}
