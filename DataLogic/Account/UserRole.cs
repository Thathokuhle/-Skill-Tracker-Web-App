using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Account
{
	[Keyless]
	public class UserRole
	{
		public string Role { get; set; } = string.Empty;
	}
}
