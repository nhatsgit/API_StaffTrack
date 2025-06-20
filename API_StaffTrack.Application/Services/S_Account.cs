using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using API_StaffTrack.Models;
using API_StaffTrack.Data.Entities;

namespace API_StaffTrack.Services
{
    public interface IS_Account
    {
        Task<string> Register(ApplicationUser user, string password);
        Task<string> Login(string email, string password);
    }

    public class S_Account : IS_Account
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;

        public S_Account(UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _config = configuration;
            _roleManager=roleManager;
        }

        public async Task<string> Register(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return "User registered successfully.";
            }

            return string.Join(", ", result.Errors.Select(e => e.Description));
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return string.Empty;
            }

            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            authClaims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));

                var roleObj = await _roleManager.FindByNameAsync(role);
                if (roleObj != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(roleObj);

                    foreach (var claim in roleClaims)
                    {
                        if (!authClaims.Any(c => c.Type == claim.Type))
                        {
                            authClaims.Add(claim);
                        }
                    }
                }
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        
    }
}
