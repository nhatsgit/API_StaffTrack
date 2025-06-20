using API_StaffTrack.Application.Services;
using API_StaffTrack.Models.Request;
using API_StaffTrack.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace API_StaffTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IS_Notification _notificationService;

        public NotificationController(IS_Notification notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MReq_Notification request)
        {
            var res = await _notificationService.Create(request);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MReq_Notification request)
        {
            var res = await _notificationService.Update(id, request);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _notificationService.Delete(id);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _notificationService.GetById(id);
            return Ok(res);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var res = await _notificationService.GetAll();
            return Ok(res);
        }

        [HttpGet("by-employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployeeId(int employeeId)
        {
            var res = await _notificationService.GetListByEmployeeId(employeeId);
            return Ok(res);
        }
    }
}
