using System.IO;
using DockerLib;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace ClassLib.Test
{
    [SetUpFixture]
    public class Test0
    {
        private const string MigrationPath = @"C:\Users\016265\Code\CSharp\DockerLab\ClassLib.Test\Migration.sql";

        [OneTimeSetUp]
        public void GlobalSetup() => Dockery.Migration = RunMigration;

        [OneTimeTearDown]
        public void GlobalTearDown() => Dockery.CleanContainer();

        public static void RunMigration(Container container)
        {
            var sqlScript = File.ReadAllText(MigrationPath);

            var crmDbContext = new CrmDbContext(container.Port);
            crmDbContext.Database.SetCommandTimeout(500);
            crmDbContext.Database.ExecuteSqlCommand(sqlScript);
        }
    }
}