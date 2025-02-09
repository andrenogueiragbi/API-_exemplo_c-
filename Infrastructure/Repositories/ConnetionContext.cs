using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Domain.Model.CompanyAggregate;

using PrimeiraAPI.Model;

namespace PrimeiraAPI.Infraestrutura
{
    public class ConnetionContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Company> Company { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder.UseNpgsql(
                "Server=177.38.179.2;" +
                "Port=5432;Database=treinee;" +
                "User Id=andre;" +
                "Password=090909;");
    }
}