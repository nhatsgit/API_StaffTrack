using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using API_StaffTrack.Models;
using API_StaffTrack.Data.Entities;
using Azure.Core;
using API_StaffTrack.Models.Request;
using API_StaffTrack.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace API_StaffTrack.Services
{
    public interface IS_Account
    {
        Task<string> RegisterAdmin(int employeeId, MReq_Register request);
        Task<string> Login(string email, string password);
        Task<string> RegisterEmployee(int employeeId, MReq_Register request);
    }

    public class S_Account : IS_Account
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly MainDbContext _context;
        public S_Account(UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, MainDbContext context)
        {
            _userManager = userManager;
            _config = configuration;
            _roleManager=roleManager;
            _context = context;
        }

        public async Task<string> RegisterAdmin(int employeeId, MReq_Register request)
        {
            var employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == employeeId);
            if (employee == null)
            {
                return "Không tìm thấy nhân viên.";
            }
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email
            };
            user.EmployeeId = employeeId;
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                await _userManager.AddToRoleAsync(user, "Admin");
                return "Employee registered successfully.";
            }

            return string.Join(", ", result.Errors.Select(e => e.Description));
        }
        public async Task<string> RegisterEmployee(int employeeId, MReq_Register request)
        {
            var employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == employeeId);
            if (employee == null)
            {              
                return "Không tìm thấy nhân viên.";
            }
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email
            };
            user.EmployeeId = employeeId;
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Employee"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Employee"));
                }
                await _userManager.AddToRoleAsync(user, "Employee");
                return "Employee registered successfully.";
            }

            return string.Join(", ", result.Errors.Select(e => e.Description));
        }
        public async Task<string> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return "Tài khoản hoặc mật khẩu không chính xác";
            }

            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim("EmployeeId", user.EmployeeId?.ToString() ?? "0")
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
