using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.Factories.Commands
{
    public static class PatchHelpRequestFactory
    {
        public static PatchResidentAndHelpRequest ToCommand(this HelpRequestPatchRequest helpRequest)
        {
            return new PatchResidentAndHelpRequest
            {
                Postcode = helpRequest.Postcode,
                Uprn = helpRequest.Uprn,
                Ward = helpRequest.Ward,
                AddressFirstLine = helpRequest.AddressFirstLine,
                AddressSecondLine = helpRequest.AddressSecondLine,
                AddressThirdLine = helpRequest.AddressThirdLine,
                GettingInTouchReason = helpRequest.GettingInTouchReason,
                HelpWithAccessingFood = helpRequest.HelpWithAccessingFood,
                HelpWithAccessingSupermarketFood = helpRequest.HelpWithAccessingSupermarketFood,
                HelpWithCompletingNssForm = helpRequest.HelpWithCompletingNssForm,
                HelpWithShieldingGuidance = helpRequest.HelpWithShieldingGuidance,
                HelpWithNoNeedsIdentified = helpRequest.HelpWithNoNeedsIdentified,
                HelpWithAccessingMedicine = helpRequest.HelpWithAccessingMedicine,
                HelpWithAccessingOtherEssentials = helpRequest.HelpWithAccessingOtherEssentials,
                HelpWithDebtAndMoney = helpRequest.HelpWithDebtAndMoney,
                HelpWithHealth = helpRequest.HelpWithHealth,
                HelpWithMentalHealth = helpRequest.HelpWithMentalHealth,
                HelpWithAccessingInternet = helpRequest.HelpWithAccessingInternet,
                HelpWithSomethingElse = helpRequest.HelpWithSomethingElse,
                CurrentSupport = helpRequest.CurrentSupport,
                CurrentSupportFeedback = helpRequest.CurrentSupportFeedback,
                FirstName = helpRequest.FirstName,
                LastName = helpRequest.LastName,
                DobMonth = helpRequest.DobMonth,
                DobYear = helpRequest.DobYear,
                DobDay = helpRequest.DobDay,
                ContactTelephoneNumber = helpRequest.ContactTelephoneNumber,
                ContactMobileNumber = helpRequest.ContactMobileNumber,
                EmailAddress = helpRequest.EmailAddress,
                GpSurgeryDetails = helpRequest.GpSurgeryDetails,
                NumberOfChildrenUnder18 = helpRequest.NumberOfChildrenUnder18,
                ConsentToShare = helpRequest.ConsentToShare,
                RecordStatus = helpRequest.RecordStatus,
                CallbackRequired = helpRequest.CallbackRequired,
                InitialCallbackCompleted = helpRequest.InitialCallbackCompleted,
                CaseNotes = helpRequest.CaseNotes,
                AdviceNotes = helpRequest.AdviceNotes,
                NhsNumber = helpRequest.NhsNumber,
                NhsCtasId = helpRequest.NhsCtasId,
                AssignedTo = helpRequest.AssignedTo,
                HelpNeeded = helpRequest.HelpNeeded,
            };
        }

        public static PatchHelpRequest ToPatchHelpRequestCommand(this PatchResidentAndHelpRequest command)
        {
            return new PatchHelpRequest()
            {
                GettingInTouchReason = command.GettingInTouchReason,
                HelpWithAccessingFood = command.HelpWithAccessingFood,
                HelpWithAccessingSupermarketFood = command.HelpWithAccessingSupermarketFood,
                HelpWithCompletingNssForm = command.HelpWithCompletingNssForm,
                HelpWithShieldingGuidance = command.HelpWithShieldingGuidance,
                HelpWithNoNeedsIdentified = command.HelpWithNoNeedsIdentified,
                HelpWithAccessingMedicine = command.HelpWithAccessingMedicine,
                HelpWithAccessingOtherEssentials = command.HelpWithAccessingOtherEssentials,
                HelpWithDebtAndMoney = command.HelpWithDebtAndMoney,
                HelpWithHealth = command.HelpWithHealth,
                HelpWithMentalHealth = command.HelpWithMentalHealth,
                HelpWithAccessingInternet = command.HelpWithAccessingInternet,
                HelpWithSomethingElse = command.HelpWithSomethingElse,
                CurrentSupport = command.CurrentSupport,
                CurrentSupportFeedback = command.CurrentSupportFeedback,
                GpSurgeryDetails = command.GpSurgeryDetails,
                NumberOfChildrenUnder18 = command.NumberOfChildrenUnder18,
                ConsentToShare = command.ConsentToShare,
                CallbackRequired = command.CallbackRequired,
                InitialCallbackCompleted = command.InitialCallbackCompleted,
                AdviceNotes = command.AdviceNotes,
                NhsCtasId = command.NhsCtasId,
                AssignedTo = command.AssignedTo,
                HelpNeeded = command.HelpNeeded,
            };
        }

        public static PatchResident ToPatchResidentCommand(this PatchResidentAndHelpRequest command)
        {
            return new PatchResident
            {
                Postcode = command.Postcode,
                Uprn = command.Uprn,
                Ward = command.Ward,
                AddressFirstLine = command.AddressFirstLine,
                AddressSecondLine = command.AddressSecondLine,
                AddressThirdLine = command.AddressThirdLine,
                FirstName = command.FirstName,
                LastName = command.LastName,
                DobMonth = command.DobMonth,
                DobYear = command.DobYear,
                DobDay = command.DobDay,
                ContactTelephoneNumber = command.ContactTelephoneNumber,
                ContactMobileNumber = command.ContactMobileNumber,
                EmailAddress = command.EmailAddress,
                GpSurgeryDetails = command.GpSurgeryDetails,
                NumberOfChildrenUnder18 = command.NumberOfChildrenUnder18,
                ConsentToShare = command.ConsentToShare,
                RecordStatus = command.RecordStatus,
                NhsNumber = command.NhsNumber
            };
        }
    }
}
