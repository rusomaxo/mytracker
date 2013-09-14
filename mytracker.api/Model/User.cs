using System;

namespace mytracker.api.Model
{
    /// <summary>
    /// User
    /// </summary>
    public class User :Entity<Guid>
    {
        /// <summary>
        /// display user name
        /// </summary>
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// e-mail (login)
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// password + salt hash
        /// </summary>
        public virtual string Hash { get; set; }

        /// <summary>
        /// user blocked or not
        /// </summary>
        public virtual bool IsBlocked { get; set; }
    }
}
