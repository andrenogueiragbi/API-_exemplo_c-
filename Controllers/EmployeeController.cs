using Microsoft.AspNetCore.Authorization;
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

        private readonly ILogger<EmployeeController> _logger;





        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            //_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public IActionResult Add([FromForm] EmployeeViewModel employeeView)
        {


            var filePath = Path.Combine("Storage", employeeView.Photo.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            employeeView.Photo.CopyTo(fileStream);

            var employee = new Employee(employeeView.Name, employeeView.Age, filePath);

            _employeeRepository.Add(employee);



            return Ok(new { error = false, message = "success in creating", employee = employee });

        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var employee = _employeeRepository.Get();
            var response = new { error = false, employee };

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetId(int id)
        {

            var employee = _employeeRepository.Get(id);

            if (employee != null) return Ok(employee);

            return Ok(new { error = false, message = $"Not exist id: {id} in database", employee });

        }


        [HttpGet]
        [Route("{id}/photo")]
        public IActionResult DownloadPhoto(int id)
        {
            var employee = _employeeRepository.Get(id);

            if (employee?.photo != null)
            {
                var dataBytes = System.IO.File.ReadAllBytes(employee.photo);
                return File(dataBytes, "image/png");
                //return Ok(new { error = false, message = "success in search", employee });


            }

            return Ok(new { error = false, message = $"Not exist id: {id} in database", employee });

        }

        [HttpGet]
        [Route("pagination")]
        public IActionResult GetPagination(int pageNumber, int pageQuantity)
        {

  
            var employee = _employeeRepository.Get(pageNumber, pageQuantity);


            return Ok(employee);
        }



    }
}
