using Microsoft.EntityFrameworkCore;
using ordermanager_dotnet.Entities;
using ordermanager_dotnet.Models;

namespace ordermanager_dotnet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
        public DbSet<User> Users {get;set;}

        public DbSet<Manufacturer> Manufacturers {get;set;}

        public DbSet<ModelMachine> ModelsMachine {get;set;}

        public DbSet<Machine> Machines {get;set;}

        public DbSet<Employee> Employees {get;set;}

        public DbSet<Provider> Providers {get;set;}

        public DbSet<AgriculturalInput> AgriculturalInputs {get;set;}

        public DbSet<Order> Orders {get;set;}
    }
}
