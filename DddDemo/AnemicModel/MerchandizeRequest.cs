using System;
using DddDemo.Entities;
using DddDemo.ValueObjects;

namespace DddDemo.AnemicModel
{
    /// <summary>
    /// 1. Нет инкапсуляции
    /// 2. Не поддерживаются инварианты
    /// 3. Непонятно, какие действия я могу провести с этим объектом
    /// </summary>
    public class MerchandizeRequest
    {
        /// <summary>
        /// Идентификатор запроса
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Текущий статус запроса
        /// </summary>
        public MerchandizeRequestStatus Status { get; set; }

        /// <summary>
        /// Идентификатор сотрудника, которому предназначен мерч
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// Телефон для связи с сотрудником
        /// </summary>
        public PhoneNumber ContactPhone { get; set; }

        /// <summary>
        /// ID ответственного менеджера за выдачу мерча
        /// </summary>
        public long ResponsibleManagerId { get; set; }
    }

    public class MerchandizeRequestService
    {
        public MerchandizeRequest Create()
        {
            throw new NotImplementedException();
        }

        public MerchandizeRequest Assign()
        {
            throw new NotImplementedException();
        }

        // ......
    }
}
