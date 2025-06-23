using System.Threading.Tasks;
using API_StaffTrack.Data.Entities;
using API_StaffTrack.Models.Request;
using API_StaffTrack.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_StaffTrack.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IS_Account _accountService;

        public AccountController(IS_Account accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin([FromBody] MReq_Register request, int employeeId)
        {
            

            var result = await _accountService.RegisterAdmin(employeeId,request);
            if (result == "User registered successfully.")
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterEmployee([FromBody] MReq_Register request,int employeeId)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email
            };

            var result = await _accountService.RegisterEmployee(employeeId, request);
            if (result == "User registered successfully.")
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Mreq_Login request)
        {
            var token = await _accountService.Login(request.UserName, request.Password);
            if (token == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(token);
        }
    }

   
}
