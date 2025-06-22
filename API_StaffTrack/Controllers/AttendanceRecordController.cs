using API_StaffTrack.Application.Services;
using API_StaffTrack.Models.Common;
using API_StaffTrack.Models.Request;
using API_StaffTrack.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_StaffTrack.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
   // [Authorize]
    public class AttendanceRecordController : ControllerBase
    {
        private readonly IS_AttendanceRecord _attendanceService;

        public AttendanceRecordController(IS_AttendanceRecord attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MReq_AttendanceRecord request)
        {
            var result = await _attendanceService.Create(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MReq_AttendanceRecord request)
        {
            var result = await _attendanceService.Update(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _attendanceService.Delete(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _attendanceService.GetById(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _attendanceService.GetAll();
            return Ok(result);
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetListByEmployee(int employeeId)
        {
            var result = await _attendanceService.GetListByEmployee(employeeId);
            return Ok(result);
        }
    }
}

