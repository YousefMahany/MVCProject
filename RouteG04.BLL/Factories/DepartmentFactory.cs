using RouteG04.BLL.DTOS;
using RouteG04.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.Factories
{
    public static class DepartmentFactory
    {
        //Get All
        public static DepartmentsDto ToDepartmentDto (this Department D)
        {
            return new DepartmentsDto()
            {
                DeptId = D.Id,
                Name = D.Name,
                Code = D.Code,
                DateOfCreation = D.CreatedOn
            };
        }
        //Get By Id
        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department D)
        {
            return new DepartmentDetailsDto()
            {
                DeptId = D.Id,
                Name = D.Name,
                Code = D.Code,
                Description = D.Description,
                DateOfCreation = D.CreatedOn,
                LastModificationBy = D.LastModificationBy,
                LastModificationOn = D.LastModificationOn,
                CreatedBy = D.CreatedBy,
                IsDeleted = D.IsDeleted,
            };
        }
        //Add
        public static Department ToEntity(this  CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation
            };
        }
        public static Department ToEntity(this  UpdatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateOfCreation
            };
        }
    }
}
