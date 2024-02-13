using Microsoft.AspNetCore.Connections;
using PrimeiraAPI.Domain.DTOs;
using PrimeiraAPI.Model;

namespace PrimeiraAPI.Infraestrutura
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConnetionContext _context = new ConnetionContext();


        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public List<EmployeeDTO> Get()
        {
            return _context.Employees
            .Select(b =>
            
                new EmployeeDTO()
                {
                    Id = b.id,
                    NameEmployee = b.name,
                    Photo = b.photo

                })
            .ToList();
        }

        public List<Employee> Get(int pageNumber, int pageQuantity)
        {
            return _context.Employees.Skip(pageNumber * pageQuantity).Take(pageQuantity).ToList();
        }

        public Employee? Get(int id)
        {
            return _context.Employees.Find(id);
        }



    }
}