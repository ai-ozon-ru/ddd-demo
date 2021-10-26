using DddDemo.Entities;
using DddDemo.ValueObjects;

namespace DddDemo.Isolation
{
    /// <summary>
    /// Паттерн ActiveRecord
    /// Минусы:
    ///  - доменная модель не изолирована
    ///  - смешиваются два аспекта приложения: бизнес-логика и работа с БД
    /// Плюсы:
    ///  + подоходит для небольших проектов и простых моделей
    /// </summary>
    public class ActiveMerchandizeRequest
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

        private readonly IDataBaseConnection _dataBaseConnection;

        public ActiveMerchandizeRequest(IDataBaseConnection dataBaseConnection)
        {
            _dataBaseConnection = dataBaseConnection;
        }

        public void Save()
        {
            _dataBaseConnection.Execute("INSERT INTO...");
        }

        public void Restore(long id)
        {
            var dto = _dataBaseConnection.Execute("SELECT * FROM ...");
            Id = id;
            //Status = dto.Status;
            //EmployeeId = dto.EmployeeId;
            //...
        }
    }

    public interface IDataBaseConnection
    {
        object Execute(string sql);
    }
}
