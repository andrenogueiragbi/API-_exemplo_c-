using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeiraAPI.ViewModel
{
    public class EmployeeViewModel
    {
        public required string Name { get; set; }
        public int Age { get; set; }

        public string? photo { get; set; }


    }
}