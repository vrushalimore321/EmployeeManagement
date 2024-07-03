using Dapper;

using EmployeeModels.DbModel;

using EmployeeRepositorys.Interfaces;
using Microsoft.Data.SqlClient;

using System.Data;

namespace DatabaseAccess.EmployeeRepositorys
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string conn;

        public EmployeeRepository()
        {
            conn = "Data Source=localhost;Initial Catalog=Employee1;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        public List<EmployeeDesignation> GetDesignations()
        {
            using IDbConnection connection = new SqlConnection(conn);
            return connection.Query<EmployeeDesignation>("GetAllEmployeeDesignations", commandType: CommandType.StoredProcedure).AsList();
        }
        public List<EmployeeDetail> GetAllEmployee()
        {
            using IDbConnection connection = new SqlConnection(conn);
           var res= connection.Query<EmployeeDetail>("GetAllEmployeeDetails", commandType: CommandType.StoredProcedure).AsList();
            return res;
        }
        public EmployeeDetail GetEmployeeById(int? id)
        {
            var parameter = new DynamicParameters();
            using IDbConnection connection = new SqlConnection(conn);
            parameter.Add("@ID", id);
            var res= connection.QuerySingleOrDefault<EmployeeDetail>("GetEmployeeById", parameter, commandType: CommandType.StoredProcedure);
            return res; 
        }
        public List<EmployeeDetail> AddEmployee(EmployeeDetail employee)
        {
            var parameter = new DynamicParameters();
            using IDbConnection connection = new SqlConnection(conn);
            {
                parameter.Add("@Name", employee.Name);
                parameter.Add("@DesignationId", employee.DesignationId);
                parameter.Add("@ProfilePicture", employee.ProfilePicture);
                parameter.Add("@Salary", employee.Salary);
                parameter.Add("@DateOfBirth", employee.DateOfBirth);
                parameter.Add("@Email", employee.Email);
                parameter.Add("@Address", employee.Address);
                return connection.Query<EmployeeDetail>("AddEmployee", parameter, commandType: CommandType.StoredProcedure).ToList();

            }
        }
        public List<EmployeeDetail> UpdateEmployee(EmployeeDetail employee)
        {
            var parameter = new DynamicParameters();
            using IDbConnection connection = new SqlConnection(conn);
            {
                parameter.Add("@ID", employee.Id);
                parameter.Add("@Name", employee.Name);
                parameter.Add("@DesignationId", employee.DesignationId);
                parameter.Add("@ProfilePicture", employee.ProfilePicture);
                parameter.Add("@Salary", employee.Salary);
                parameter.Add("@DateOfBirth", employee.DateOfBirth);
                parameter.Add("@Email", employee.Email);
                parameter.Add("@Address", employee.Address);
                return connection.Query<EmployeeDetail>("UpdateEmployee", parameter, commandType: CommandType.StoredProcedure).ToList();

            }
        }
        public EmployeeDetail DeleteEmployee(int? id)
        {
            var parameter = new DynamicParameters();
            using IDbConnection connection = new SqlConnection(conn);
            parameter.Add("@ID", id);
            return connection.QuerySingleOrDefault<EmployeeDetail>("DeleteEmployee", parameter, commandType: CommandType.StoredProcedure);
        }


    }

}
