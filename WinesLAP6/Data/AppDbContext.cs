using Microsoft.EntityFrameworkCore;
using WinesLAP6.Data.Models;

namespace WinesLAP6.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<User> Users {  get; set; }
		public DbSet<Vintner> Vintners {  get; set; }
		public DbSet<Wine> Wines {  get; set; }
	}
}
