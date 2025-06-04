using System.ComponentModel.DataAnnotations;

namespace ViewLogic.AccountViews
{
	public class CreateAccountView
	{
		
		[Required(ErrorMessage = "Please enter your first name")]
		public string FirstName { get; set; } = string.Empty;

		
		[Required(ErrorMessage = "Please enter your last name")]
		public string LastName { get; set; } = string.Empty;

		public string EmailAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your date of birth")]
        public DateTime DateOfBirth { get; set; }

	}
}
