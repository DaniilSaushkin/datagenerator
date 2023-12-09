using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Text.Json;

namespace datagenerator.core.repository
{
    public sealed class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).FullName;
            string connectionString = String.Empty;

            try
            {
                var json = File.ReadAllText(projectDirectory + "\\config.json");
                connectionString = JsonSerializer.Deserialize<Dictionary<string, string>>(json)["connectionString"];
            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine("config.json not found");
                throw new FileNotFoundException(fnfe.Message);
            }
            catch (JsonException je)
            {
                Console.WriteLine("json file is broken or empty");
                throw new JsonException(je.Message);
            }
            catch (KeyNotFoundException knfe)
            {
                Console.WriteLine("connection string not found");
                throw new KeyNotFoundException(knfe.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("I don't know what's happening :(");
                throw new Exception(ex.Message, ex);
            }

            DbContextOptionsBuilder<DatabaseContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer(connectionString);

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
