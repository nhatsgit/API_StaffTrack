using API_StaffTrack.Application.Services;
using API_StaffTrack.Models.Request;
using API_StaffTrack.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_StaffTrack.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
   // [Authorize]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IS_LeaveRequest _leaveService;

        public LeaveRequestController(IS_LeaveRequest leaveService)
        {
            _leaveService = leaveService;
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> CreateByEmployee ([FromBody] MReq_LeaveRequest request)
        {
            var employeeIdClaim = User.FindFirst("EmployeeId");
            if (employeeIdClaim == null)
                return Unauthorized("Không xác định được EmployeeId.");
            request.EmployeeId = int.Parse(employeeIdClaim.Value); // Lấy EmployeeId từ claim
            var result = await _leaveService.Create(request);
            return Ok(result);
        }
        

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MReq_LeaveRequest request)
        {
            var result = await _leaveService.Update(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _leaveService.Delete(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _leaveService.GetById(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _leaveService.GetAll();
            return Ok(result);
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetByEmployee(int employeeId)
        {
            var result = await _leaveService.GetByEmployee(employeeId);
            return Ok(result);
        }
        [HttpPost("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id)
        {
            var employeeIdClaim = User.FindFirst("EmployeeId");

            if (employeeIdClaim == null)
                return Unauthorized("Không xác định được EmployeeId.");

            int approvedBy = int.Parse(employeeIdClaim.Value); 
            var result = await _leaveService.Approve(id, approvedBy);
            return Ok(result);
        }
    }
}
