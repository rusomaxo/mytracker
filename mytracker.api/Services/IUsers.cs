using mytracker.api.Model;

namespace mytracker.api.Services
{
    public interface IUsers
    {
        /// <summary>
        /// Register new user. If name or email exists - throw DublicateUserException
        /// </summary>
        /// <param name="name">user display name</param>
        /// <param name="password">password</param>
        /// <param name="email">e-mail address</param>
        /// <returns>registered user</returns>
        User Register(string name, string password, string email);

        /// <summary>
        /// Block existing user by e-mail. Throw UserNotFound exception when user not found.
        /// </summary>
        /// <param name="email">E-mail address</param>
        void Block(string email);

        /// <summary>
        /// Unblock existing user by e-mail. Throw UserNotFound exception when user not found.
        /// </summary>
        /// <param name="email">E-mail address</param>
        void Unblock(string email);

        /// <summary>
        /// Validate password for user. 
        /// </summary>
        /// <param name="email">user e-mail</param>
        /// <param name="password">user password</param>
        /// <returns>if the password and e-mail is correct then returns true. If e-mail not found or email is blocked or password incorrect then returns false
        /// </returns>
        bool Validate(string email, string password);

        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="email">e-mail</param>
        /// <param name="password">old password</param>
        /// <param name="newPassword">new password</param>
        /// <returns>if old password and e-mail is correct then returns true. If e-mail not found or email is blocked or password incorrect then returns false</returns>
        bool ChangePassword(string email, string password, string newPassword);
    }
}
