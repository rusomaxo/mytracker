using FluentNHibernate.Mapping;

namespace mytracker.api.Model.Maps
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.DisplayName);
            Map(x => x.Email);
            Map(x => x.IsBlocked);
        }
    }
}
