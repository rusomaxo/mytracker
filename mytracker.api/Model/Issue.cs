using System;

namespace mytracker.api.Model
{
    /// <summary>
    /// issue
    /// </summary>
    public class Issue : Entity<Guid>
    {
        /// <summary>
        /// issue subject
        /// </summary>
        public virtual string Subject { get; set; }

        /// <summary>
        /// issue description
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// issue number
        /// </summary>
        public virtual int Number { get; set; }

        /// <summary>
        /// issue author
        /// </summary>
        public virtual User Author { get; set; }
    }
}
