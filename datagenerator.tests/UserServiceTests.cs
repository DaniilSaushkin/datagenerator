using datagenerator.core.models;
using datagenerator.core.repository;
using datagenerator.core.services;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;

namespace datagenerator.tests
{
    public sealed class UserServiceTests : IDisposable
    {
        private readonly UserService _SUT;
        private readonly DatabaseContext _context;

        public UserServiceTests()
        {
            DbContextOptionsBuilder<DatabaseContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DGDB;Trusted_Connection=True;");
            _context = new(optionsBuilder.Options);

            _SUT = new UserService(_context);
        }

        [Fact]
        public void Must_create_user()
        {
            User user = _SUT.Create("Vasiliy", "123");

            using (new AssertionScope())
            {
                user.Nickname.Should().Be("Vasiliy");
                user.IsDeleted.Should().BeFalse();
                user.UserData?.Password.Should().NotBeNullOrEmpty();
            }
        }

        [Fact]
        public void Name_unique_error_when_creating_user()
        {
            User user = _SUT.Create("Vasiliy", "123");

            Action act = () => _SUT.Create("Vasiliy", "123");

            act.Should().Throw<ArgumentException>().WithMessage("Vasiliy user already exists");
        }

        [Fact]
        public void Name_length_error_when_creating_user()
        {
            Action act = () => _SUT.Create("Va", "123");

            act.Should().Throw<ArgumentException>().WithMessage("Nickname should be greater than 3 symbols and less than 32");
        }

        [Fact]
        public void Password_length_error_when_creating_user()
        {
            Action act = () => _SUT.Create("Vasiliy", "12");

            act.Should().Throw<ArgumentException>().WithMessage("Password should be greater than 3 symbols and less than 32");
        }

        [Fact]
        public void User_IsDeleted_should_be_true()
        {
            User user = _SUT.Create("Vasiliy", "123");

            user = _SUT.Remove("Vasiliy");

            user.IsDeleted.Should().BeTrue();
        }

        [Fact]
        public void Error_alreary_removing_when_removing_user()
        {
            User user = _SUT.Create("Vasiliy", "123");
            user = _SUT.Remove("Vasiliy");

            Action act = () => _SUT.Remove("Vasiliy");

            act.Should().Throw<ArgumentException>().WithMessage($"{user.Nickname} already removed");
        }

        [Fact]
        public void Error_not_found_user_when_removing_user()
        {            
            Action act = () => _SUT.Remove("Vasiliy");

            act.Should().Throw<ArgumentException>().WithMessage($"Value cannot be null. (Parameter 'Vasiliy user not found')");
        }

        [Fact]
        public void Should_get_one_user()
        {
            User userCreate = _SUT.Create("Vasiliy", "123");

            User user = _SUT.GetOne(userCreate.Nickname);

            user.Nickname.Should().Be(userCreate.Nickname);
        }

        public void Dispose()
        {
            User user = _context.Users.Include(u => u.UserData).FirstOrDefault(u => u.Nickname == "Vasiliy");

            if (user == null)
                return;

            try
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error disposing test: " + ex.Message, ex);
            }
        }
    }
}
