using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Gateways
{
    [TestFixture]
    public class ResidentGatewayTest : DatabaseTests
    {
        private ResidentGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new ResidentGateway(DatabaseContext);
        }

        [Test]
        public void FindResidentWithUprnAndNameReturnsTheResidentIdIfItExists()
        {
            var existingResident = new ResidentEntity { Uprn = "uprn", FirstName = "FirstName", LastName = "LastName" };

            var findResidentCommand = new FindResident
            {
                Uprn = "uprn",
                FirstName = "FirstName",
                LastName = "LastName",
                DobDay = "Day",
                DobMonth = "Month",
                DobYear = "Year"
            };

            DatabaseContext.ResidentEntities.Add(existingResident);
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.FindResident(findResidentCommand);
            var createdRecord = DatabaseContext.ResidentEntities.Find(response);
            var expectedRecord = new ResidentEntity
            {
                Id = existingResident.Id,
                Uprn = "uprn",
                FirstName = "FirstName",
                LastName = "LastName"
            };

            createdRecord.Should().BeEquivalentTo(expectedRecord);
        }

        [Test]
        public void FindResidentWithDobandNameReturnsTheResidentIdIfItExists()
        {
            var existingResident = new ResidentEntity
            {
                FirstName = "FirstName",
                LastName = "LastName",
                DobDay = "Day",
                DobMonth = "Month",
                DobYear = "Year"
            };

            var findResidentCommand = new FindResident
            {
                Uprn = "uprn",
                FirstName = "FirstName",
                LastName = "LastName",
                DobDay = "Day",
                DobMonth = "Month",
                DobYear = "Year"
            };

            DatabaseContext.ResidentEntities.Add(existingResident);
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.FindResident(findResidentCommand);
            var createdRecord = DatabaseContext.ResidentEntities.Find(response);
            var expectedRecord = new ResidentEntity
            {
                Id = existingResident.Id,
                FirstName = "FirstName",
                LastName = "LastName",
                DobDay = "Day",
                DobMonth = "Month",
                DobYear = "Year"
            };

            createdRecord.Should().BeEquivalentTo(expectedRecord);
        }


        [Test]
        public void PatchResidentReturnsTheUpdatedResident()
        {
            var existingResident = new ResidentEntity
            {
                FirstName = "FirstName",
                LastName = "LastName",
                DobDay = "Day",
                DobMonth = "Month",
                DobYear = "Year",
                Uprn = "Uprn"
            };

            var updateResidentCommand = new PatchResident
            {
                Uprn = "Uprn",
                DobDay = "NewDay",
                DobMonth = "NewMonth",
                DobYear = "NewYear"
            };

            DatabaseContext.ResidentEntities.Add(existingResident);
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.PatchResident(existingResident.Id, updateResidentCommand);
            var createdRecord = DatabaseContext.ResidentEntities.Find(response.Id);
            var expectedRecord = new ResidentEntity
            {
                Id = existingResident.Id,
                FirstName = existingResident.FirstName,
                LastName = existingResident.LastName,
                DobDay = updateResidentCommand.DobDay,
                DobMonth = updateResidentCommand.DobMonth,
                DobYear = updateResidentCommand.DobYear,
                Uprn = existingResident.Uprn
            };

            createdRecord.Should().BeEquivalentTo(expectedRecord);
        }


        [Test]
        public void updateResidentReturnsTheUpdatedResident()
        {
            var existingResident = new ResidentEntity
            {
                FirstName = "FirstName",
                LastName = "LastName",
                DobDay = "Day",
                DobMonth = "Month",
                DobYear = "Year",
                Uprn = "Uprn"
            };

            var updateResidentCommand = new UpdateResident
            {
                Uprn = "Uprn",
                DobDay = "NewDay",
                DobMonth = "NewMonth",
                DobYear = "NewYear"
            };

            DatabaseContext.ResidentEntities.Add(existingResident);
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.UpdateResident(existingResident.Id, updateResidentCommand);
            var createdRecord = DatabaseContext.ResidentEntities.Find(response.Id);
            var expectedRecord = new ResidentEntity
            {
                Id = existingResident.Id,
                FirstName = existingResident.FirstName,
                LastName = existingResident.LastName,
                DobDay = updateResidentCommand.DobDay,
                DobMonth = updateResidentCommand.DobMonth,
                DobYear = updateResidentCommand.DobYear,
                Uprn = existingResident.Uprn
            };

            createdRecord.Should().BeEquivalentTo(expectedRecord);
        }

        [Test]
        public void CreateResidentReturnsTheResidentId()
        {
            var createResidentCommand = new CreateResident
            {
                FirstName = "FirstName",
                LastName = "LastName",
                DobDay = "Day",
                DobMonth = "Month",
                DobYear = "Year",
                Uprn = "Uprn"
            };


            var response = _classUnderTest.CreateResident(createResidentCommand);
            var createdRecord = DatabaseContext.ResidentEntities.Find(response.Id);
            var expectedRecord = new ResidentEntity
            {
                Id = response.Id,
                FirstName = createResidentCommand.FirstName,
                LastName = createResidentCommand.LastName,
                DobDay = createResidentCommand.DobDay,
                DobMonth = createResidentCommand.DobMonth,
                DobYear = createResidentCommand.DobYear,
                Uprn = createResidentCommand.Uprn
            };

            createdRecord.Should().BeEquivalentTo(expectedRecord);
        }

        [Test]
        public void PatchResidentPatchesCorrectFields()
        {
            var resident = EntityHelpers.createResident(114);

            DatabaseContext.ResidentEntities.Add(resident);
            DatabaseContext.SaveChanges();
            var patchRequestObject = new PatchResident() { DobMonth = "12", EmailAddress = "abc", };

            _classUnderTest.PatchResident(resident.Id, patchRequestObject);

            var oldEntity = DatabaseContext.ResidentEntities.Find(resident.Id);
            DatabaseContext.Entry(oldEntity).State = EntityState.Detached;
            var updatedEntity = DatabaseContext.ResidentEntities.Find(resident.Id);

            updatedEntity.DobMonth.Should().Be(patchRequestObject.DobMonth);
            updatedEntity.EmailAddress.Should().Be(patchRequestObject.EmailAddress);
        }

        [Test]
        public void GetResidentReturnsResidentIfItExist()
        {
            var residentId = 111;
            var residentEntity = EntityHelpers.createResident(residentId);
            DatabaseContext.ResidentEntities.Add(residentEntity);
            DatabaseContext.SaveChanges();
            var response = _classUnderTest.GetResident(residentId);
            residentEntity.Should().BeEquivalentTo(response);
        }
    }
}
