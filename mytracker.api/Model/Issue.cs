using System;

namespace mytracker.api.Model
{
    /// <summary>
    /// issue
    /// </summary>
    public class Issue : Entity<Guid>
    {
        public virtual string Subject { get; set; }

        public virtual string Number { get; set; }
    }
}
