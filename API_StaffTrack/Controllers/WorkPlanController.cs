using API_StaffTrack.Application.Services;
using API_StaffTrack.Models.Request;
using API_StaffTrack.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace API_StaffTrack.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkPlanController : ControllerBase
    {
        private readonly IS_WorkPlan _service;

        public WorkPlanController(IS_WorkPlan service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MReq_WorkPlan request)
        {
            var res = await _service.Create(request);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] MReq_WorkPlan request)
        {
            var res = await _service.Update(id, request);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _service.Delete(id);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _service.GetById(id);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _service.GetAll();
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetByEmployee(int employeeId)
        {
            var res = await _service.GetListByEmployee(employeeId);
            return Ok(res);
        }
    }
}
