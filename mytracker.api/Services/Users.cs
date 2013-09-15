using System.Linq;
using mytracker.api.DataAccess;
using mytracker.api.Exceptions;
using mytracker.api.Model;
using mytracker.api.Security;

namespace mytracker.api.Services
{
    public class Users : IUsers
    {
        private readonly IDataSession _session;

        public Users(IDataSession session)
        {
            _session = session;
        }


        /// <summary>
        /// Register new user. If name or email exists - throw DublicateUserException
        /// </summary>
        /// <param name="name">user display name</param>
        /// <param name="password">password</param>
        /// <param name="email">e-mail address</param>
        /// <returns>registered user</returns>
        public User Register(string name, string password, string email)
        {
            //check user exists
            if (_session.Query<User>().Any(u => u.DisplayName.ToLower() == name.ToLower() || u.Email.ToLower() == email.ToLower()))
                throw new DublicateUserException(name, email);

            //generate password hash
            var hash = PasswordHash.CreateHash(password);

            var user = new User
                           {
                               Email = email,
                               Hash = hash,
                               DisplayName = name,
                               IsBlocked = false
                           };

            _session.SaveOrUpdate(user);

            return user;
        }

        /// <summary>
        /// Block existing user by e-mail. Throw UserNotFound exception when user not found.
        /// </summary>
        /// <param name="email">E-mail address</param>
        public void Block(string email)
        {
            var user = FindUser(email);
            
            user.IsBlocked = true;

            _session.SaveOrUpdate(user);
        }

        /// <summary>
        /// Unblock existing user by e-mail. Throw UserNotFound exception when user not found.
        /// </summary>
        /// <param name="email">E-mail address</param>
        public void Unblock(string email)
        {
            var user = FindUser(email);
            
            user.IsBlocked = false;

            _session.SaveOrUpdate(user);
        }


        /// <summary>
        /// Validate password for user. 
        /// </summary>
        /// <param name="email">user e-mail</param>
        /// <param name="password">user password</param>
        /// <returns>if the password and e-mail is correct then returns true. If e-mail not found or email is blocked or password incorrect then returns false
        /// </returns>
        public bool Validate(string email, string password)
        {
            var user = FindValidUser(email, password);

            return user != null;
        }

        public bool ChangePassword(string email, string password, string newPassword)
        {
            var user = FindValidUser(email, password);

            if(user == null) return false;

            user.Hash = PasswordHash.CreateHash(newPassword);
            _session.SaveOrUpdate(user);

            return true;
        }

        /// <summary>
        /// find user by email address. throw UserNotFoundException if user not founded.
        /// </summary>
        /// <param name="email">email address</param>
        /// <returns>user</returns>
        private User FindUser(string email)
        {
            var user = _session.Query<User>().FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (user == null) throw new UserNotFoundException(email);

            return user;
        }

        /// <summary>
        /// find user with valid password
        /// </summary>
        /// <param name="email">email address</param>
        /// <param name="password">password</param>
        /// <returns>user</returns>
        private User FindValidUser(string email, string password)
        {
            return _session.Query<User>().FirstOrDefault(
                    u => u.Email.ToLower() == email.ToLower() 
                        && PasswordHash.ValidatePassword(password, u.Hash)
                        && !u.IsBlocked);
        }

    }
}
