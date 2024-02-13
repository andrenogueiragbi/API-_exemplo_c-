using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeiraAPI.Domain.DTOs;

namespace PrimeiraAPI.Model
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);

        List<EmployeeDTO> Get();

        List<Employee> Get(int pageNumber, int pageQuantity);

         Employee? Get(int id);

      
    }
}