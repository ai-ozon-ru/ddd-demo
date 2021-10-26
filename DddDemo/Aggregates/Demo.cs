using DddDemo.Entities;

namespace DddDemo.Aggregates
{
    /// <summary>
    /// Сущность заказ
    /// </summary>
    public class Order : Entity
    {
        public OrderStatus Status { get; private set; }
        public string Number { get; private set; }
        public bool IsReadyForShipment { get; private set; }

        // other properties

        /// <summary>
        /// Помечаем заказ как оплаченый
        /// </summary>
        public void SetPaid()
        {
            // ...

            Status = OrderStatus.Paid;
            IsReadyForShipment = true;

            // ...
        }
    }

    /// <summary>
    /// Сущность платеж
    /// </summary>
    public class Payment : Entity
    {
        public PaymentStatus Status { get; private set; }
        public Card PaidByCard { get; private set; }

        // other properties

        public void Authorize(Card card)
        {
            // ...

            Status = PaymentStatus.Authorized;
            PaidByCard = card;

            // ...
        }

        public void Cancel() { }
    }

    /// <summary>
    /// Защита инварианта
    /// </summary>
    public class OrderAggregate
    {
        public Payment Payment { get; private set; }
        public Order Order { get; private set; }
        private readonly IMediator _mediator;

        public void PayByCard(Card card)
        {
            Payment.Authorize(card);
            if (Payment.Status == PaymentStatus.Authorized)
            {
                Order.SetPaid();
                _mediator.Send(new OrderPaidEvent());
            }
            else { /*...*/ }
        }
    }

    public class OrderPaidEvent
    {

    }


    #region Other objects

    public class Card
    {
        public string Pan { get; set; }
    }

    public enum OrderStatus
    {
        New,
        Issued,
        Paid,
        Delivered,
        // .........
    }

    public enum PaymentStatus
    {
        New,
        Authorized,
        Cancelled,
    }

    internal interface IMediator
    {
        void Send(object anyEvent);
    }

    #endregion

}
