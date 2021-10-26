using System;
using System.Collections.Generic;
using System.Linq;
using DddDemo.Entities;

namespace DddDemo.DomainServices
{
    public sealed class DomainService
    {
        public static MerchandizeRequest CreateMerchandizeRequest(
            Employee employee, IEnumerable<Manager> managers)
        {
            var request = new MerchandizeRequest(employee.Id, employee.PhoneNumber);

            if (!managers.Any(m => m.CanHandleNewTask))
            {
                throw new Exception("No vacant managers");
            }

            var responsibleManager = managers.OrderBy(m => m.AssignedTasks).First();

            request.AssignTo(responsibleManager.Id);
            responsibleManager.AssignTask();

            return request;
        }
    }
}
