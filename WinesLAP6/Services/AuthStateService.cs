namespace WinesLAP6.Services
{
	public class AuthStateService
	{
		public bool IsLoggedIn { get; set; } = false;
		private string? UserName { get; set; }
		private Guid? UserId { get; set; }

		public event Action? AuthStateChanged;

		public void Login(string userName, Guid? userId)
		{
			IsLoggedIn = true;
			UserName = userName;
			UserId = userId;

			AuthStateChanged?.Invoke();
		}

		public void Logout()
		{
			IsLoggedIn = false;
			UserName = null;
			UserId = null;

			AuthStateChanged?.Invoke();
		}
	}
}
