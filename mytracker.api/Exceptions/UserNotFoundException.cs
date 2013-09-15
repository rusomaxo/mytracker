namespace mytracker.api.Exceptions
{
    public class UserNotFoundException : ApiException
    {
        public UserNotFoundException(string email) : base(ApiExceptionType.UserNotFound)
        {
            Values["Email"] = email;
        }

        public string Email { get { return Values["Email"].ToString(); } }
    }
}