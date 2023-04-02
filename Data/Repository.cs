using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ordermanager_dotnet.Models;

namespace ordermanager_dotnet.Data
{
    public class Repository : IRepository
    {
        public ApplicationDbContext _context { get; }
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<Manufacturer[]> GetAllManufacturersAsync()
        {
            IQueryable<Manufacturer> query = _context.Manufacturers;
            query = query.AsNoTracking().OrderBy(manufacturer => manufacturer.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Manufacturer> GetManufacturerAsyncById(int ManufacturerId)
        {
            IQueryable<Manufacturer> query = _context.Manufacturers;
            query = query.AsNoTracking().OrderBy(manufacturer => manufacturer.Id).Where(manufacturer => manufacturer.Id == ManufacturerId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<ModelMachine[]> GetAllModelAsync(bool includeManufacturer = false)
        {
            IQueryable<ModelMachine> query = _context.ModelsMachine;
            if (includeManufacturer)
            {
                query = query.Include(ma => ma.Manufacturers);
            }
            query = query.AsNoTracking().OrderBy(model => model.Id);
            return await query.ToArrayAsync();
        }

        public async Task<ModelMachine> GetModelAsyncById(int ModelMachineId, bool includeManufacturer)
        {
            IQueryable<ModelMachine> query = _context.ModelsMachine;
            if (includeManufacturer)
            {
                query = query.Include(ma => ma.Manufacturers);
            }
            query = query.AsNoTracking().OrderBy(model => model.Id).Where(model => model.Id == ModelMachineId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<Machine[]> GetAllMachineAsync(bool includeModelMachine = false)
        {
            IQueryable<Machine> query = _context.Machines;
            if (includeModelMachine)
            {
                query = query.Include(modelmachine => modelmachine.ModelsMachine);
            }
            query = query.AsNoTracking().OrderBy(machine => machine.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Machine> GetMachineAsyncById(int MachineId, bool includeModelMachine)
        {
            IQueryable<Machine> query = _context.Machines;
            if (includeModelMachine)
            {
                query = query.Include(modelmachine => modelmachine.ModelsMachine);
            }
            query = query.AsNoTracking().OrderBy(machine => machine.Id).Where(machine => machine.Id == MachineId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Employee[]> GetAllEmployeesAsync()
        {
            IQueryable<Employee> query = _context.Employees;
            query = query.AsNoTracking().OrderBy(employee => employee.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Employee> GetEmployeeAsyncById(int EmployeeId)
        {
            IQueryable<Employee> query = _context.Employees;
            query = query.AsNoTracking().OrderBy(employee => employee.Id).Where(employee => employee.Id == EmployeeId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Provider[]> GetAllProvidersAsync()
        {
            IQueryable<Provider> query = _context.Providers;
            query = query.AsNoTracking().OrderBy(provider => provider.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Provider> GetProviderAsyncById(int ProviderId)
        {
            IQueryable<Provider> query = _context.Providers;
            query = query.AsNoTracking().OrderBy(provider => provider.Id).Where(provider => provider.Id == ProviderId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<AgriculturalInput[]> GetAllAgriculturalInputAsync(bool includeProvider = false)
        {
            IQueryable<AgriculturalInput> query = _context.AgriculturalInputs;
            if (includeProvider)
            {
                query = query.Include(provider => provider.Providers);
            }
            query = query.AsNoTracking().OrderBy(agriculturalinput => agriculturalinput.Id);
            return await query.ToArrayAsync();
        }

        public async Task<AgriculturalInput> GetAgriculturalInputAsyncById(int AgriculturalInputId, bool includeProvider)
        {
            IQueryable<AgriculturalInput> query = _context.AgriculturalInputs;
            if (includeProvider)
            {
                query = query.Include(provider => provider.Providers);
            }
            query = query.AsNoTracking().OrderBy(agriculturalinput => agriculturalinput.Id).Where(agriculturalinput => agriculturalinput.Id == AgriculturalInputId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Order[]> GetAllOrderAsync(bool includeMachine, bool includeEmployee, bool includeAgriculturalInput)
        {
            IQueryable<Order> query = _context.Orders;
            if (includeMachine && includeEmployee && includeAgriculturalInput)
            {
                query = query.Include(machine => machine.Machines);
                query = query.Include(employee => employee.Employees);
                query = query.Include(agriculturalInput => agriculturalInput.AgriculturalInputs);
            }
            query = query.AsNoTracking().OrderBy(order => order.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Order> GetOrderAsyncById(int OrderId, bool includeMachine, bool includeEmployee, bool includeAgriculturalInput)
        {
            IQueryable<Order> query = _context.Orders;
            if (includeMachine && includeEmployee && includeAgriculturalInput)
            {
                query = query.Include(machine => machine.Machines);
                query = query.Include(employee => employee.Employees);
                query = query.Include(agriculturalInput => agriculturalInput.AgriculturalInputs);
            }
            query = query.AsNoTracking().OrderBy(order => order.Id).Where(order => order.Id == OrderId);
            return await query.FirstOrDefaultAsync();
        }
    }
}
