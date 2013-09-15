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
    [TestFixture]
    public class UsersTest
    {
        private IDataSession _session;
        private List<User> _usersList;
        private IUsers _users;

        [SetUp]
        public void SetUp()
        {
            _session = Substitute.For<IDataSession>();
            _usersList = new List<User>
                         {
                             new User {Email = "notblocked@mail.ru", DisplayName = "notblocked", Hash= PasswordHash.CreateHash("notblockedpass"), IsBlocked = false},
                             new User {Email = "blocked@mail.ru", DisplayName = "blocked", Hash= PasswordHash.CreateHash("blockedpass"), IsBlocked = true}
                         };

            _session.Query<User>().Returns(_usersList.AsQueryable());

            _users = new Users(_session);
        }

        [Test]
        public void RegisterDublicateDisplayUserName()
        {
            var expr = Assert.Throws<DublicateUserException>(() => _users.Register("notblocked", "testpass", "aaa@mail.ru"));
            Assert.That(expr.ExceptionType, Is.EqualTo(ApiExceptionType.DublicateUser));
            Assert.That(expr.User, Is.EqualTo("notblocked"));
            Assert.That(expr.Email, Is.EqualTo("aaa@mail.ru"));
        }

        [Test]
        public void RegisterDublicateEmail()
        {
            var expr = Assert.Throws<DublicateUserException>(() => _users.Register("new user name", "test pass", "notblocked@mail.ru"));
            Assert.That(expr.ExceptionType, Is.EqualTo(ApiExceptionType.DublicateUser));
        }

        [Test]
        public void BlockUser()
        {
            _users.Block("notblocked@mail.ru");

            _session.Received().SaveOrUpdate(Arg.Is<User>(user => user.Email == "notblocked@mail.ru" && user.IsBlocked));
        }

        [Test]
        public void BlockNotExistingUser()
        {
            var ex = Assert.Throws<UserNotFoundException>(() => _users.Block("not existing email@mail.ru"));
            Assert.That(ex.Email, Is.EqualTo("not existing email@mail.ru"));
            
        }

        [Test]
        public void UnblockUser()
        {
            _users.Unblock("blocked@mail.ru");

            _session.Received().SaveOrUpdate(Arg.Is<User>(user => user.Email == "blocked@mail.ru" && !user.IsBlocked));
        }

        [Test]
        public void ValidateExistingUser()
        {
            var res = _users.Validate("notblocked@mail.ru", "notblockedpass");

            Assert.That(res, Is.True);
        }

        [Test]
        public void ValidateIncorrectPassword()
        {
            var res = _users.Validate("notblocked@mail.ru", "invalid pass");

            Assert.That(res, Is.False);
        }

        [Test]
        public void ValidateNotExistsUser()
        {
            var res = _users.Validate("not existing email@mail.ru", "notblockedpass");

            Assert.That(res, Is.False);
        }

        [Test]
        public void ValidateBlockedUser()
        {
            var res = _users.Validate("blocked@mail.ru", "blockedpass");

            Assert.That(res, Is.False);
        }

        [Test]
        public void ChangePassword()
        {
            var res = _users.ChangePassword("notblocked@mail.ru", "notblockedpass", "new notblockedpass");

            Assert.That(res, Is.True);
            _session.Received().SaveOrUpdate(Arg.Is<User>(user => user.Email == "notblocked@mail.ru"));
        }
    }
}
