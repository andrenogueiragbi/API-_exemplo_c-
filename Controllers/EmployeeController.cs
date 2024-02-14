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

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] EmployeeViewModel employeeView)
        {


            var filePath = Path.Combine("Storage", employeeView.Photo.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            employeeView.Photo.CopyTo(fileStream);

            var employee = new Employee(employeeView.Name, employeeView.Age, filePath);

            _employeeRepository.Add(employee);


            _logger.Log(LogLevel.Information, "CRIAÇÂO DE FUNCIONÁRIO COM SUCESSO");
            return Ok(new { error = false, message = "success in creating", employee = employee });

        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            var employee = _employeeRepository.Get();
            var response = new { error = false, employee };

            _logger.Log(LogLevel.Information, "BUSCA DE TODOS OS FUNCIONÁRIO COM SUCESSO");

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetId(int id)
        {

            var employee = _employeeRepository.Get(id);

            _logger.Log(LogLevel.Information, "BUSCA DE UM FUNCIONÁRIO COM SUCESSO");

            if (employee != null) return Ok(employee);

            return Ok(new { error = false, message = $"Not exist id: {id} in database", employee });

        }

        [Authorize]
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

            _logger.Log(LogLevel.Information, "BUSCA DE FOTO DO FUNCIONÁRIO COM SUCESSO");

            return Ok(new { error = false, message = $"Not exist id: {id} in database", employee });

        }

        [Authorize]
        [HttpGet]
        [Route("pagination")]
        public IActionResult GetPagination(int pageNumber, int pageQuantity)
        {


            var employee = _employeeRepository.Get(pageNumber, pageQuantity);

            _logger.Log(LogLevel.Information, "BUSCA DE PAGINAÇÃO EM FUNCIONÁRIO COM SUCESSO");

            return Ok(employee);
        }



    }
}
