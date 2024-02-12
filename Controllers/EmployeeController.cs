using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Model;
using PrimeiraAPI.ViewModel;

namespace PrimeiraAPI.Controllers
{
    [ApiController]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException();
        }

        [HttpPost]
        public IActionResult Add(EmployeeViewModel employeeView)
        {
            var employee = new Employee(employeeView.Name, employeeView.Age,null);

            _employeeRepository.Add(employee);

            return Ok();

        }

        [HttpGet]
        public IActionResult Get()
        {
            DateTime agora = DateTime.Now;
            var employee = _employeeRepository.Get();
            var response = new { error = false ,employee};

            return Ok(response);
        }



    }
}
