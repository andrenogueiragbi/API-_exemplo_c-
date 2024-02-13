using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeiraAPI.ViewModel
{
    public class EmployeeViewModel
    {
        public required string Name { get; set; }
        public required  int Age { get; set; }

        public required IFormFile Photo { get; set; }


    }
}