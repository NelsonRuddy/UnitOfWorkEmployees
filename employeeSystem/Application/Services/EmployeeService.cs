using Contracts;
using Domain.Entities;
using employeeSystem.Api.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace Application.Services
{
    public class EmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                return await _unitOfWork.Employees.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new CustomException("An error occurred while fetching all employees", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                var employee = await _unitOfWork.Employees.GetByIdAsync(id);
                if (employee == null)
                {
                    throw new CustomException("Employee not found", HttpStatusCode.NotFound);
                }
                return employee;
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new CustomException("An error occurred while fetching the employee by ID", HttpStatusCode.InternalServerError);
            }
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            try
            {
                await _unitOfWork.Employees.AddAsync(employee);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new CustomException("An error occurred while adding the employee", HttpStatusCode.InternalServerError);
            }
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                var existingEmployee = await _unitOfWork.Employees.GetByIdAsync(employee.Id);
                if (existingEmployee == null)
                {
                    throw new CustomException("Employee does not exist", HttpStatusCode.NotFound);
                }

                await _unitOfWork.Employees.UpdateAsync(employee);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new CustomException("An error occurred while updating the employee", HttpStatusCode.InternalServerError);
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            try
            {
                var employee = await _unitOfWork.Employees.GetByIdAsync(id);
                if (employee == null)
                {
                    throw new CustomException("Employee does not exist", HttpStatusCode.NotFound);
                }

                await _unitOfWork.Employees.DeleteAsync(employee);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new CustomException("An error occurred while deleting the employee", HttpStatusCode.InternalServerError);
            }
        }
    }
}
