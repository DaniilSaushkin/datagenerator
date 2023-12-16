using datagenerator.core.models;
using datagenerator.core.repository;
using datagenerator.core.services;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;

namespace datagenerator.tests
{
    public sealed class TemplateServiceTests : IDisposable
    {
        private readonly TemplateService _SUT;
        private readonly DatabaseContext _context;

        public TemplateServiceTests()
        {
            DbContextOptionsBuilder<DatabaseContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DGDB;Trusted_Connection=True;");
            _context = new(optionsBuilder.Options);

            _SUT = new TemplateService(_context);
        }

        [Fact]
        public void Must_create_template()
        {
            Template template = _SUT.Create("Countries");

            using (new AssertionScope())
            {
                template.Name.Should().Be("Countries");
                template.ID.Should().NotBeEmpty();
                template.IsDeleted.Should().BeFalse();
            }
        }

        [Fact]
        public void Name_unique_error_when_creating_template()
        {
            Template template = _SUT.Create("Countries");

            Action act = () => _SUT.Create("Countries");

            act.Should().Throw<ArgumentException>().WithMessage("Countries template already exists");
        }

        [Fact]
        public void Name_length_error_when_creating_template()
        {
            Action act = () => _SUT.Create("Co");

            act.Should().Throw<ArgumentException>().WithMessage("Name should be greater than 3 symbols and less than 16");
        }

        [Fact]
        public void Template_IsDeleted_should_be_true()
        {
            Template template = _SUT.Create("Countries");

            template = _SUT.Remove("Countries");

            template.IsDeleted.Should().BeTrue();
        }

        [Fact]
        public void Template_change_name_should_be_changed()
        {
            Template template = _SUT.Create("Countris");

            template = _SUT.ChangeName("Countris", "Countries");

            template.Name.Should().Be("Countries");
        }

        [Fact]
        public void Name_length_error_when_changing_name_template()
        {
            Template template = _SUT.Create("Countries");

            Action act = () => _SUT.ChangeName("Countries", "Co");

            act.Should().Throw<ArgumentException>().WithMessage("New name should be greater than 3 symbols and less than 16");
        }

        public void Dispose()
        {
            Template template = _context.Templates.Include(t => t.Items).FirstOrDefault(t => t.Name == "Countries");

            if (template == null)
                return;

            try
            {
                _context.Templates.Remove(template);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error disposing test: " + ex.Message, ex);
            }
        }
    }
}
