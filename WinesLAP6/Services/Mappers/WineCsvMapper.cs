using CsvHelper.Configuration;
using WinesLAP6.Data.Models.Dtos;

namespace WinesLAP6.Services.Mappers
{
	public class WineCsvMapper : ClassMap<WineCsvViewModel>
	{
		public WineCsvMapper() {
			Map(m => m.WineName).Name("Weinname");
			Map(m => m.Region).Name("Region");
			Map(m => m.VintnerName).Name("Winzer");
			Map(m => m.Vintage).Name("Jahrgang");
		}
	}
}
