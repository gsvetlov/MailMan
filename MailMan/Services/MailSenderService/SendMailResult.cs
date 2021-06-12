namespace MailMan.Services.MailSenderService
{
    public record SendMailResult
    {
        public SendMailResult() { }
        public SendMailResult(bool isSuccessfull, string message)
        {
            IsSuccessfull = isSuccessfull;
            Message = message;
        }

        public bool IsSuccessfull { get; init; }
        public string Message { get; init; }
    }
}