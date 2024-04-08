// using ApartmentManagmentBlazorAppCopy.Requests;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
//
// namespace ApartmentManagmentBlazorAppCopy
// {
//     public interface IAuthServices
//     {
//         Task Register(RegistrationRequest request);
//         Task<LogInResponse> LogIn( LogInRequest request);
//     }
//     public class AuthServices : IAuthServices
//     {
//         private readonly UserManager<IdentityUser> _userManager;
//         private readonly UserContext _context;
//         private readonly ITokenServices _tokenService;
//
//         public AuthServices(UserManager<IdentityUser> userManager, UserContext context, ITokenServices tokenServices)
//         {
//             _userManager = userManager;
//             _context = context;
//             _tokenService = tokenServices;
//         }
//
//         public async Task Register(RegistrationRequest request)
//         {
//             var result =
//                 await _userManager.CreateAsync(new IdentityUser { UserName = request.Name, Email = request.Email },
//                     request.Password);
//             
//             if (result.Succeeded)
//             {
//                 await _context.SaveChangesAsync();
//             }
//            
//         }
//
//         public async Task<LogInResponse> LogIn([FromBody] LogInRequest request)
//         {
//             var managedUser = await _userManager.FindByEmailAsync(request.Email);
//             if (managedUser == null)
//             {
//                 throw new Exception("Not found");
//             }
//
//             var isPasswordOk = await _userManager.CheckPasswordAsync(managedUser, request.Password);
//             
//             if (!isPasswordOk)
//             {
//                 throw new Exception("Bad password");
//             }
//
//
//             var accesToken = _tokenService.CreateToken(managedUser);
//             await _context.SaveChangesAsync();
//
//             return new() { Token = accesToken, Emial = managedUser.Email!};
//         }
//     }
// }
