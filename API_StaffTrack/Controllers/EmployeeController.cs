using API_StaffTrack.Application.Services;
using API_StaffTrack.Data;
using API_StaffTrack.Data.EF;
using API_StaffTrack.Data.Entities;
using API_StaffTrack.Models.Common;
using API_StaffTrack.Models.Request;
using API_StaffTrack.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_StaffTrack.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IS_Employee _employeeService;

    public EmployeeController(IS_Employee employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost]
    public async Task<ActionResult<ResponseData<MRes_Employee>>> Create([FromBody] MReq_Employee request)
    {
        var result = await _employeeService.Create(request);
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<ResponseData<MRes_Employee>>> Update(int id, [FromBody] MReq_Employee request)
    {
        var result = await _employeeService.Update(id, request);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<ActionResult<ResponseData<int>>> Delete(int id)
    {
        var result = await _employeeService.Delete(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<ResponseData<MRes_Employee>>> GetById(int id)
    {
        var result = await _employeeService.GetById(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<ResponseData<List<MRes_Employee>>>> GetAll()
    {
        var result = await _employeeService.GetAll();
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<ResponseData<List<MRes_Employee>>>> GetByStatus(int status)
    {
        var result = await _employeeService.GetListByStatus(status);
        return Ok(result);
    }
}
