using DddDemo.Entities;

namespace DddDemo.DomainServices
{
    public record GiveMerchResult
    {
        public bool IsSuccess { get; private init; }
        public MerchandizeRequestStatus? Status { get; private init; }
        public string Message { get; private init; }
        public long? RequestId { get; private init; }

        public static GiveMerchResult Fail(string errorMessage)
            => new GiveMerchResult
            {
                IsSuccess = false,
                Message = errorMessage,
            };

        public static GiveMerchResult Success(MerchandizeRequestStatus status, string message, long requestId)
            => new GiveMerchResult
            {
                IsSuccess = true,
                Status = status,
                Message = message,
                RequestId = requestId,
            };

    }
}
