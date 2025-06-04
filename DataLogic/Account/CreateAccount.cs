using Microsoft.EntityFrameworkCore;

namespace DataLogic.Account
{
	[Keyless]
	public class CreateAccount
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string EmailAddress { get; set; } = string.Empty;
		public DateTime DateOfBirth { get; set; }
	}
}
