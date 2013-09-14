namespace mytracker.api.Exceptions
{
    public class DublicateUserException : ApiException
    {
        public DublicateUserException(string user, string email) 
            :base(ApiExceptionType.DublicateUser)
        {
            Values["User"] = user;
            Values["Email"] = email;
        }

        public string User { get { return Values["User"].ToString(); } }
        public string Email { get { return Values["Email"].ToString(); } }
    }

    public class UserNotFoundException : ApiException
    {
        public UserNotFoundException(string email) : base(ApiExceptionType.UserNotFound)
        {
            Values["Email"] = email;
        }

        public string Email { get { return Values["Email"].ToString(); } }
    }
}
