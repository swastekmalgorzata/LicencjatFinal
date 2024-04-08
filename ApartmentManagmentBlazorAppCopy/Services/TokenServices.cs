// using Microsoft.AspNetCore.Identity;
// using System.IdentityModel.Tokens.Jwt;
// using Microsoft.IdentityModel.Tokens;
// using System.Security.Claims;
// using System.Globalization;
// using System.Text;
//
// namespace ApartmentManagmentBlazorAppCopy
// {
//     public interface ITokenServices
//     {
//         string CreateToken(IdentityUser user);
//     }
//     public class TokenServices : ITokenServices
//     {
//         private const int ExpirationTime = 30;
//         public string CreateToken(IdentityUser user)
//         {
//             var expiration = DateTime.UtcNow.AddMinutes(ExpirationTime);
//             var token = CreateJwtToken(
//                 CreateClaims(user),
//                 CreateSingingCredentials(),
//                 expiration
//                 );
//             var tokenHandler = new JwtSecurityTokenHandler();
//             return tokenHandler.WriteToken(token);
//         }
//
//         private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration) =>
//         new(
//             "ApartmentManagmentBlazorAppCopy",
//             "ApartmentManagmentBlazorAppCopy",
//             claims,
//             expires: expiration,
//             signingCredentials: credentials
//             );
//
//         private List<Claim> CreateClaims(IdentityUser user)
//         {
//             try
//             {
//                 var claims = new List<Claim>
//                 {
//                     new Claim(JwtRegisteredClaimNames.Sub, "TokenForMyApi"),
//                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                     new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
//                     new Claim("UserId", user.Id),
//                     new Claim(ClaimTypes.Email, user.Email)
//                 };
//                 return claims;
//             }
//             catch(Exception e)
//             {
//                 Console.WriteLine(e);
//                 throw;
//             }
//         }
//         private SigningCredentials CreateSingingCredentials()
//         {
//             return new SigningCredentials(
//                 new SymmetricSecurityKey(
//                     Encoding.UTF8.GetBytes("!secret11111111!")),
//                 SecurityAlgorithms.HmacSha256
//                 );
//                 
//         }
//
//     }
// }
