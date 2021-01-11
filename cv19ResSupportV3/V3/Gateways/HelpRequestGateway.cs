using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Amazon.Lambda.Core;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using Microsoft.EntityFrameworkCore;
using HelpRequest = cv19ResSupportV3.V3.Domain.HelpRequest;

namespace cv19ResSupportV3.V3.Gateways
{
    public class HelpRequestGateway : IHelpRequestGateway
    {

        private readonly HelpRequestsContext _helpRequestsContext;

        public HelpRequestGateway(HelpRequestsContext helpRequestsContext)
        {
            _helpRequestsContext = helpRequestsContext;
        }

        public int CreateHelpRequest(CreateHelpRequest command)
        {
            var requestEntity = command.ToEntity();
            //check the resident by firstName, lastName, DoB
            if (requestEntity == null) return 0;
            SetRecordStatus(requestEntity);
            requestEntity.CallbackRequired ??= true;
            try
            {
                _helpRequestsContext.HelpRequestEntities.Add(requestEntity);
                _helpRequestsContext.SaveChanges();
                return requestEntity.Id;
            }
            catch (Exception e)
            {
                LambdaLogger.Log("CreateHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public List<LookupEntity> GetLookups(LookupQueryParams requestParams)
        {
            Expression<Func<LookupEntity, bool>> queryLookups = x =>
                string.IsNullOrWhiteSpace(requestParams.LookupGroup)
                || x.LookupGroup.Replace(" ", "").ToUpper().Equals(requestParams.LookupGroup.Replace(" ", "").ToUpper());
            try
            {
                var response = _helpRequestsContext.Lookups
                    .Where(queryLookups)
                    .OrderBy(x => x.LookupGroup)
                    .ThenBy(x => x.Lookup)
                    .ToList();
                return response;
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetCallbacks error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public HelpRequest UpdateHelpRequest(UpdateHelpRequest command)
        {
            if (command == null) return null;
            try
            {
                var entity = _helpRequestsContext.HelpRequestEntities.Find(command.Id);
                _helpRequestsContext.Entry(entity).CurrentValues.SetValues(command);
                _helpRequestsContext.SaveChanges();
                return entity.ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("UpdateHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public HelpRequest GetHelpRequest(int id)
        {
            try
            {
                var result = _helpRequestsContext.HelpRequestEntities
                    .Include(x => x.HelpRequestCalls)
                    .FirstOrDefault(x => x.Id == id);
                return result?.ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public List<HelpRequest> SearchHelpRequests(RequestQueryParams queryParams)
        {
            Expression<Func<HelpRequestEntity, bool>> queryPostCode = x =>
                string.IsNullOrWhiteSpace(queryParams.Postcode)
                || x.PostCode.Replace(" ", "").ToUpper().Contains(queryParams.Postcode.Replace(" ", "").ToUpper());

            Expression<Func<HelpRequestEntity, bool>> queryFirstName = x =>
                string.IsNullOrWhiteSpace(queryParams.FirstName)
                || x.FirstName.Replace(" ", "").ToUpper().Contains(queryParams.FirstName.Replace(" ", "").ToUpper());

            Expression<Func<HelpRequestEntity, bool>> queryLastName = x =>
                string.IsNullOrWhiteSpace(queryParams.LastName)
                || x.LastName.Replace(" ", "").ToUpper().Contains(queryParams.LastName.Replace(" ", "").ToUpper());

            Expression<Func<HelpRequestEntity, bool>> queryHelpNeeded = x =>
                string.IsNullOrWhiteSpace(queryParams.HelpNeeded)
                || x.HelpNeeded.Replace(" ", "").ToUpper().Equals(queryParams.HelpNeeded.Replace(" ", "").ToUpper());

            try
            {
                return _helpRequestsContext.HelpRequestEntities
                    .Include(x => x.HelpRequestCalls)
                    .Where(queryPostCode)
                    .Where(queryFirstName)
                    .Where(queryLastName)
                    .Where(queryHelpNeeded)
                    .ToList()
                    .ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("SearchHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public void PatchHelpRequest(int id, PatchHelpRequest command)
        {
            try
            {
                var requestEntity = command.ToEntity();
                var rec = _helpRequestsContext.HelpRequestEntities.SingleOrDefault(x => x.Id == id);
                if (requestEntity == null)
                {
                    throw new Exception("Record not found.");
                }
                if (requestEntity.GettingInTouchReason != null)
                {
                    rec.GettingInTouchReason = requestEntity.GettingInTouchReason;
                }
                if (requestEntity.HelpWithAccessingFood != null)
                {
                    rec.HelpWithAccessingFood = requestEntity.HelpWithAccessingFood;
                }
                if (requestEntity.HelpWithAccessingSupermarketFood != null)
                {
                    rec.HelpWithAccessingSupermarketFood = requestEntity.HelpWithAccessingSupermarketFood;
                }
                if (requestEntity.HelpWithCompletingNssForm != null)
                {
                    rec.HelpWithCompletingNssForm = requestEntity.HelpWithCompletingNssForm;
                }
                if (requestEntity.HelpWithShieldingGuidance != null)
                {
                    rec.HelpWithShieldingGuidance = requestEntity.HelpWithShieldingGuidance;
                }
                if (requestEntity.HelpWithNoNeedsIdentified != null)
                {
                    rec.HelpWithNoNeedsIdentified = requestEntity.HelpWithNoNeedsIdentified;
                }
                if (requestEntity.HelpWithAccessingMedicine != null)
                {
                    rec.HelpWithAccessingMedicine = requestEntity.HelpWithAccessingMedicine;
                }
                if (requestEntity.HelpWithAccessingOtherEssentials != null)
                {
                    rec.HelpWithAccessingOtherEssentials = requestEntity.HelpWithAccessingOtherEssentials;
                }
                if (requestEntity.HelpWithDebtAndMoney != null)
                {
                    rec.HelpWithDebtAndMoney = requestEntity.HelpWithDebtAndMoney;
                }
                if (requestEntity.HelpWithMentalHealth != null)
                {
                    rec.HelpWithMentalHealth = requestEntity.HelpWithMentalHealth;
                }
                if (requestEntity.HelpWithHealth != null)
                {
                    rec.HelpWithHealth = requestEntity.HelpWithHealth;
                }
                if (requestEntity.HelpWithAccessingInternet != null)
                {
                    rec.HelpWithAccessingInternet = requestEntity.HelpWithAccessingInternet;
                }
                if (requestEntity.HelpWithSomethingElse != null)
                {
                    rec.HelpWithSomethingElse = requestEntity.HelpWithSomethingElse;
                }
                if (requestEntity.CurrentSupport != null)
                {
                    rec.CurrentSupport = requestEntity.CurrentSupport;
                }

                if (requestEntity.CurrentSupportFeedback != null)
                {
                    rec.CurrentSupportFeedback = requestEntity.CurrentSupportFeedback;
                }

                if (requestEntity.FirstName != null)
                {
                    rec.FirstName = requestEntity.FirstName;
                }

                if (requestEntity.LastName != null)
                {
                    rec.LastName = requestEntity.LastName;
                }

                if (requestEntity.DobMonth != null)
                {
                    rec.DobMonth = requestEntity.DobMonth;
                }

                if (requestEntity.DobYear != null)
                {
                    rec.DobYear = requestEntity.DobYear;
                }

                if (requestEntity.DobDay != null)
                {
                    rec.DobDay = requestEntity.DobDay;
                }

                if (requestEntity.ContactTelephoneNumber != null)
                {
                    rec.ContactTelephoneNumber = requestEntity.ContactTelephoneNumber;
                }

                if (requestEntity.ContactMobileNumber != null)
                {
                    rec.ContactMobileNumber = requestEntity.ContactMobileNumber;
                }

                if (requestEntity.EmailAddress != null)
                {
                    rec.EmailAddress = requestEntity.EmailAddress;
                }

                if (requestEntity.AddressFirstLine != null && requestEntity.PostCode != null)
                {
                    // update new address fields
                    rec.AddressFirstLine = requestEntity.AddressFirstLine;
                    rec.AddressSecondLine = requestEntity.AddressSecondLine;
                    rec.AddressThirdLine = requestEntity.AddressThirdLine;
                    rec.PostCode = requestEntity.PostCode;
                    rec.Uprn = requestEntity.Uprn;
                    rec.Ward = requestEntity.Ward;
                }

                if (requestEntity.GpSurgeryDetails != null)
                {
                    rec.GpSurgeryDetails = requestEntity.GpSurgeryDetails;
                }

                if (requestEntity.NumberOfChildrenUnder18 != null)
                {
                    rec.NumberOfChildrenUnder18 = requestEntity.NumberOfChildrenUnder18;
                }

                if (requestEntity.ConsentToShare != null)
                {
                    rec.ConsentToShare = requestEntity.ConsentToShare;
                }

                if (requestEntity.CaseNotes != null)
                {
                    rec.CaseNotes = requestEntity.CaseNotes;
                }

                if (requestEntity.AdviceNotes != null)
                {
                    rec.AdviceNotes = requestEntity.AdviceNotes;
                }

                if (requestEntity.InitialCallbackCompleted != null)
                {
                    rec.InitialCallbackCompleted = requestEntity.InitialCallbackCompleted;
                }

                if (requestEntity.CallbackRequired != null)
                {
                    rec.CallbackRequired = requestEntity.CallbackRequired;
                }

                if (requestEntity.RecordStatus != null)
                {
                    rec.RecordStatus = requestEntity.RecordStatus;
                }

                if (requestEntity.HelpNeeded != null)
                {
                    rec.HelpNeeded = requestEntity.HelpNeeded;
                }
                _helpRequestsContext.SaveChanges();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("PatchHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public List<HelpRequestEntity> GetCallbacks(CallbackRequestParams requestParams)
        {
            Expression<Func<HelpRequestEntity, bool>> queryHelpNeeded = x =>
                string.IsNullOrWhiteSpace(requestParams.HelpNeeded)
                || x.HelpNeeded.Replace(" ", "").ToUpper().Equals(requestParams.HelpNeeded.Replace(" ", "").ToUpper());
            try
            {
                var response = _helpRequestsContext.HelpRequestEntities.Include(x => x.HelpRequestCalls)
                    .Where(x => (x.CallbackRequired == true || x.CallbackRequired == null ||
                                 (x.InitialCallbackCompleted == false && x.CallbackRequired == false)))
                    .Where(queryHelpNeeded)
                    .OrderByDescending(x => x.InitialCallbackCompleted)
                    .ThenBy(x => x.DateTimeRecorded)
                    .ToList();
                if (!string.IsNullOrWhiteSpace(requestParams.Master))
                {
                    return response.Where(x => x.RecordStatus != null && x.RecordStatus.Replace(" ", "").ToUpper() == "MASTER").ToList();
                }
                return response;
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetCallbacks error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        private void SetRecordStatus(HelpRequestEntity request)
        {
            request.RecordStatus = "MASTER";
            var duplicates = _helpRequestsContext.HelpRequestEntities
                .Where(x => x.Uprn == request.Uprn && x.DobMonth == request.DobMonth
                                                   && x.DobDay == request.DobDay && x.DobYear == request.DobYear &&
                                                   x.ContactTelephoneNumber == request.ContactTelephoneNumber &&
                                                   x.ContactMobileNumber == request.ContactMobileNumber).ToList();
            if (duplicates.Count > 0)
            {
                foreach (var record in duplicates)
                {
                    var rec = _helpRequestsContext.HelpRequestEntities.Find(record.Id);
                    rec.RecordStatus = "DUPLICATE";
                }
            }
            else
            {
                var exceptions = _helpRequestsContext.HelpRequestEntities
                    .Where(x => x.Uprn == request.Uprn).ToList();
                if (exceptions.Count > 0)
                {
                    request.RecordStatus = "EXCEPTION";
                }
            }

        }
    }
}
