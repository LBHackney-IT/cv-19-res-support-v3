using System;
using cv19ResSupportV3.Tests.V3.Helper;
using cv19ResSupportV3.V3.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests
{
    [TestFixture]
    public class DatabaseTests
    {
        private IDbContextTransaction _transaction;
        protected HelpRequestsContext DatabaseContext { get; private set; }

        [SetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseNpgsql(ConnectionString.TestDatabase());
            DatabaseContext = new HelpRequestsContext(builder.Options);
            DatabaseContext.Database.Migrate();
            _transaction = DatabaseContext.Database.BeginTransaction();
        }

        [TearDown]
        public void RunAfterAnyTests()
        {
            _transaction.Rollback();
            _transaction.Dispose();
            ClearTable();
        }

        public void ClearTable()
        {
            var addedCases = DatabaseContext.CaseNoteEntities;
            DatabaseContext.CaseNoteEntities.RemoveRange(addedCases);
            var addedEntities = DatabaseContext.HelpRequestEntities;
            DatabaseContext.HelpRequestEntities.RemoveRange(addedEntities);
            var addedLookups = DatabaseContext.Lookups;
            DatabaseContext.Lookups.RemoveRange(addedLookups);
            try
            {
                DatabaseContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine($"Cleardown {e}");
            }
        }
    }
}
