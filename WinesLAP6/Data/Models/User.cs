using System.ComponentModel.DataAnnotations;

namespace WinesLAP6.Data.Models
{
	public class User
	{
		[Key]
		[Required]
		public Guid UserId { get; set; } = Guid.NewGuid();

		[Required]
		public string UserName { get; set; }

		[Required]
		public string PasswordHash { get; set; }
	}
}
