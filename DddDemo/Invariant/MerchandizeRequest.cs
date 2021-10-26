using System;
using System.Collections.Generic;
using DddDemo.ValueObjects;

namespace DddDemo.Invariant
{
    /// <summary>
    /// Запрос на выдачу мерча
    /// </summary>
    public sealed class MerchandizeRequest
    {
        /// <summary>
        /// Уникальный идентификатор запроса
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Текущий статус запроса
        /// </summary>
        public MerchandizeRequestStatus Status { get; private set; }

        /// <summary>
        /// Идентификатор сотрудника, которому предназначен мерч
        /// </summary>
        public long EmployeeId { get; private set; }

        /// <summary>
        /// Телефон для связи с сотрудником
        /// </summary>
        public PhoneNumber ContactPhone { get; private set; }

        /// <summary>
        /// ID ответсвенного менеджера за выдачу мерча
        /// </summary>
        public long ResponsibleManagerId { get; private set; }

        /// <summary>
        /// Коллекция идентификаторов мерча
        /// </summary>
        public IReadOnlyCollection<string> Items => _items;

        private List<string> _items = new ();

        public MerchandizeRequest()
        {
            Id = new Guid();
            Status = MerchandizeRequestStatus.Draft;
        }

        public MerchandizeRequest(long employeeId, PhoneNumber contactPhone)
            : this()
        {
            EmployeeId = employeeId;
            ContactPhone = contactPhone ?? throw new Exception("Phone should not be null");
            Status = MerchandizeRequestStatus.Created;
        }

        public MerchandizeRequest(long employeeId, PhoneNumber contactPhone, long responsibleManagerId)
            : this(employeeId, contactPhone)
        {
            ResponsibleManagerId = responsibleManagerId;
            Status = MerchandizeRequestStatus.Assigned;
        }

        public MerchandizeRequest(long employeeId, PhoneNumber contactPhone, long responsibleManagerId,
            List<string> items)
            : this(employeeId, contactPhone, responsibleManagerId)
        {
            ResponsibleManagerId = responsibleManagerId;
            Status = MerchandizeRequestStatus.InProgress;
            _items = items;
        }

        /// <summary>
        /// Создаем заявку для конкретного сотрудника
        /// </summary>
        public void Create(long employeeId, PhoneNumber contactPhone)
        {
            if (Status != MerchandizeRequestStatus.Draft)
            {
                throw new Exception("Incorrect request status");
            }

            EmployeeId = employeeId;
            ContactPhone = contactPhone ?? throw new Exception("Phone should not be null");
            Status = MerchandizeRequestStatus.Created;
        }

        /// <summary>
        /// Назначаем заявку на мерч ответственному менеджеру
        /// Если заявка не в статусе Created, то выбрасываем исключение
        /// </summary>
        public void AssignTo(long responsibleManagerId)
        {
            if (Status != MerchandizeRequestStatus.Created)
            {
                throw new Exception("Incorrect request status");
            }

            Status = MerchandizeRequestStatus.Assigned;
            ResponsibleManagerId = responsibleManagerId;
        }

        /// <summary>
        /// Берем заявку на мерч в работу
        /// </summary>
        public void StartWork(List<string> items)
        {
            if (Status != MerchandizeRequestStatus.Assigned)
            {
                throw new Exception("Incorrect request status");
            }

            _items = items;
            Status = MerchandizeRequestStatus.InProgress;
        }

        /// <summary>
        /// Завершить работу по заявке
        /// </summary>
        public void Complete()
        {
            if (Status != MerchandizeRequestStatus.InProgress)
            {
                throw new Exception("Incorrect request status");
            }

            Status = MerchandizeRequestStatus.Done;
        }
    }

    public enum MerchandizeRequestStatus
    {
        Draft,
        Created,
        Assigned,
        InProgress,
        Done,
    }
}
