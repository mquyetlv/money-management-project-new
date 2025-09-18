namespace money_management_service.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string Message) : base(Message) { }
        public NotFoundException(string Message, Exception InnerException) : base(Message, InnerException) { }
    }
}
