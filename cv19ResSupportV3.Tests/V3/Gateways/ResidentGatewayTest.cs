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

        // If NHS number rule fails to match &
        // If Uprn rule fails to match &
        // If Dob rule fails to match &
        // If Resident First Name & Last Name match &
        // If Resident has associated HelpCase entity with matching Contact Tracing Id (NhsCtasId) :
        // Then the resident is considered a duplicate.
        [Test]
        public void FindResidentReturnsAMatchWhenFullNameAndNhsCtsIdOfChildHelpCaseEntityMatchButNoPreviousRulesMatch()
        {
            //// arrange

            // create matching name data
            var matchingFirstName = _faker.Random.Hash();
            var matchingLastName = _faker.Random.Hash();

            // matching Nhs Ctas Id
            var matchingNhsCtasId = _faker.Random.AlphaNumeric(8);

            // create a request object
            var searchParameters = new FindResident
            {
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                NhsCtasId = matchingNhsCtasId
            };

            // create a matching resident object
            var duplicateResident = EntityHelpers.createResident(id: _faker.Random.Int(10, 1000));
            duplicateResident.FirstName = matchingFirstName;
            duplicateResident.LastName = matchingLastName;

            // create a matching help case entity with ctas id
            var matchingNhsCtasIdHelpCase = EntityHelpers.createHelpRequestEntity(
                id: _faker.Random.Int(100, 1000), // avoid potential inter-test clash
                residentId: duplicateResident.Id);
            matchingNhsCtasIdHelpCase.NhsCtasId = matchingNhsCtasId;

            // create non-matching (control) resident
            var controlResident = EntityHelpers.createResident(id: duplicateResident.Id + 1); // making sure ids are different

            // create non-matching help case entity (making sure only that non-matching ctas id wouldn't trigger the rule)
            var controlHelpCase = EntityHelpers.createHelpRequestEntity(
                id: matchingNhsCtasIdHelpCase.Id + 1, // making sure ids are different
                residentId: controlResident.Id);

            // add resident entities
            DatabaseContext.ResidentEntities.Add(duplicateResident);
            DatabaseContext.ResidentEntities.Add(controlResident);

            // add help request entities
            DatabaseContext.HelpRequestEntities.Add(matchingNhsCtasIdHelpCase);
            DatabaseContext.HelpRequestEntities.Add(controlHelpCase);

            // save db changes
            DatabaseContext.SaveChanges();


            //// act

            // call FindResident function with the request object
            var returnedDuplicateResidentId = _classUnderTest.FindResident(searchParameters);
            bool isResidentDuplicate = returnedDuplicateResidentId != null;


            //// assert

            // duplicate should have been found
            isResidentDuplicate.Should().BeTrue();

            // confirm that the retrieved id was the one inserted
            returnedDuplicateResidentId.Should().Be(duplicateResident.Id);

        }

        // If NHS number rule fails to match &
        // If Uprn rule fails to match &
        // If Dob rule fails to match &
        // If First Name & Last Name rule fails to match &
        // Even if Resident has associated HelpCase entity with matching Contact Tracing Id (NhsCtasId) :
        // Then the Ctas rule fails to match.
        [Test]
        public void FindResidentReturnsNoMatchWhenFirstAndLastNamesDoNotMatchRegardlessOfNhsCtasId()
        {
            //// arrange

            // create matching Nhs Ctas Id
            var matchingNhsCtasId = _faker.Random.AlphaNumeric(8);

            // create a request object
            var searchParameters = new FindResident
            {
                FirstName = _faker.Random.Hash(),
                LastName = _faker.Random.Hash(),
                NhsCtasId = matchingNhsCtasId
            };

            // create a resident with a child help case containing matching NhsCtasId
            var residentWithMachingCtasCase = EntityHelpers.createResident(id: _faker.Random.Int(10, 1000));

            var matchingNhsCtasIdHelpCase = EntityHelpers.createHelpRequestEntity(
                id: _faker.Random.Int(100, 1000), // avoid potential inter-test clash
                residentId: residentWithMachingCtasCase.Id);
            matchingNhsCtasIdHelpCase.NhsCtasId = matchingNhsCtasId;

            // create non-matching by everything (control) resident
            var controlResident = EntityHelpers.createResident(id: residentWithMachingCtasCase.Id + 1); // making sure ids are different

            var controlHelpCase = EntityHelpers.createHelpRequestEntity(
                id: matchingNhsCtasIdHelpCase.Id + 1, // making sure ids are different
                residentId: controlResident.Id);

            // add resident entities
            DatabaseContext.ResidentEntities.Add(residentWithMachingCtasCase);
            DatabaseContext.ResidentEntities.Add(controlResident);

            // add help request entities
            DatabaseContext.HelpRequestEntities.Add(matchingNhsCtasIdHelpCase);
            DatabaseContext.HelpRequestEntities.Add(controlHelpCase);

            // save db changes
            DatabaseContext.SaveChanges();


            //// act

            // call FindResident function with the request object
            var returnedDuplicateResidentId = _classUnderTest.FindResident(searchParameters);
            bool isResidentDuplicate = returnedDuplicateResidentId != null;


            //// assert

            // duplicate should have been found
            isResidentDuplicate.Should().BeFalse();
        }

        // If NHS number rule fails to match &
        // If Uprn rule fails to match &
        // If Dob rule fails to match &
        // If First Name & Last Name rule match &
        // If Request contains empty, null, whitespace or non-matching Contact Tracing Id (NhsCtasId) :
        // Then the overall Ctas rule fails to match.
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("nhsctasid")]
        public void FindResidentReturnsNoMatchWhenNhsCtasIdIsNullEmptyWhiteSpaceOrDoesntMatchAndThereAreNoMatchesAgaistOtherRules(string testCaseNhsCtasId)
        {
            //// arrange

            // create matching Nhs Ctas Id
            var ruleFailingNhsCtasId = testCaseNhsCtasId;

            // create a request object
            var searchParameters = new FindResident
            {
                FirstName = _faker.Random.Hash(),
                LastName = _faker.Random.Hash(),
                NhsCtasId = ruleFailingNhsCtasId
            };

            // create a resident with a child help case containing matching NhsCtasId
            var nonMatchingResident = EntityHelpers.createResident(id: _faker.Random.Int(10, 1000));

            var nonMatchingHelpCase = EntityHelpers.createHelpRequestEntity(
                id: _faker.Random.Int(100, 1000), // avoid potential inter-test clash
                residentId: nonMatchingResident.Id);
            nonMatchingHelpCase.NhsCtasId = ruleFailingNhsCtasId;

            // during the first 3 test cases, there should be no match even when request & db values match.
            // for the 4th one, no match should be found only when request & db record NhsCtasIds are different.
            // so setting them to be different.
            if (testCaseNhsCtasId == "nhsctasid") nonMatchingHelpCase.NhsCtasId = _faker.Random.Hash();

            // add resident entities
            DatabaseContext.ResidentEntities.Add(nonMatchingResident);

            // add help request entities
            DatabaseContext.HelpRequestEntities.Add(nonMatchingHelpCase);

            // save db changes
            DatabaseContext.SaveChanges();


            //// act

            // call FindResident function with the request object
            var returnedDuplicateResidentId = _classUnderTest.FindResident(searchParameters);
            bool isResidentDuplicate = returnedDuplicateResidentId != null;


            //// assert

            // duplicate should have been found
            isResidentDuplicate.Should().BeFalse();
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

        // When de-duplicating by nhsnumber, number formatting is ignored
        [Test]
        public void UponDeduplicatingByNhsNumberRuleFindResidentFindsAMatchEvenWhenNhsNumberAreSameButFormattedDifferentlyWithSpacesInTheMiddle()
        {
            // arrange
            // case formatted in db, not in request
            var unformattedNhsNumberInARequest = "4857773456";
            var formattedNhsNumberInADatabase = "485 777 3456";

            var searchParametersNoFormat = new FindResident
            {
                NhsNumber = unformattedNhsNumberInARequest
            };

            var existingResidentFormatted = new ResidentEntity
            {
                NhsNumber = formattedNhsNumberInADatabase
            };

            DatabaseContext.ResidentEntities.Add(existingResidentFormatted);

            // case formatted in request, not in db
            var unformattedNhsNumberInADatabase = "4888991234";
            var formattedNhsNumberInARequest = "48 88 99 1234";

            var searchParametersFormattedRequest = new FindResident
            {
                NhsNumber = formattedNhsNumberInARequest
            };

            var existingResidentNoFormat = new ResidentEntity
            {
                NhsNumber = unformattedNhsNumberInADatabase
            };

            DatabaseContext.ResidentEntities.Add(existingResidentNoFormat);
            DatabaseContext.SaveChanges();

            // act
            var duplicateResidentFormattedId = _classUnderTest.FindResident(searchParametersNoFormat);
            bool isResidentFormattedDuplicate = duplicateResidentFormattedId != null;

            var duplicateResidentNotFormattedId = _classUnderTest.FindResident(searchParametersFormattedRequest);
            bool isResidentNotFormattedDuplicate = duplicateResidentNotFormattedId != null;

            // assert
            isResidentFormattedDuplicate.Should().BeTrue();
            duplicateResidentFormattedId.Should().Be(existingResidentFormatted.Id);

            isResidentNotFormattedDuplicate.Should().BeTrue();
            duplicateResidentNotFormattedId.Should().Be(existingResidentNoFormat.Id);
        }

        // When de-duplicating by dob, dobDay and dobMonth leading zeros are ignored
        [Test]
        public void UponDeduplicatingByDobRuleFindResidentDoesNotLooseTrailingZerosByTrimmingLeadingZeros()
        {
            // arrange
            // generic setup, so the rest would work
            var matchingFirstName = _faker.Random.Hash();
            var matchingLastName = _faker.Random.Hash();
            var dobYear = _faker.Random.Hash();

            // need to make sure we don't lose the non-leading zero's upon removing leading ones
            var dobDay1 = "1";
            var dobMonth1 = "1";

            var dobDay10 = "10";
            var dobMonth10 = "10";

            // request date = 10, db date = 1
            var searchParametersDate10 = new FindResident
            {
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = dobDay10,
                DobMonth = dobMonth10,
                DobYear = dobYear,
            };

            // Should not match. If it matches, it meanst the zero from "10" gets trimmed from request field
            var existingResidentDate1InDB = new ResidentEntity
            {
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = dobDay1,
                DobMonth = dobMonth1,
                DobYear = dobYear
            };

            DatabaseContext.ResidentEntities.Add(existingResidentDate1InDB);

            var dobDay2 = "2";
            var dobMonth2 = "2";

            var dobDay20 = "20";
            var dobMonth20 = "20";

            // request date = 2, db date = 20
            var searchParametersDate2 = new FindResident
            {
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = dobDay2,
                DobMonth = dobMonth2,
                DobYear = dobYear,
            };

            // Should not match. If it matches, it meanst the zero from "20" gets trimmed from DB field
            var existingResidentDate20InDB = new ResidentEntity
            {
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = dobDay20,
                DobMonth = dobMonth20,
                DobYear = dobYear
            };

            DatabaseContext.ResidentEntities.Add(existingResidentDate20InDB);
            DatabaseContext.SaveChanges();

            // act
            var duplicateResidentDate1Id = _classUnderTest.FindResident(searchParametersDate10);
            bool isResidentDate1Duplicate = duplicateResidentDate1Id != null;

            var duplicateResidentDate20Id = _classUnderTest.FindResident(searchParametersDate2);
            bool isResidentDate20Duplicate = duplicateResidentDate20Id != null;

            // assert
            isResidentDate1Duplicate.Should().BeFalse();
            duplicateResidentDate1Id.Should().Be(null);

            isResidentDate20Duplicate.Should().BeFalse();
            duplicateResidentDate20Id.Should().Be(null);
        }


        [Test]
        public void WhenDeduplicatingByDobRuleFindResidentReturnsAMatchEvenWhenDobDayAndMonthFieldsContainLeadingZeros()
        {
            // arrange
            // generic setup, so the rest would work
            var matchingFirstName = _faker.Random.Hash();
            var matchingLastName = _faker.Random.Hash();
            var dobYear = _faker.Random.Hash();

            // other case - leading 0s in db, not in request
            var requestDobDayAny = _faker.Random.Int(1, 4).ToString();
            var requestDobMonthAny = _faker.Random.Int(1, 4).ToString();
            var databaseDobDayAnyLeadingZero = "0" + requestDobDayAny;
            var databaseDobMonthAnyLeadingZero = "0" + requestDobMonthAny;

            // no leading zeros
            var searchParametersNoLeadingZeros = new FindResident
            {
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = requestDobDayAny,
                DobMonth = requestDobMonthAny,
                DobYear = dobYear,
            };

            // with leading zeros
            var existingResidentLeadingZeros = new ResidentEntity
            {
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = databaseDobDayAnyLeadingZero,
                DobMonth = databaseDobMonthAnyLeadingZero,
                DobYear = dobYear
            };

            DatabaseContext.ResidentEntities.Add(existingResidentLeadingZeros);

            // other case - leading 0s in request, not in db
            var databaseDobDayAny = _faker.Random.Int(5, 9).ToString();
            var databaseDobMonthAny = _faker.Random.Int(5, 9).ToString();
            var requestDobDayAnyLeadingZero = "0" + databaseDobDayAny;
            var requestDobMonthAnyLeadingZero = "0" + databaseDobMonthAny;

            // leading zeros
            var searchParametersWithLeadingZeros = new FindResident
            {
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = requestDobDayAnyLeadingZero,
                DobMonth = requestDobMonthAnyLeadingZero,
                DobYear = dobYear,
            };

            // no leading zeros
            var existingResidentNoneLeadingZeros = new ResidentEntity
            {
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = databaseDobDayAny,
                DobMonth = databaseDobMonthAny,
                DobYear = dobYear
            };

            DatabaseContext.ResidentEntities.Add(existingResidentNoneLeadingZeros);

            DatabaseContext.SaveChanges();

            // act
            var duplicateResidentLeadZerosInDbId = _classUnderTest.FindResident(searchParametersNoLeadingZeros);
            bool isResidentLeadZerosInDbDuplicate = duplicateResidentLeadZerosInDbId != null;

            var duplicateResidentLeadZerosInRequestId = _classUnderTest.FindResident(searchParametersWithLeadingZeros);
            bool isResidentLeadZerosInRequestDuplicate = duplicateResidentLeadZerosInRequestId != null;

            // assert
            isResidentLeadZerosInDbDuplicate.Should().BeTrue();
            duplicateResidentLeadZerosInDbId.Should().Be(existingResidentLeadingZeros.Id);

            isResidentLeadZerosInRequestDuplicate.Should().BeTrue();
            duplicateResidentLeadZerosInRequestId.Should().Be(existingResidentNoneLeadingZeros.Id);
        }

        // When de-duplicating by nhsnumber, number formatting is ignored
        // To simplify setup, I didn't do mixed case, but making request and db casing different should cover the cases implicitly
        [Test]
        public void UponDeduplicatingByDobRuleFindResidentFindsAMatchEvenWhenDobMonthIsAMonthNameAndCasingDiffers()
        {
            // arrange
            // generic setup, so the rest would work
            var matchingFirstName = _faker.Random.Hash();
            var matchingLastName = _faker.Random.Hash();
            var dobYear = _faker.Random.Hash();
            var dobDay = _faker.Random.Hash();

            // case: Request Lowercase, Database Uppercase (dobMonth text)
            // hash is a complex string like "Jan", "Dec", "April" already, so that part of the test is long covered
            // what's left is to test whether matching happens on different casing.
            var dobMonthRLDU = _faker.Random.Hash();

            var searchParametersLowerCasing = new FindResident
            {
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = dobDay,
                DobMonth = dobMonthRLDU.ToLower(),
                DobYear = dobYear,
            };

            var existingResidentInUpperCasing = new ResidentEntity
            {
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = dobDay,
                DobMonth = dobMonthRLDU.ToUpper(),
                DobYear = dobYear
            };

            DatabaseContext.ResidentEntities.Add(existingResidentInUpperCasing);

            // case: Request Uppercase, Database Lowercase (dobMonth text)
            var dobMonthRUDL = _faker.Random.Hash();

            var searchParametersUpperCasing = new FindResident
            {
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = dobDay,
                DobMonth = dobMonthRUDL.ToUpper(),
                DobYear = dobYear,
            };

            var existingResidentInLowerCasing = new ResidentEntity
            {
                FirstName = matchingFirstName,
                LastName = matchingLastName,
                DobDay = dobDay,
                DobMonth = dobMonthRUDL.ToLower(),
                DobYear = dobYear
            };

            DatabaseContext.ResidentEntities.Add(existingResidentInLowerCasing);
            DatabaseContext.SaveChanges();

            // act
            var duplicateResidentUpperCasingId = _classUnderTest.FindResident(searchParametersLowerCasing);
            bool isResidentUpperCasingDuplicate = duplicateResidentUpperCasingId != null;

            var duplicateResidentLowerCasingId = _classUnderTest.FindResident(searchParametersUpperCasing);
            bool isResidentLowerCasingDuplicate = duplicateResidentLowerCasingId != null;

            // assert
            isResidentUpperCasingDuplicate.Should().BeTrue();
            duplicateResidentUpperCasingId.Should().Be(existingResidentInUpperCasing.Id);

            isResidentLowerCasingDuplicate.Should().BeTrue();
            duplicateResidentLowerCasingId.Should().Be(existingResidentInLowerCasing.Id);
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
