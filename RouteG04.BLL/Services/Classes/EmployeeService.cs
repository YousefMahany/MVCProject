using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RouteG04.BLL.DTOS.Employee;
using RouteG04.BLL.Services.AttachmentsService;
using RouteG04.BLL.Services.Interfaces;
using RouteG04.DAL.Models.EmployeeModule;
using RouteG04.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteG04.BLL.Services.Classes
{
    public class EmployeeService( IUnitOfWork unitOfWork,IMapper mapper,IAttachmentService attachmentService) : IEmployeeService
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IAttachmentService _attachmentService = attachmentService;

        public IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking =false)
        {
            var Employees = _unitOfWork.EmployeeRepository.GetAll(WithTracking);
            var EmployeeDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(Employees);
            return EmployeeDto;
        }

        public EmployeeDetailsDto? GetEmployeeId(int id)
        {
            var Employee = _unitOfWork.EmployeeRepository.GetById(id);
           return Employee is null ? null : _mapper.Map<Employee,EmployeeDetailsDto>(Employee);
        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var Employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            if(employeeDto.Image is not null)
            {
                Employee.ImageName = _attachmentService.Upload(employeeDto.Image, "Images");
            }
             _unitOfWork.EmployeeRepository.Add(Employee);
            return _unitOfWork.SaveChanges();
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
             _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto));
            return _unitOfWork.SaveChanges();
        }
        public bool DeleteEmployee(int id)
        {
            var Employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (Employee is null) return false;
            else
            {
                Employee.IsDeleted = true;
                _unitOfWork.EmployeeRepository.Update(Employee);
                   return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }




    }
}
