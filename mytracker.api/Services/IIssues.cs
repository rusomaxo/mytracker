using mytracker.api.Model;

namespace mytracker.api.Services
{
    public interface IIssues
    {
        Issue Create(User author, string subject, string description);

        Issue Import(User author, int number, string subject, string description);

        void Delete(int number);
    }
}
