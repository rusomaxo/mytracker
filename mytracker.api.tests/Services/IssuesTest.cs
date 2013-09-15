using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using mytracker.api.DataAccess;
using mytracker.api.Exceptions;
using mytracker.api.Model;
using mytracker.api.Security;
using mytracker.api.Services;

namespace mytracker.api.tests.Services
{
    public class IssuesTest
    {
        private IDataSession _session;
        private List<Issue> _issuesList;
        private IIssues _issues;
        private User _author;

        [SetUp]
        public void SetUp()
        {
            _author = new User {DisplayName = "test author", Email = "author@mail.ru", IsBlocked = false};

            _session = Substitute.For<IDataSession>();
            _issuesList = new List<Issue>();

            _session.Query<Issue>().Returns(_issuesList.AsQueryable());

            _issues = new Issues(_session);
        }

        [Test]
        public void CreateIssue()
        {
            _issues.Create(_author, "test subject", "test description");
            _session.Received().SaveOrUpdate(Arg.Is<Issue>(issue => issue.Author == _author && issue.Subject == "test subject" && issue.Number == 1 && issue.Description == "test description"));
        }

        [Test]
        public void ImportIssue()
        {
            _issuesList.Add(new Issue {Author = _author, Description = "first issue description", Number= 1, Subject = "first issue subject"});

            var ex = Assert.Throws<DublicateIssueNumberException>(() => _issues.Import(_author, 1, "test subject", "test description"));

            Assert.That(ex.Number, Is.EqualTo(1));
        }
    }
}
