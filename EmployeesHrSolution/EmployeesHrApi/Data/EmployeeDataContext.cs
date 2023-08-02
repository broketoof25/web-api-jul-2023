using Microsoft.EntityFrameworkCore;

namespace EmployeesHrApi.Data;

public class EmployeeDataContext : DbContext
{
    public EmployeeDataContext(DbContextOptions<EmployeeDataContext> options) : base(options)
    {

    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<HiringRequests> HiringRequests { get; set; }

    /// <summary>
    /// This method returns an IQueryable that knows how to get just the employees in a department, or all of them.
    /// </summary>
    /// <param name="department"></param>
    /// <returns></returns>
    public IQueryable<Employee> GetEmployeesByDepartment(string department)
    {
        if (department != "All")
        {
            return Employees.Where(e => e.Department == department);
        }
        else
        {
            return Employees;
        }
    }
}
