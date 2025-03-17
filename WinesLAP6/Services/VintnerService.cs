using Microsoft.EntityFrameworkCore;
using WinesLAP6.Data;
using WinesLAP6.Data.Models;

namespace WinesLAP6.Services
{
	public class VintnerService
	{
		private readonly AppDbContext dbContext;

		public VintnerService(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task AddVintnerAsync(Vintner vintner)
		{
			dbContext.Vintners.Add(vintner);
			await dbContext.SaveChangesAsync();
		}

		public async Task<Vintner?> GetVintnerByName(string vintnerName)
		{
			return await dbContext.Vintners.FirstOrDefaultAsync(v => v.VintnerName == vintnerName);
		}

		public async Task<bool> VintnerAlreadyExists(string vintnerName)
		{
			return await dbContext.Vintners.AnyAsync(v => v.VintnerName == vintnerName);
		}

	}
}
