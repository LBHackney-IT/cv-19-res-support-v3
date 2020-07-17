using System.Linq;
using cv19ResRupportV3.Tests.V3.Helper;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResRupportV3.Tests.V3.Infrastructure
{
    [TestFixture]
    public class HelpRequestsContextTests : DatabaseTests
    {
        [Test]
        public void CanGetADatabaseEntity()
        {
            var databaseEntity = DatabaseEntityHelper.CreateDatabaseEntity();

            DatabaseContext.Add(databaseEntity);
            DatabaseContext.SaveChanges();

            var result = DatabaseContext.HelpRequestEntities.ToList().FirstOrDefault();

            result.Should().BeEquivalentTo(databaseEntity);
        }
    }
}
