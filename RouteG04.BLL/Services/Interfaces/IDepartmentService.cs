using RouteG04.BLL.DTOS;
using RouteG04.BLL.DTOS.Department;

namespace RouteG04.BLL.Services.Interfaces
{
    public interface IDepartmentService
    {
        int AddDepartment(CreatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentsDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int UpdateDepartment(UpdatedDepartmentDto departmentDto);
    }
}