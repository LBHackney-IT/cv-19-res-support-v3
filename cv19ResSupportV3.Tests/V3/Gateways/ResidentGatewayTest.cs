using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Bogus;
using System.Linq;

namespace cv19ResSupportV3.Tests.V3.Gateways
{
    [TestFixture]
    public class ResidentGatewayTest : DatabaseTests
    {
        private ResidentGateway _classUnderTest;
        private Faker _faker = new Faker();

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new ResidentGateway(DatabaseContext);
        }

        // not matching first name, but matching uprn or dob
        // Does not throw an error if... empty db

        #region Duplicate Resident Finding Tests (FindResident)

        // If NHS numbers match, then:
        // Resident id is returned as duplicate.

        [Test]
        public void FindResidentReturnsAMatchIdWhenThereIsAResidentWithMatchingNHSNumber()
        {
            // arrange
            var matchingNhsNumber = _faker.Random.Hash();

            var searchParameters = new FindResident
            {
                NhsNumber = matchingNhsNumber
            };

            // control resident
            var existingResidentNoNhsNumberMatch = new ResidentEntity
            {
                NhsNumber = _faker.Random.Hash()
            };

            var existingResidentNhsNumberMatch = new ResidentEntity
            {
                NhsNumber = matchingNhsNumber
            };

            DatabaseContext.ResidentEntities.Add(existingResidentNoNhsNumberMatch);
            DatabaseContext.ResidentEntities.Add(existingResidentNhsNumberMatch);
            DatabaseContext.SaveChanges();

            // act
            var duplicateResidentId = _classUnderTest.FindResident(searchParameters);
            bool isResidentDuplicate = duplicateResidentId != null;

            // assert
            isResidentDuplicate.Should().BeTrue();
            duplicateResidentId.Should().Be(existingResidentNhsNumberMatch.Id);
        }

