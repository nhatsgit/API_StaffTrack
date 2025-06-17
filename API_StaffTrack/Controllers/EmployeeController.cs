using API_StaffTrack.Data;
using API_StaffTrack.Data.EF;
using API_StaffTrack.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_StaffTrack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly MainDbContext _context;

    public EmployeesController(MainDbContext context)
    {
        _context = context;
    }

    // GET: api/employees
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _context.Employees
            .Where(e => e.Status == 1) 
            .OrderBy(e => e.Name)
            .ToListAsync();

        return Ok(employees);
    }
    [HttpPost("test-create-employee")]
    public async Task<IActionResult> Create()
    {
        int count = await _context.Employees.CountAsync();

        var employee = new Employee
        {
            Name = $"staff{count + 1}", 
            Email = $"staff{count + 1}@gmail.com",
            PhoneNumber = "0394778814",
            Department = "abbcb",
            Position = "position",
            JoinDate = DateOnly.FromDateTime(DateTime.Today),
            IsActive = true,
            Status = 1,
            CreatedAt = DateTime.Now,
            UpdateAt = null
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return Ok(employee);
    }

}
