﻿using Microsoft.EntityFrameworkCore;

namespace DataLogic.Account
{
	[Keyless]
	public class GetUserByEmail
	{
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }

    }
}
