using EmployeesHrApi.Data;
using EmployeesHrApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EmployeesHrApi.Controllers;

public class EmployeesController : ControllerBase
{

    private readonly EmployeeDataContext _context;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(EmployeeDataContext context, ILogger<EmployeesController> logger)
    {
        _context = context;
        _logger = logger;
    }


    //GET /employees/3
    [HttpGet("/employees/{employeeId:int}")]
    public async Task<ActionResult> GetAnEmployeeAsync(int employeeId)
    {
        _logger.LogInformation("Got the following employeeId {0}", employeeId);
        var employee = await _context.Employees
            .Where(e => e.Id == employeeId)
            .SingleOrDefaultAsync();

        if (employee is null)
        {
            return NotFound(); // 404
        }
        else
        {
            return Ok(employee);
        }

    }


    // GET /employees
    [HttpGet("/employees")]
    public async Task<ActionResult<EmployeesResponseModel>> GetEmployeesAsync([FromQuery] string department = "All")
    {
        var employees = await _context.GetEmployeesByDepartment(department) //this waits for the context of GetEmployeesByDeparment in the EmployeeDataContext.cs file then runs over it's code
            .Select(emp => new EmployeesSummaryResponseModel
            {
                Id = emp.Id.ToString(),
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Department = emp.Department,
                Email = emp.Email,
            })
            .ToListAsync(); //runs the query at this point
        // TODO: Get Back to This

        var response = new EmployeesResponseModel
        {
            Employees = employees,
            ShowingDepartment = department
        };
        return Ok(response);
    }

}