using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
using WinesLAP6.Data.Models;
using WinesLAP6.Data.Models.Dtos;
using WinesLAP6.Services.Mappers;

namespace WinesLAP6.Services
{
	public class ImportCsvService
	{
		private readonly WineService wineService;
		private readonly VintnerService vintnerService;

		public ImportCsvService(WineService WineService, VintnerService VintnerService)
		{
			wineService = WineService;
			vintnerService = VintnerService;
		}

		public async Task Import(string filePath)
		{
			if (string.IsNullOrWhiteSpace(filePath))
			{
				throw new ArgumentNullException(nameof(filePath));
			}

			var config = new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				Delimiter = ",",
				HasHeaderRecord = true,
				HeaderValidated = null,
				MissingFieldFound = null,
				Encoding = Encoding.UTF8
			};

			using (var reader = new StreamReader(filePath))
			using (var csv = new CsvReader(reader, config))
			{
				csv.Context.RegisterClassMap<WineCsvMapper>();
				var wines = csv.GetRecords<WineCsvViewModel>();

				foreach (var wine in wines)
				{
					var vintner = await vintnerService.GetVintnerByName(wine.VintnerName);

					if (vintner == null)
					{
						vintner = new Vintner
						{
							VintnerName = wine.VintnerName,
						};

						await vintnerService.AddVintnerAsync(vintner);
						await AddSaveWineAsync(wine, vintner);
						continue;
					}

					if(await wineService.WineAlreadyExistsAsync(wine.WineName, wine.Region, wine.Vintage))
					{
						await AddSaveWineAsync(wine, vintner);
					}
				}
			}
		}

		private async Task AddSaveWineAsync(WineCsvViewModel wine, Vintner vintner)
		{
			if (wine != null)
			{
				var newWine = new Wine
				{
					WineName = wine.WineName,
					Region = wine.Region,
					Vintage = wine.Vintage,
					VintnerId = vintner.VintnerId,
					Vintner = vintner
				};

				await wineService.AddWineAsync(newWine);
			}
		}
	}
}
