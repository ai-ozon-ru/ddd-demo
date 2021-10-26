using System;
using DddDemo.DomainServices;
using DddDemo.ValueObjects;

namespace DddDemo.Trilemma
{
    public sealed class ApplicationService
    {
        private readonly IRepository _repository;

        public ApplicationService(IRepository repository)
            => _repository = repository;

        public string ChangeEmail(long employeeId, string newEmail)
        {
            #region Новое требование

            #region проблема
            // Доменная модель не инкапсулирована
            // появился новый инвариант, что емейл должен быть уникальный
            // доменная модель не знает про этот инвариант
            #endregion
            var existingEmployee = _repository.FindEmployeeByEmail(newEmail);

            if (existingEmployee != null && existingEmployee.Id != employeeId)
            {
                return "Email is already taken";
            }

            #endregion

            var employee = _repository.FindEmployeeById(employeeId);
            employee.ChangeEmail(newEmail);
            _repository.Save(employee);

            return "OK";
        }

        public string ChangeEmailAlternative(long employeeId, string newEmail)
        {
            var employee = _repository.FindEmployeeById(employeeId);
            try
            {
                #region проблема
                // Модель инкапсулирована
                // Но модель не изолирована, так как она теперь знает про репозиторий
                #endregion
                employee.ChangeEmail(Email.Parse(newEmail), _repository);
                _repository.Save(employee);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "OK";
        }

        public string ChangeEmailAlternative2(long employeeId, string newEmail)
        {
            var allEmployees = _repository.GetAllEmployees();
            var employee = _repository.FindEmployeeById(employeeId);

            try
            {
                employee.ChangeEmail(Email.Parse(newEmail), allEmployees);
                _repository.Save(employee);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "OK";
        }
    }
}
