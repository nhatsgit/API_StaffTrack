using API_StaffTrack.Application.Services;
using API_StaffTrack.Models.Request;
using API_StaffTrack.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_StaffTrack.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
  //  [Authorize]
    public class MonthlyReportController : ControllerBase
    {
        private readonly IS_MonthlyReport _reportService;

        public MonthlyReportController(IS_MonthlyReport reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MReq_MonthlyReport request)
        {
            var result = await _reportService.Create(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MReq_MonthlyReport request)
        {
            var result = await _reportService.Update(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _reportService.Delete(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _reportService.GetById(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reportService.GetAll();
            return Ok(result);
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetByEmployee(int employeeId)
        {
            var result = await _reportService.GetByEmployee(employeeId);
            return Ok(result);
        }
    }
}
