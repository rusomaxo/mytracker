namespace mytracker.api.Model
{
    public class Entity<T>
    {
        public virtual T Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if(obj is Entity<T>)
            {
                return Equals((Entity<T>) obj);
            }
            return false;
        }

        public bool Equals(Entity<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
