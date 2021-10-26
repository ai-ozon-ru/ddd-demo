using System.Linq;
using DddDemo.Entities;

namespace DddDemo.DomainServices
{
    public sealed class DemoService
    {
        /// <summary>
        /// Интерфейс репозитория БД
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Интерфейс шины сообщений
        /// </summary>
        private readonly IMessageBus _messageBus;

        public DemoService(IRepository repository, IMessageBus messageBus)
        {
            _repository = repository;
            _messageBus = messageBus;
        }

        /// <summary>
        /// Создать запрос на выдачу мерча новому сотруднику
        /// </summary>
        public GiveMerchResult GiveMerchForNewEmployee(string newEmployeeEmail)
        {
            var employee = _repository.FindEmployeeByEmail(newEmployeeEmail);

            if (employee == null)
            {
                return GiveMerchResult.Fail("Employee not found");
            }

            var request = new MerchandizeRequest(employee.Id, employee.PhoneNumber);

            var managers = _repository.GetVacantManagers().ToList();

            if (!managers.Any(m => m.AssignedTasks < 3))
            {
                return GiveMerchResult.Fail("No vacant managers");
            }

            var responsibleManager = managers.OrderBy(m => m.AssignedTasks).First();

            request.AssignTo(responsibleManager.Id);
            responsibleManager.AssignTask();

            _messageBus.Notify(new EmailMessage(responsibleManager.Email, "Надо выдать мерч"));
            _messageBus.Notify(new EmailMessage(employee.Email, "Вам будет выдан мерч"));

            return GiveMerchResult.Success(request.Status, "Task has been assigned to a manager", request.Id);
        }
    }
}
