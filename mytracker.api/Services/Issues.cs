using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mytracker.api.DataAccess;
using mytracker.api.Exceptions;
using mytracker.api.Model;

namespace mytracker.api.Services
{
    public class Issues : IIssues
    {
        private readonly IDataSession _session;

        public Issues(IDataSession session)
        {
            _session = session;
        }

        public Issue Import(User author, int number, string subject, string description)
        {
            if(_session.Query<Issue>().Any(x => x.Number == number))
                throw new DublicateIssueNumberException(number);

            var issue = new Issue
                            {
                                Author = author,
                                Number = number,
                                Subject = subject,
                                Description = description
                            };

            _session.SaveOrUpdate(issue);

            return issue;
        }

        public void Delete(int number)
        {
            throw new NotImplementedException();
        }

        public Issue Create(User author, string subject, string description)
        {
            var maxid = 1 + _session.Query<Issue>().Select(x => x.Number).DefaultIfEmpty(0).Max();

            return Import(author, maxid, subject, description);
        }
    }
}
