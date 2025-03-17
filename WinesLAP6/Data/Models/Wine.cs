using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinesLAP6.Data.Models
{
	public class Wine
	{
		[Key]
		public int WineId { get; set; }

		[Required]
		[StringLength(50)]
		public string WineName { get; set;}

		[Required]
		[StringLength(50)]
		public string Region { get; set; }

		[Required]
		public int Vintage { get; set; }

		public int VintnerId { get; set; }

		[ForeignKey("VintnerId")]
		public Vintner Vintner { get; set; }

		public bool IsSelected { get; set; } = false;
	}
}
