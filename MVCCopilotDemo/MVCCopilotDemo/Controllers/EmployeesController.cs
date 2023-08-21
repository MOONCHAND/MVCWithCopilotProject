using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCopilotDemo.Data;
using MVCCopilotDemo.Models;
using MVCCopilotDemo.Models.Domain;

namespace MVCCopilotDemo.Controllers
{
    public class EmployeesController : Controller
    {
        // create a constructor that takes a MVCCopilotDbContext object as a parameter
        private readonly MVCCopilotDbContext _dbContext;
        public EmployeesController(MVCCopilotDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // add a get method keyword
        [HttpGet]
        // add a functionality of Index action method which can get list of all the data from the database and display it in the view
        public async Task<IActionResult> Index()
        {
            // get the list of all the employees from the database
            var employees = await _dbContext.Employees.ToListAsync();
            // return the list of employees to the view
            return View(employees);
        }
        // add a functionality to add a new employee
        //add a get method keyword
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        //add a post method keyword
        [HttpPost]

        // add a functionality to add a new employee as async method which can post the data to the database

        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                EmployeeId =Guid.NewGuid(),
                EmployeeName = addEmployeeRequest.EmployeeName,
                EmployeeEmail = addEmployeeRequest.EmployeeEmail,
                EmployeeSalary = addEmployeeRequest.EmployeeSalary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department
            };
            // add the employee to the database
            await _dbContext.Employees.AddAsync(employee);
            // save the changes
            await _dbContext.SaveChangesAsync();
            // redirect to the Index action
            return RedirectToAction("Index");      
        }
        // add a get method keyword
        [HttpGet]
        // add a functionality of View method to view the details of an employee
        public async Task<IActionResult> View(Guid id)
        {
            // get the employee from the database
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            // create a if condition and it will till employee gets null
            if(employee != null)
            {
                //create a new view model object
                var employeeViewModel = new UpdateEmployeeViewModel()
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    EmployeeEmail = employee.EmployeeEmail,
                    EmployeeSalary = employee.EmployeeSalary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department
                };
                // return the employee to the view
                return await Task.Run(() =>View("View",employeeViewModel));
            }
           
            // return redirect to action Index method
            return RedirectToAction("Index");
        }
        // add a post method keyword
        [HttpPost]
        // add a functionality to update the details of an employee
        public async Task<IActionResult> View(UpdateEmployeeViewModel updateEmployeeRequest)
        {
            // get the employee from the database
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == updateEmployeeRequest.EmployeeId);
            // create a if condition and it will run till employee gets null
            if (employee != null)
            {
                // update the employee details
                employee.EmployeeName = updateEmployeeRequest.EmployeeName;
                employee.EmployeeEmail = updateEmployeeRequest.EmployeeEmail;
                employee.EmployeeSalary = updateEmployeeRequest.EmployeeSalary;
                employee.DateOfBirth = updateEmployeeRequest.DateOfBirth;
                employee.Department = updateEmployeeRequest.Department;
                // update the employee to the database
                _dbContext.Employees.Update(employee);
                // save the changes
                await _dbContext.SaveChangesAsync();
                // return redirect to action Index method
                return RedirectToAction("Index");
            }
            // return redirect to action Index method
            return RedirectToAction("Index");
        }
        // add a post method keyword
        [HttpPost]
        // add a functionality to delete the details of an employee



        public async Task<IActionResult> Delete(UpdateEmployeeViewModel employeeViewModel)
        {
            // get the employee from the database
            var employee = await _dbContext.Employees.FindAsync(employeeViewModel.EmployeeId);
            // create a if condition and it will till employee gets null
            if (employee != null)
            {
                // delete the employee from the database
                _dbContext.Employees.Remove(employee);
                // save the changes
                await _dbContext.SaveChangesAsync();
                // return redirect to action Index method
                return RedirectToAction("Index");
            }
            // return redirect to action Index method
            return RedirectToAction("Index");
        }







       



    }
}
