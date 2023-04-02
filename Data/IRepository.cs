using System.Threading.Tasks;
using ordermanager_dotnet.Models;

namespace ordermanager_dotnet.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveChangesAsync();

        //Manufacturer
        Task<Manufacturer[]> GetAllManufacturersAsync();
        Task<Manufacturer> GetManufacturerAsyncById(int ManufacturerId);

        //Model
        Task<ModelMachine[]> GetAllModelAsync(bool includeManufacturer);
        Task<ModelMachine> GetModelAsyncById(int ModelMachineId, bool includeManufacturer);

        //Machine
        Task<Machine[]> GetAllMachineAsync(bool includeModelMachine);
        Task<Machine> GetMachineAsyncById(int MachineId, bool includeModelMachine);

        //Employee
        Task<Employee[]> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeAsyncById(int EmployeeId);

        //Provider
        Task<Provider[]> GetAllProvidersAsync();
        Task<Provider> GetProviderAsyncById(int ProviderId);

        //AgriculturalInput
        Task<AgriculturalInput[]> GetAllAgriculturalInputAsync(bool includeProvider);
        Task<AgriculturalInput> GetAgriculturalInputAsyncById(int AgriculturalInputId, bool includeProvider);

        //Order
        Task<Order[]> GetAllOrderAsync(bool includeMachine, bool includeEmployee, bool includeAgriculturalInput);
        Task<Order> GetOrderAsyncById(int OrderId, bool includeMachine, bool includeEmployee, bool includeAgriculturalInput);
    }
}
