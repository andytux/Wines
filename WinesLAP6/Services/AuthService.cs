using System.Security.Cryptography;
using System.Text;

namespace WinesLAP6.Services
{
	public class AuthService
	{
		private readonly UserService userService;

		public AuthService(UserService userService)
		{
			this.userService = userService;
		}

		public string GetSaltOfGuid(Guid userId)
		{
			return userId.ToString().Substring(0, 8);
		}

		private byte[] GetHashableBytes(string password, string salt) {
			var combined = password + salt;

			return Encoding.UTF8.GetBytes(combined);
		}

		public string GetPasswordHash(string password, string salt) { 
			var hashableBytes = GetHashableBytes(password, salt);

			using(var sha256 = SHA256.Create())
			{
				var hashBytes = sha256.ComputeHash(hashableBytes);

				return Encoding.UTF8.GetString(hashBytes);
			}
		}

		public bool VerifyPassword(string password, string storedHash, string salt)
		{
			var hashOfInput = GetPasswordHash(password, salt);

			if (hashOfInput != storedHash)
			{
				return false;
			}

			return true;
		}
	}
}
