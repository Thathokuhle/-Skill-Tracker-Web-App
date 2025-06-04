using BusinessLogic.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewLogic.AccountViews;

namespace BusinessLogic.Token
{
	public static class TokenBusiness
	{
		public static string GenerateJsonWebToken(GetUserByEmailView staff, int tokenDurationMinutes = 120)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.AppSettings.GetJWTTokenKey()));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[] {
				new Claim((string) JwtRegisteredClaimNames.Sub, (string) staff.FirstName + " " + staff.LastName),
				new Claim((string)JwtRegisteredClaimNames.Email, (string)staff.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var token = new JwtSecurityToken(AppSettings.AppSettings.BaseUrl,
				AppSettings.AppSettings.BaseUrl,
				claims,
				expires: DateTime.Now.SaDateTime().AddMinutes(tokenDurationMinutes),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
