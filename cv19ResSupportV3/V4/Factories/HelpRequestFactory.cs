using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V4.Boundary.Requests;
using cv19ResSupportV3.V4.Boundary.Response;

namespace cv19ResSupportV3.V4.Factories
{
    public static class HelpRequestFactory
    {
        public static CreateHelpRequest ToCommand(this ResidentHelpRequestRequest request)
        {
            return request == null
                ? null
                : new CreateHelpRequest
                {
                    IsOnBehalf = request.IsOnBehalf,
                    ConsentToCompleteOnBehalf = request.ConsentToCompleteOnBehalf,
                    OnBehalfFirstName = request.OnBehalfFirstName,
                    OnBehalfLastName = request.OnBehalfLastName,
                    OnBehalfEmailAddress = request.OnBehalfEmailAddress,
                    OnBehalfContactNumber = request.OnBehalfContactNumber,
                    RelationshipWithResident = request.RelationshipWithResident,
                    GettingInTouchReason = request.GettingInTouchReason,
                    HelpWithAccessingFood = request.HelpWithAccessingFood,
                    HelpWithAccessingSupermarketFood = request.HelpWithAccessingSupermarketFood,
                    HelpWithCompletingNssForm = request.HelpWithCompletingNssForm,
                    HelpWithShieldingGuidance = request.HelpWithShieldingGuidance,
                    HelpWithNoNeedsIdentified = request.HelpWithNoNeedsIdentified,
                    HelpWithAccessingMedicine = request.HelpWithAccessingMedicine,
                    HelpWithAccessingOtherEssentials = request.HelpWithAccessingOtherEssentials,
                    HelpWithDebtAndMoney = request.HelpWithDebtAndMoney,
                    HelpWithHealth = request.HelpWithHealth,
                    HelpWithMentalHealth = request.HelpWithMentalHealth,
                    HelpWithAccessingInternet = request.HelpWithAccessingFood,
                    HelpWithHousing = request.HelpWithHousing,
                    HelpWithJobsOrTraining = request.HelpWithJobsOrTraining,
                    HelpWithChildrenAndSchools = request.HelpWithChildrenAndSchools,
                    HelpWithDisabilities = request.HelpWithDisabilities,
                    HelpWithSomethingElse = request.HelpWithSomethingElse,
                    MedicineDeliveryHelpNeeded = request.MedicineDeliveryHelpNeeded,
                    WhenIsMedicinesDelivered = request.WhenIsMedicinesDelivered,
                    UrgentEssentials = request.UrgentEssentials,
                    UrgentEssentialsAnythingElse = request.UrgentEssentials,
                    CurrentSupport = request.CurrentSupport,
                    CurrentSupportFeedback = request.CurrentSupportFeedback,
                    DateTimeRecorded = request.DateTimeRecorded,
                    InitialCallbackCompleted = request.InitialCallbackCompleted,
                    CallbackRequired = request.CallbackRequired,
                    AdviceNotes = request.AdviceNotes,
                    HelpNeeded = request.HelpNeeded,
                    AssignedTo = request.AssignedTo,
                    NhsCtasId = request.NhsCtasId
                };
        }

        public static PatchHelpRequest ToPatchHelpRequest(this ResidentHelpRequestRequest request)
        {
            return request == null
                ? null
                : new PatchHelpRequest
                {
                    GettingInTouchReason = request.GettingInTouchReason,
                    HelpWithAccessingFood = request.HelpWithAccessingFood,
                    HelpWithAccessingSupermarketFood = request.HelpWithAccessingSupermarketFood,
                    HelpWithCompletingNssForm = request.HelpWithCompletingNssForm,
                    HelpWithShieldingGuidance = request.HelpWithShieldingGuidance,
                    HelpWithNoNeedsIdentified = request.HelpWithNoNeedsIdentified,
                    HelpWithAccessingMedicine = request.HelpWithAccessingMedicine,
                    HelpWithAccessingOtherEssentials = request.HelpWithAccessingOtherEssentials,
                    HelpWithDebtAndMoney = request.HelpWithDebtAndMoney,
                    HelpWithHealth = request.HelpWithHealth,
                    HelpWithMentalHealth = request.HelpWithMentalHealth,
                    HelpWithAccessingInternet = request.HelpWithAccessingFood,
                    HelpWithSomethingElse = request.HelpWithSomethingElse,
                    CurrentSupport = request.CurrentSupport,
                    CurrentSupportFeedback = request.CurrentSupportFeedback,
                    InitialCallbackCompleted = request.InitialCallbackCompleted,
                    CallbackRequired = request.CallbackRequired,
                    AdviceNotes = request.AdviceNotes,
                    HelpNeeded = request.HelpNeeded,
                    AssignedTo = request.AssignedTo,
                    NhsCtasId = request.NhsCtasId
                };
        }

