using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Amazon.Lambda.Core;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
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

        public int CreateHelpRequest(HelpRequestEntity request)
        {
            if (request == null) return 0;
            SetRecordStatus(request);
            request.CallbackRequired ??= true;
            try
            {
                _helpRequestsContext.HelpRequestEntities.Add(request);
                _helpRequestsContext.SaveChanges();
                return request.Id;
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

        public HelpRequestEntity UpdateHelpRequest(HelpRequestEntity request)
        {
            if (request == null) return null;
            try
            {
                var entity = _helpRequestsContext.HelpRequestEntities.Find(request.Id);
                _helpRequestsContext.Entry(entity).CurrentValues.SetValues(request);
                _helpRequestsContext.SaveChanges();
                return entity;
            }
            catch (Exception e)
            {
                LambdaLogger.Log("UpdateHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public HelpRequestEntity GetHelpRequest(int id)
        {
            try
            {
                return _helpRequestsContext.HelpRequestEntities
                    .FirstOrDefault(x => x.Id == id);
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public List<HelpRequestEntity> SearchHelpRequests(RequestQueryParams queryParams)
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
                    .Where(queryPostCode)
                    .Where(queryFirstName)
                    .Where(queryLastName)
                    .Where(queryHelpNeeded)
                    .ToList();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("SearchHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public void PatchHelpRequest(int id, HelpRequestEntity request)
        {
            try
            {
                var rec = _helpRequestsContext.HelpRequestEntities.SingleOrDefault(x => x.Id == id);
                if (request == null)
                {
                    throw new Exception("Record not found.");
                }
                if (request.GettingInTouchReason != null)
                {
                    rec.GettingInTouchReason = request.GettingInTouchReason;
                }
                if (request.HelpWithAccessingFood != null)
                {
                    rec.HelpWithAccessingFood = request.HelpWithAccessingFood;
                }
                if (request.HelpWithAccessingMedicine != null)
                {
                    rec.HelpWithAccessingMedicine = request.HelpWithAccessingMedicine;
                }
                if (request.HelpWithAccessingOtherEssentials != null)
                {
                    rec.HelpWithAccessingOtherEssentials = request.HelpWithAccessingOtherEssentials;
                }
                if (request.HelpWithDebtAndMoney != null)
                {
                    rec.HelpWithDebtAndMoney = request.HelpWithDebtAndMoney;
                }
                if (request.HelpWithMentalHealth != null)
                {
                    rec.HelpWithMentalHealth = request.HelpWithMentalHealth;
                }
                if (request.HelpWithHealth != null)
                {
                    rec.HelpWithHealth = request.HelpWithHealth;
                }
                if (request.HelpWithAccessingInternet != null)
                {
                    rec.HelpWithAccessingInternet = request.HelpWithAccessingInternet;
                }
                if (request.HelpWithSomethingElse != null)
                {
                    rec.HelpWithSomethingElse = request.HelpWithSomethingElse;
                }
                if (request.CurrentSupport != null)
                {
                    rec.CurrentSupport = request.CurrentSupport;
                }

                if (request.CurrentSupportFeedback != null)
                {
                    rec.CurrentSupportFeedback = request.CurrentSupportFeedback;
                }

                if (request.FirstName != null)
                {
                    rec.FirstName = request.FirstName;
                }

                if (request.LastName != null)
                {
                    rec.LastName = request.LastName;
                }

                if (request.DobMonth != null)
                {
                    rec.DobMonth = request.DobMonth;
                }

                if (request.DobYear != null)
                {
                    rec.DobYear = request.DobYear;
                }

                if (request.DobDay != null)
                {
                    rec.DobDay = request.DobDay;
                }

                if (request.ContactTelephoneNumber != null)
                {
                    rec.ContactTelephoneNumber = request.ContactTelephoneNumber;
                }

                if (request.ContactMobileNumber != null)
                {
                    rec.ContactMobileNumber = request.ContactMobileNumber;
                }

                if (request.EmailAddress != null)
                {
                    rec.EmailAddress = request.EmailAddress;
                }

                if (request.AddressFirstLine != null && request.PostCode != null)
                {
                    // update new address fields
                    rec.AddressFirstLine = request.AddressFirstLine;
                    rec.AddressSecondLine = request.AddressSecondLine;
                    rec.AddressThirdLine = request.AddressThirdLine;
                    rec.PostCode = request.PostCode;
                    rec.Uprn = request.Uprn;
                    rec.Ward = request.Ward;
                }

                if (request.GpSurgeryDetails != null)
                {
                    rec.GpSurgeryDetails = request.GpSurgeryDetails;
                }

                if (request.NumberOfChildrenUnder18 != null)
                {
                    rec.NumberOfChildrenUnder18 = request.NumberOfChildrenUnder18;
                }

                if (request.ConsentToShare != null)
                {
                    rec.ConsentToShare = request.ConsentToShare;
                }

                if (request.CaseNotes != null)
                {
                    rec.CaseNotes = request.CaseNotes;
                }

                if (request.AdviceNotes != null)
                {
                    rec.AdviceNotes = request.AdviceNotes;
                }

                if (request.InitialCallbackCompleted != null)
                {
                    rec.InitialCallbackCompleted = request.InitialCallbackCompleted;
                }

                if (request.CallbackRequired != null)
                {
                    rec.CallbackRequired = request.CallbackRequired;
                }

                if (request.RecordStatus != null)
                {
                    rec.RecordStatus = request.RecordStatus;
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

        public List<HelpRequestEntity> GetHelpRequests()
        {
            try
            {
                return _helpRequestsContext.HelpRequestEntities.ToList();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetHelpRequests error: ");
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
                var response = _helpRequestsContext.HelpRequestEntities
                    .Where(x => (x.CallbackRequired == true || x.CallbackRequired == null ||
                                 (x.InitialCallbackCompleted == false && x.CallbackRequired == false)))
                    .Where(queryHelpNeeded)
                    .OrderByDescending(x => x.InitialCallbackCompleted)
                    .ThenBy(x => x.DateTimeRecorded)
                    .ToList();
                if (!string.IsNullOrWhiteSpace(requestParams.Master))
                {
                    return response.Where(x => x.RecordStatus != null && x.RecordStatus.ToUpper() == "MASTER").ToList();
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
