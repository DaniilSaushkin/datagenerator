using datagenerator.core.models;
using datagenerator.core.repository;
using Microsoft.EntityFrameworkCore;

namespace datagenerator.core.services
{
    public sealed class TemplateService
    {
        private readonly DatabaseContext _context;

        public TemplateService(DatabaseContext context)
        {
            _context = context;
        }

        public List<Template> GetAll() => _context.Templates.Include(t => t.Items).ToList();

        public Template GetOne(string name) => _context.Templates.FirstOrDefault(t => t.Name == name);

        public Template Create(string name)
        {
            name.Trim();

            if (name.Length < 3 || name.Length > 16)
                throw new ArgumentException("Name should be greater than 3 symbols and less than 16");

            Template template = GetOne(name);

            if (template != null)
                throw new ArgumentException($"{name} template already exists");

            template = new Template() { Name = name, IsDeleted = false };

            try
            {
                _context.Templates.Add(template);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating user" +  ex.Message, ex);
            }

            return template;
        }

        public Template Remove(string name)
        {
            Template template = GetOne(name);

            if (template == null)
                throw new ArgumentNullException($"{name} template not found");

            template.IsDeleted = true;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error removing template: " + ex.Message, ex);
            }

            return template;
        }

        public Template ChangeName(string currentName, string newName)
        {
            newName.Trim();

            if (newName.Length < 3 || newName.Length > 16)
                throw new ArgumentException("New name should be greater than 3 symbols and less than 16");

            Template template = GetOne(currentName);

            if (template == null)
                throw new ArgumentNullException($"{currentName} template not found");

            if (GetOne(newName) != null)
                throw new ArgumentException($"{newName} template alreary exists");

            template.Name = newName;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error name changing:" + ex.Message, ex);
            }

            return template;
        }
    }
}