        // If NHS number is empty or null or does not match &
        // If Uprn is present, then:
        // Resident is considered a duplicate, when First Name, Last Name & Uprn match.

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("nhs number")]
        public void FindResidentReturnsAMatchWhenNoNhsNumberIsPresentAndWhenThereIsAResidentWithMatchingNonEmptyUprnAndFirstAndLastNamesForAllEmptyCasesOfNhsNumber(string testCaseNhsNumber)
        {
            // arrange
            var matchingEmptyNhsNumber = testCaseNhsNumber;
            var matchingFirstName = _faker.Random.Hash();
            var matchingLastName = _faker.Random.Hash();
            var matchingUprn = _faker.Random.Hash();

            var searchParameters = new FindResident
            {
                NhsNumber = matchingEmptyNhsNumber,
                Uprn = matchingUprn,
                FirstName = matchingFirstName,
                LastName = matchingLastName,
            };

            // empty nhs number match - making sure that doesn't get treated as a false positive
            var existingResidentEmptyNhsNumberMatch = new ResidentEntity
            {
                NhsNumber = matchingEmptyNhsNumber,
                Uprn = _faker.Random.Hash(),
                FirstName = _faker.Random.Hash(),
                LastName = _faker.Random.Hash()
            };

            var existingResidentUprnMatch = new ResidentEntity
            {
                NhsNumber = _faker.Random.Hash(),
                Uprn = matchingUprn,
                FirstName = matchingFirstName,
                LastName = matchingLastName
            };

            // testing the no match path (adding this 1 line, so I wouldn't need to copy the whole test)
            if (matchingEmptyNhsNumber == "nhs number") existingResidentEmptyNhsNumberMatch.NhsNumber = _faker.Random.Hash();

            DatabaseContext.ResidentEntities.Add(existingResidentEmptyNhsNumberMatch);
            DatabaseContext.ResidentEntities.Add(existingResidentUprnMatch);
            DatabaseContext.SaveChanges();

            // act
            var duplicateResidentId = _classUnderTest.FindResident(searchParameters);
            bool isResidentDuplicate = duplicateResidentId != null;

            // assert
            isResidentDuplicate.Should().BeTrue();
            duplicateResidentId.Should().Be(existingResidentUprnMatch.Id);
        }

        // If NHS number is empty or null or does not match &
        // If Uprn is empty or null or does not match &
        // If any of the Dob fields are missing (The focus here is on the Uprn paths, not the dob), then:
        // Resident is considered unique even when the First Name, Last Name & Nonempty Uprn match.

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("uprn")]
        public void FindResidentReturnsNoMatchWhenNoNhsNumberAndUprnNumberAndAnyOfTheDobFieldsArePresentForAllEmptyCasesOfUprn(string testCaseUprn)
        {
            // arrange
            var notMatchingNhsNumber = _faker.Random.Hash();
            var matchingFirstName = _faker.Random.Hash();
            var matchingLastName = _faker.Random.Hash();
            var matchingEmptyUprn = testCaseUprn;
            // not even setting Dob fields (null by default)

            var searchParameters = new FindResident
            {
                NhsNumber = notMatchingNhsNumber,
                Uprn = matchingEmptyUprn,
                FirstName = matchingFirstName,
                LastName = matchingLastName,
            };

            var existingResidentNoUprnMatch = new ResidentEntity
            {
                NhsNumber = _faker.Random.Hash(),
                Uprn = _faker.Random.Hash(), // no uprn match
                FirstName = matchingFirstName,
                LastName = matchingLastName
            };

            var existingResidentEmptyUprnMatch = new ResidentEntity
            {
                NhsNumber = _faker.Random.Hash(),
                Uprn = matchingEmptyUprn, // matching empty uprn
                FirstName = matchingFirstName,
                LastName = matchingLastName
            };

            // testing a case path, where uprn is not empty, but still does not match
            if (matchingEmptyUprn == "uprn") existingResidentEmptyUprnMatch.Uprn = _faker.Random.Hash();

            DatabaseContext.ResidentEntities.Add(existingResidentNoUprnMatch);
            DatabaseContext.ResidentEntities.Add(existingResidentEmptyUprnMatch);
            DatabaseContext.SaveChanges();

            // act
            var duplicateResidentId = _classUnderTest.FindResident(searchParameters);
            bool isResidentDuplicate = duplicateResidentId != null;

            // assert
            isResidentDuplicate.Should().BeFalse();
            duplicateResidentId.Should().Be(null);
        }

        // If NHS number is empty or null or does not match &
        // If Uprn is empty or null or does not match &
        // If Dob is non-empty and not null, then
        // Resident is considered a duplicate when its Dob, Fname & Lname match.
        // (Difference between this test and the above one is that for the same given UPRNs, DOBs are no longer empty)

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("uprn")]
        public void FindResidentReturnsAMatchWhenNoNhsAndUprnNumberMatchIsFoundButThereExistsAResidentWithMatchingFirstAndLastNameAndDOBForAllEmptyCasesOfUprn(string testCaseUprn)
        {
            // arrange
            var notMatchingNhsNumber = _faker.Random.Hash();
            var matchingFirstName = _faker.Random.Hash();
            var matchingLastName = _faker.Random.Hash();
            var emptyMatchingUprn = testCaseUprn;

            var matchingDOBDay = _faker.Random.Hash();
            var matchingDOBMonth = _faker.Random.Hash();
            var matchingDOBYear = _faker.Random.Hash();

            var searchParameters = new FindResident
            {
                NhsNumber = notMatchingNhsNumber,
                Uprn = emptyMatchingUprn,
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = matchingDOBDay,
                DobMonth = matchingDOBMonth,
                DobYear = matchingDOBYear
            };

            var existingResidentNoDOBMatch = new ResidentEntity
            {
                NhsNumber = _faker.Random.Hash(),
                Uprn = _faker.Random.Hash(),
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = _faker.Random.Hash(),
                DobMonth = _faker.Random.Hash(),
                DobYear = _faker.Random.Hash()
            };

            var existingResidentDOBMatch = new ResidentEntity
            {
                NhsNumber = _faker.Random.Hash(),
                Uprn = emptyMatchingUprn, // check to see if we fall through to the next rule on empty match
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = matchingDOBDay,
                DobMonth = matchingDOBMonth,
                DobYear = matchingDOBYear
            };

            // checking rule falling through to the next on Uprn no match (not just empty)
            if (emptyMatchingUprn == "uprn") existingResidentDOBMatch.Uprn = _faker.Random.Hash();

            DatabaseContext.ResidentEntities.Add(existingResidentNoDOBMatch);
            DatabaseContext.ResidentEntities.Add(existingResidentDOBMatch);
            DatabaseContext.SaveChanges();

            // act
            var duplicateResidentId = _classUnderTest.FindResident(searchParameters);
            bool isResidentDuplicate = duplicateResidentId != null;

            // assert
            isResidentDuplicate.Should().BeTrue();
            duplicateResidentId.Should().Be(existingResidentDOBMatch.Id);
        }

        // If NHS number rule returns no match &
        // If Uprn rule returns no match &
        // If Dob is null or empty, then:
        // Resident is considered to be unique (as there's no better way to dedupe it after that point)
        // Test cases not exhaustive (there are 27 combinations), however due to the syntax nature of C# it's relatively safe to assume
        // that if the following 3 combinations pass, then the code works for all 27 (unless we're dealing with many lines of code of IF statments).
        // (Difference from one of the tests above is that this test assumes the UPRN rule is tested for all UPRN inputs, and it's only looking at
        // the possible DOB inputs under the case of assumed bad UPRN input)

        [TestCase(null, null, null)]
        [TestCase("", "", "")]
        [TestCase(" ", " ", " ")]
        [TestCase("23", "1", "1994")]
        public void FindResidentReturnsNoMatchWhenNoNhsAndUprnNumberMatchIsFoundAndThereIsNoResidentWithMatchingDOBForParticularCasesOfDobFieldsBeingEmpty(string tcDay, string tcMonth, string tcYear)
        {
            // arrange
            var notMatchingNhsNumber = _faker.Random.Hash();
            var notMatchingUprn = _faker.Random.Hash();
            var matchingFirstName = _faker.Random.Hash();
            var matchingLastName = _faker.Random.Hash();

            var emptyMatchingDOBDay = tcDay;
            var emptyMatchingDOBMonth = tcMonth;
            var emptyMatchingDOBYear = tcYear;

            var searchParameters = new FindResident
            {
                NhsNumber = notMatchingNhsNumber,
                Uprn = notMatchingUprn,
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = emptyMatchingDOBDay,
                DobMonth = emptyMatchingDOBMonth,
                DobYear = emptyMatchingDOBYear
            };

            var existingResidentNoDOBMatch = new ResidentEntity
            {
                NhsNumber = _faker.Random.Hash(),
                Uprn = _faker.Random.Hash(),
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = _faker.Random.Hash(),
                DobMonth = _faker.Random.Hash(),
                DobYear = _faker.Random.Hash()
            };

            var existingResidentEmptyDOBMatch = new ResidentEntity
            {
                NhsNumber = _faker.Random.Hash(),
                Uprn = _faker.Random.Hash(),
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = emptyMatchingDOBDay,
                DobMonth = emptyMatchingDOBMonth,
                DobYear = emptyMatchingDOBYear
            };

            // checking a non-empty match case
            if (emptyMatchingDOBYear == "1994")
            {
                existingResidentEmptyDOBMatch.DobDay = _faker.Random.Hash();
                existingResidentEmptyDOBMatch.DobMonth = _faker.Random.Hash();
                existingResidentEmptyDOBMatch.DobYear = _faker.Random.Hash();
            }

            DatabaseContext.ResidentEntities.Add(existingResidentNoDOBMatch);
            DatabaseContext.ResidentEntities.Add(existingResidentEmptyDOBMatch);
            DatabaseContext.SaveChanges();

            // act
            var duplicateResidentId = _classUnderTest.FindResident(searchParameters);
            bool isResidentDuplicate = duplicateResidentId != null;

            // assert
            isResidentDuplicate.Should().BeFalse();
            duplicateResidentId.Should().Be(null);
        }

        // Should there be checks for the inserted records values?
        // Also should this case be treated as Unique Resident or the 400 Bad Request? (There doesn't seem to be validation preventing this)
        // For now will treat this as a new unique record.

        [TestCase(false, null)] //should throw
        [TestCase(false, "")]
        [TestCase(false, " ")]
        [TestCase(true, null)]
        [TestCase(true, "")]
        [TestCase(true, " ")]
        public void FindResidentReturnsNoMatchWhenEitherFirstNameOrLastNameIsMissingForAllEmptyCases(bool isDobTest, string testCaseName)
        {
            // arrange
            var notMatchingNhsNumber = _faker.Random.Hash();
            var matchingFirstName = _faker.Random.Hash();
            var matchingLastName = _faker.Random.Hash();
            var matchingUprn = _faker.Random.Hash();

            var matchingDOBDay = _faker.Random.Hash();
            var matchingDOBMonth = _faker.Random.Hash();
            var matchingDOBYear = _faker.Random.Hash();

            var searchParametersNoFirstName = new FindResident
            {
                NhsNumber = notMatchingNhsNumber,
                Uprn = isDobTest ? _faker.Random.Hash() : matchingUprn,
                FirstName = testCaseName,
                LastName = matchingLastName,
                DobDay = isDobTest ? matchingDOBDay : _faker.Random.Hash(),
                DobMonth = isDobTest ? matchingDOBMonth : _faker.Random.Hash(),
                DobYear = isDobTest ? matchingDOBYear : _faker.Random.Hash(),
            };

            var searchParametersNoLastName = new FindResident
            {
                NhsNumber = notMatchingNhsNumber,
                Uprn = isDobTest ? _faker.Random.Hash() : matchingUprn,
                FirstName = matchingFirstName,
                LastName = testCaseName,
                DobDay = isDobTest ? matchingDOBDay : _faker.Random.Hash(),
                DobMonth = isDobTest ? matchingDOBMonth : _faker.Random.Hash(),
                DobYear = isDobTest ? matchingDOBYear : _faker.Random.Hash(),
            };

            var existingResident = new ResidentEntity
            {
                NhsNumber = _faker.Random.Hash(),
                Uprn = matchingUprn,
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = matchingDOBDay,
                DobMonth = matchingDOBMonth,
                DobYear = matchingDOBYear
            };

            DatabaseContext.ResidentEntities.Add(existingResident);
            DatabaseContext.SaveChanges();

            // act
            var duplicateResidentIdAttempt1 = _classUnderTest.FindResident(searchParametersNoFirstName);
            var duplicateResidentIdAttempt2 = _classUnderTest.FindResident(searchParametersNoLastName);
            bool isResidentRequest1Duplicate = duplicateResidentIdAttempt1 != null;
            bool isResidentRequest2Duplicate = duplicateResidentIdAttempt2 != null;

            // assert
            isResidentRequest1Duplicate.Should().BeFalse();
            duplicateResidentIdAttempt1.Should().Be(null);

            isResidentRequest2Duplicate.Should().BeFalse();
            duplicateResidentIdAttempt2.Should().Be(null);
        }

        [Test]
        public void FindResidentReturnsAMatchByNhsNumberEvenWhenProvidedNhsNumberIsNotTrimmed()
        {
            // arrange

            // Case 1 setup: Non-trimmed number is in the request, and the trimmed one in the DB.

            var nhsNumber1 = _faker.Random.Hash();
            var matchingNonTrimmedNHSNumber1 = $"  {nhsNumber1}     ";

            // don't care about Fname, Lname or anything else, as I'm expecting a match via NHS number
            var searchParameters1 = new FindResident
            {
                NhsNumber = matchingNonTrimmedNHSNumber1
            };

            var existingResidentWithNormalNhsNumber = new ResidentEntity
            {
                NhsNumber = nhsNumber1
            };

            // Case 2 setup: Non-trimmed number is in the DB, and the trimmed one in the request.

            var matchingNhsNumber2 = _faker.Random.Hash();
            var nonTrimmedNHSNumber2 = $"     {matchingNhsNumber2} ";

            var searchParameters2 = new FindResident
            {
                NhsNumber = matchingNhsNumber2
            };

            var existingResidentWithNonTrimmedNhsNumber = new ResidentEntity
            {
                NhsNumber = nonTrimmedNHSNumber2
            };

            DatabaseContext.ResidentEntities.Add(existingResidentWithNormalNhsNumber);
            DatabaseContext.ResidentEntities.Add(existingResidentWithNonTrimmedNhsNumber);
            DatabaseContext.SaveChanges();

            // act
            var duplicateResidentId1 = _classUnderTest.FindResident(searchParameters1); // trimmed in DB
            bool isResidentDuplicate1 = duplicateResidentId1 != null;

            var duplicateResidentId2 = _classUnderTest.FindResident(searchParameters2); // trimmed in request
            bool isResidentDuplicate2 = duplicateResidentId2 != null;

            // assert
            isResidentDuplicate1.Should().BeTrue(); // trimmed in DB
            duplicateResidentId1.Should().Be(existingResidentWithNormalNhsNumber.Id);

            isResidentDuplicate2.Should().BeTrue(); // trimmed in request
            duplicateResidentId2.Should().Be(existingResidentWithNonTrimmedNhsNumber.Id);
        }

        #endregion


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
