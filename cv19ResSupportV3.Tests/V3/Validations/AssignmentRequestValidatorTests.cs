using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Validations;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Validations
{
    [TestFixture]
    public class AssignmentRequestValidatorTests
    {
        [Test]
        public void AssignmentValidatorReturnsValidIfAllParametersAreSupplied()
        {
            var request = Randomm.Create<UpdateStaffAssignmentsRequestBoundary>();
            var response = request.Validate();
            response.IsValid.Should().Be(ValidationResult.Valid);
            response.Errors.Count.Should().Be(0);
        }

        [Test]
        public void AssignmentValidatorReturnsInvalidWithErrorIfHelpNeededParameterIsNotSupplied()
        {
            var request = Randomm.Create<UpdateStaffAssignmentsRequestBoundary>();
            request.HelpNeeded = "";
            var response = request.Validate();
            response.IsValid.Should().Be(ValidationResult.Invalid);
            response.Errors.Count.Should().Be(1);
            response.Errors[0].Should().Be("Help needed parameter not provided");
        }

        [Test]
        public void AssignmentValidatorReturnsInvalidWithErrorIfStaffMembersParameterIsNotSupplied()
        {
            var request = Randomm.Create<UpdateStaffAssignmentsRequestBoundary>();
            request.StaffMembers = null;
            var response = request.Validate();
            response.IsValid.Should().Be(ValidationResult.Invalid);
            response.Errors.Count.Should().Be(1);
            response.Errors[0].Should().Be("Staff member list not provided");
        }

        [Test]
        public void AssignmentValidatorReturnsInvalidWithErrorsIfBothParametersAreNotSupplied()
        {
            var request = Randomm.Create<UpdateStaffAssignmentsRequestBoundary>();
            request.HelpNeeded = "";
            request.StaffMembers = null;
            var response = request.Validate();
            response.IsValid.Should().Be(ValidationResult.Invalid);
            response.Errors.Count.Should().Be(2);
            response.Errors[0].Should().Be("Help needed parameter not provided");
            response.Errors[1].Should().Be("Staff member list not provided");
        }
    }
}
