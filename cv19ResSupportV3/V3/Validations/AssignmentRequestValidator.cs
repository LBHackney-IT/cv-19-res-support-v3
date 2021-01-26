using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Boundary.Requests;

namespace cv19ResSupportV3.V3.Validations
{
    public static class AssignmentRequestValidator
    {
        public static ValidationResponse Validate(this UpdateStaffAssignmentsRequestBoundary request)
        {
            var response = new ValidationResponse();
            response.IsValid = ValidationResult.Valid;
            if (string.IsNullOrWhiteSpace(request.HelpNeeded))
                response.Errors.Add("Help needed parameter not provided");
            if (request.StaffMembers == null)
                response.Errors.Add("Staff member list not provided");
            if (response.Errors.Count > 0)
                response.IsValid = ValidationResult.Invalid;
            return response;
        }
    }

    public enum ValidationResult
    {
        Valid,
        Invalid
    }

    public class ValidationResponse
    {
        public ValidationResponse()
        {
            Errors = new List<string>();
        }
        public ValidationResult IsValid { get; set; }
        public List<string> Errors { get; set; }
    }
}
