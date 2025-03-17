using System.ComponentModel.DataAnnotations;

namespace WinesLAP6.Data.Models
{
	public class Vintner
	{
		[Key]
		[Required]
		public int VintnerId { get; set; }

		[Required]
		[StringLength(50)]
		public string VintnerName { get; set; }

		public ICollection<Wine> Wines { get; set; } = new List<Wine>();
	}
}
