using System;
using DddDemo.Entities;
using DddDemo.ValueObjects;

namespace DddDemo.Isolation
{
    /// <summary>
    /// Неизолированная доменная модель
    /// </summary>
    public sealed class VolatileDependency : Entity
    {
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
        /// ID ответственного менеджера за выдачу мерча
        /// </summary>
        public long ResponsibleManagerId { get; private set; }

        /// <summary>
        /// Дата начала обработки запроса
        /// </summary>
        public DateTimeOffset StartedAt { get; private set; }

        /// <summary>
        /// Коллекция идентификаторов мерча
        /// </summary>
        public SkuList Items
        {
            get => (SkuList)_items;
            set => _items = value;
        }

        private string _items;

        public VolatileDependency()
            : base()
        {
            Id = 0;
            Status = MerchandizeRequestStatus.Draft;
        }

        public VolatileDependency(long employeeId, PhoneNumber contactPhone)
            : this()
        {
            EmployeeId = employeeId;
            ContactPhone = contactPhone ?? throw new Exception("Phone should not be null");
            Status = MerchandizeRequestStatus.Created;
        }

        public VolatileDependency(long employeeId, PhoneNumber contactPhone, long responsibleManagerId)
            : this(employeeId, contactPhone)
        {
            ResponsibleManagerId = responsibleManagerId;
            Status = MerchandizeRequestStatus.Assigned;
        }

        public VolatileDependency(long employeeId, PhoneNumber contactPhone, long responsibleManagerId, SkuList items)
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
        public void StartWork(SkuList items)
        {
            if (Status != MerchandizeRequestStatus.Assigned)
            {
                throw new Exception("Incorrect request status");
            }

            _items = items;
            Status = MerchandizeRequestStatus.InProgress;

            #region Проблема

            // Нарушение изоляции
            // Завимость не является ссылочно прозрачной
            // То есть если заменить вызов метода на результат работы этого метода,
            // то поведение программы не должно поменяться

            #endregion

            StartedAt = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Берем заявку на мерч в работу
        /// </summary>
        public void BetterStartWork(SkuList items, DateTimeOffset startedAt)
        {
            if (Status != MerchandizeRequestStatus.Assigned)
            {
                throw new Exception("Incorrect request status");
            }

            _items = items;
            Status = MerchandizeRequestStatus.InProgress;

            StartedAt = startedAt;
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
}
