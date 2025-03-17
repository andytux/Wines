using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WinesLAP6.Data.Models.Dtos
{
	public class WineViewModel
	{
		public int WineId { get; set; }

		public string WineName { get; set; }

		public string Region { get; set; }

		public int Vintage { get; set; }

		public string VintnerName { get; set; }

		public bool IsSelected { get; set; } = false;
	}
}
