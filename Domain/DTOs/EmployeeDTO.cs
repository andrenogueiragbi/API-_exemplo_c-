using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeiraAPI.Domain.DTOs
{
    public class EmployeeDTO
  {
        public int Id { get; set; }
        public string? NameEmployee { get; set; }
        public string? Photo { get; set; }
    }
}