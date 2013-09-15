namespace mytracker.api.Exceptions
{
    public class DublicateIssueNumberException : ApiException
    {
        public DublicateIssueNumberException(int number)
            : base(ApiExceptionType.DublicateIssueNumber)
        {
            Values["Number"] = number;
        }

        public int Number { get { return (int) Values["Number"]; } }
    }
}
