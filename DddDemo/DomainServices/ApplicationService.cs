using System;
using System.Linq;

namespace DddDemo.DomainServices
{
    public sealed class ApplicationService
    {
        /// <summary>
        /// Интерфейс репозитория БД
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Интерфейс шины сообщений
        /// </summary>
        private readonly IMessageBus _messageBus;

        public ApplicationService(IRepository repository, IMessageBus messageBus)
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

            var managers = _repository.GetVacantManagers().ToList();

            try
            {
                var request = DomainService.CreateMerchandizeRequest(employee, managers);
                var responsibleManager = managers.First(m => m.Id == request.ResponsibleManagerId);

                _messageBus.Notify(new EmailMessage(responsibleManager.Email, "Надо выдать мерч"));
                _messageBus.Notify(new EmailMessage(employee.Email, "Вам будет выдан мерч"));

                return GiveMerchResult.Success(request.Status, "Task has been assigned to a manager", request.Id);
            }
            catch (Exception e)
            {
                return GiveMerchResult.Fail(e.Message);
            }
        }
    }
}