        public static ResidentHelpRequestResponse ToResponse(this HelpRequest domain)
        {
            return domain == null
                ? null
                : new ResidentHelpRequestResponse
                {
                    Id = domain.Id,
                    IsOnBehalf = domain.IsOnBehalf,
                    ConsentToCompleteOnBehalf = domain.ConsentToCompleteOnBehalf,
                    OnBehalfFirstName = domain.OnBehalfFirstName,
                    OnBehalfLastName = domain.OnBehalfLastName,
                    OnBehalfEmailAddress = domain.OnBehalfEmailAddress,
                    OnBehalfContactNumber = domain.OnBehalfContactNumber,
                    RelationshipWithResident = domain.RelationshipWithResident,
                    GettingInTouchReason = domain.GettingInTouchReason,
                    HelpWithAccessingFood = domain.HelpWithAccessingFood,
                    HelpWithAccessingSupermarketFood = domain.HelpWithAccessingSupermarketFood,
                    HelpWithCompletingNssForm = domain.HelpWithCompletingNssForm,
                    HelpWithShieldingGuidance = domain.HelpWithShieldingGuidance,
                    HelpWithNoNeedsIdentified = domain.HelpWithNoNeedsIdentified,
                    HelpWithAccessingMedicine = domain.HelpWithAccessingMedicine,
                    HelpWithAccessingOtherEssentials = domain.HelpWithAccessingOtherEssentials,
                    HelpWithDebtAndMoney = domain.HelpWithDebtAndMoney,
                    HelpWithHealth = domain.HelpWithHealth,
                    HelpWithMentalHealth = domain.HelpWithMentalHealth,
                    HelpWithAccessingInternet = domain.HelpWithAccessingFood,
                    HelpWithHousing = domain.HelpWithHousing,
                    HelpWithJobsOrTraining = domain.HelpWithJobsOrTraining,
                    HelpWithChildrenAndSchools = domain.HelpWithChildrenAndSchools,
                    HelpWithDisabilities = domain.HelpWithDisabilities,
                    HelpWithSomethingElse = domain.HelpWithSomethingElse,
                    MedicineDeliveryHelpNeeded = domain.MedicineDeliveryHelpNeeded,
                    WhenIsMedicinesDelivered = domain.WhenIsMedicinesDelivered,
                    UrgentEssentials = domain.UrgentEssentials,
                    UrgentEssentialsAnythingElse = domain.UrgentEssentials,
                    CurrentSupport = domain.CurrentSupport,
                    CurrentSupportFeedback = domain.CurrentSupportFeedback,
                    DateTimeRecorded = domain.DateTimeRecorded,
                    InitialCallbackCompleted = domain.InitialCallbackCompleted,
                    CallbackRequired = domain.CallbackRequired,
                    AdviceNotes = domain.AdviceNotes,
                    HelpNeeded = domain.HelpNeeded,
                    AssignedTo = domain.AssignedTo,
                    NhsCtasId = domain.NhsCtasId
                };
        }

        public static ResidentHelpRequestResponse ToResidentHelpRequestResponse(this HelpRequestWithResident domain)
        {
            return domain == null
                ? null
                : new ResidentHelpRequestResponse
                {
                    Id = domain.Id,
                    ResidentId = domain.ResidentId,
                    IsOnBehalf = domain.IsOnBehalf,
                    ConsentToCompleteOnBehalf = domain.ConsentToCompleteOnBehalf,
                    OnBehalfFirstName = domain.OnBehalfFirstName,
                    OnBehalfLastName = domain.OnBehalfLastName,
                    OnBehalfEmailAddress = domain.OnBehalfEmailAddress,
                    OnBehalfContactNumber = domain.OnBehalfContactNumber,
                    RelationshipWithResident = domain.RelationshipWithResident,
                    GettingInTouchReason = domain.GettingInTouchReason,
                    HelpWithAccessingFood = domain.HelpWithAccessingFood,
                    HelpWithAccessingSupermarketFood = domain.HelpWithAccessingSupermarketFood,
                    HelpWithCompletingNssForm = domain.HelpWithCompletingNssForm,
                    HelpWithShieldingGuidance = domain.HelpWithShieldingGuidance,
                    HelpWithNoNeedsIdentified = domain.HelpWithNoNeedsIdentified,
                    HelpWithAccessingMedicine = domain.HelpWithAccessingMedicine,
                    HelpWithAccessingOtherEssentials = domain.HelpWithAccessingOtherEssentials,
                    HelpWithDebtAndMoney = domain.HelpWithDebtAndMoney,
                    HelpWithHealth = domain.HelpWithHealth,
                    HelpWithMentalHealth = domain.HelpWithMentalHealth,
                    HelpWithAccessingInternet = domain.HelpWithAccessingFood,
                    HelpWithHousing = domain.HelpWithHousing,
                    HelpWithJobsOrTraining = domain.HelpWithJobsOrTraining,
                    HelpWithChildrenAndSchools = domain.HelpWithChildrenAndSchools,
                    HelpWithDisabilities = domain.HelpWithDisabilities,
                    HelpWithSomethingElse = domain.HelpWithSomethingElse,
                    MedicineDeliveryHelpNeeded = domain.MedicineDeliveryHelpNeeded,
                    WhenIsMedicinesDelivered = domain.WhenIsMedicinesDelivered,
                    UrgentEssentials = domain.UrgentEssentials,
                    UrgentEssentialsAnythingElse = domain.UrgentEssentials,
                    CurrentSupport = domain.CurrentSupport,
                    CurrentSupportFeedback = domain.CurrentSupportFeedback,
                    DateTimeRecorded = domain.DateTimeRecorded,
                    InitialCallbackCompleted = domain.InitialCallbackCompleted,
                    CallbackRequired = domain.CallbackRequired,
                    AdviceNotes = domain.AdviceNotes,
                    HelpNeeded = domain.HelpNeeded,
                    AssignedTo = domain.AssignedTo,
                    NhsCtasId = domain.NhsCtasId
                };
        }

        public static List<ResidentHelpRequestResponse> ToResidentHelpRequestResponse(
            this IEnumerable<HelpRequestWithResident> requests)
        {
            return requests?.Select(rq => rq.ToResidentHelpRequestResponse()).ToList();
        }
    }
}
