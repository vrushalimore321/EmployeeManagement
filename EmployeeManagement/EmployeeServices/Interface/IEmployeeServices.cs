using EmployeeModels.ViewModel;

namespace EmployeeServices.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeDetailView> GetAllEmployee();
        List<EmployeeDesignationView> GetDesignations();
        EmployeeDetailView GetEmployeeById(int? id);
        void DeleteEmployee(int? id);
        void UpdateEmployee(EmployeeDetailView empDetail);
        void AddEmployee(EmployeeDetailView empDetail);
    }
}