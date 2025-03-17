using Microsoft.EntityFrameworkCore;
using WinesLAP6.Data;
using WinesLAP6.Data.Models;
using WinesLAP6.Data.Models.Dtos;

namespace WinesLAP6.Services
{
	public class WineService
	{
		private readonly AppDbContext dbContext;

		public WineService(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task AddWineAsync(Wine wine)
		{
			dbContext.Wines.Add(wine);
			await dbContext.SaveChangesAsync();
		}

		public async Task<Wine?> GetWineByNameAsync(string wineName, string region, int vintage)
		{
			return await dbContext.Wines.FirstOrDefaultAsync(w => w.WineName == wineName && w.Region == region && w.Vintage == vintage);
		}

		public async Task<bool> WineAlreadyExistsAsync(string wineName, string region, int vintage)
		{
			return await dbContext.Wines.AnyAsync(w => w.WineName == wineName && w.Region == region && w.Vintage == vintage);
		}

		public async Task<List<Wine>> GetAllWinesAsync()
		{
			return await dbContext.Wines.Include(w=> w.Vintner).ToListAsync();
		}

		public async Task<WineViewModel> GetWineViewModel(Wine wine)
		{
			return new WineViewModel
			{
				WineId = wine.WineId,
				Region = wine.Region,
				Vintage = wine.Vintage,
				IsSelected = wine.IsSelected,
				VintnerName = wine.Vintner.VintnerName,
				WineName = wine.WineName,
			};
		}

		public async Task<List<Wine>> GetSelectedWines()
		{
			return await dbContext.Wines.Where(w=> w.IsSelected==true).Include(w=> w.Vintner).ToListAsync();
		}

		public async Task UpdateWineSelection(WineViewModel wine, bool isSelected)
		{
			var storedWine = await GetWineByNameAsync(wine.WineName, wine.Region, wine.Vintage);

			if (storedWine != null)
			{
				if (isSelected)
				{
					storedWine.IsSelected = isSelected;
				}
				else
				{
					storedWine.IsSelected = false;
				}

				dbContext.Wines.Update(storedWine);
				await dbContext.SaveChangesAsync();
			}
		}
	}
}
