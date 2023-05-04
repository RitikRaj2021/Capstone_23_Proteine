using CapstoneTest2.Data;
using CapstoneTest2.Models;
using CapstoneTest2.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneTest2.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext mvcTestDBContext;

        public EmployeesController(ApplicationDbContext mvcTestDBContext)
        {
            this.mvcTestDBContext = mvcTestDBContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
            };

            await mvcTestDBContext.Employees.AddAsync(employee);
            await mvcTestDBContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }
    }
}
