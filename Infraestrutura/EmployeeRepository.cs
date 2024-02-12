using Microsoft.AspNetCore.Connections;
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

        public List<Employee> Get()
        {
           return _context.Employees.ToList();
        }
    }
}