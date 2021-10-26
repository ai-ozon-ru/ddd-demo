using System.Collections.Generic;

namespace DddDemo.DomainServices
{
    public interface IRepository
    {
        /// <summary>
        /// Получить сотрудника по email
        /// </summary>
        Employee FindEmployeeByEmail(string email);

        /// <summary>
        /// Получить список вакантных менеджеров
        /// </summary>
        IEnumerable<Manager> GetVacantManagers();

        /// <summary>
        /// Поиск сотрудника по ID
        /// </summary>
        Employee FindEmployeeById(long id);

        /// <summary>
        /// Сохраняет сотрудника в БД
        /// </summary>
        void Save(Employee employee);

        /// <summary>
        /// Возвращает всех-всех сотрудников
        /// </summary>
        ICollection<Employee> GetAllEmployees();
    }
}
