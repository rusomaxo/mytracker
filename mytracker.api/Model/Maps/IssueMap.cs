using FluentNHibernate.Mapping;

namespace mytracker.api.Model.Maps
{
    public class IssueMap : ClassMap<Issue>
    {
        public IssueMap()
        {
            Id(x => x.Id);
            Map(x => x.Subject);
            Map(x => x.Description);
            Map(x => x.Number);
            References(x => x.Author);
        }
    }
}
