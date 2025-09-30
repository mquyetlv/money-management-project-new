namespace money_management_service.Exceptions
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException() : base() {}

        public InsufficientBalanceException(string message) : base(message) {}

        public InsufficientBalanceException(string message, Exception internalException) : base(message, internalException) {} 
    }
}
