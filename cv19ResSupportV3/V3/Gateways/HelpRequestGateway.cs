using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Amazon.Lambda.Core;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Domain.Queries;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Factories.Commands;
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

        public int CreateHelpRequest(int residentId, CreateHelpRequest command)
        {
            var requestEntity = command.ToEntity();
            if (requestEntity == null) return 0;

            requestEntity.ResidentId = residentId;

            // SetRecordStatus(requestEntity);
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
        public Resident CreateResident(CreateResident command)
        {
            var requestEntity = command.ToResidentEntity();
            if (requestEntity == null) return null;

            try
            {
                _helpRequestsContext.ResidentEntities.Add(requestEntity);
                _helpRequestsContext.SaveChanges();
                var resident = _helpRequestsContext.ResidentEntities.Find(requestEntity.Id);
                return resident.ToResidentDomain();

            }
            catch (Exception e)
            {
                LambdaLogger.Log("CreateHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }
        public Resident UpdateResident(int residentId, UpdateResident command)
        {
            try
            {
                var requestEntity = _helpRequestsContext.ResidentEntities.Find(residentId);
                _helpRequestsContext.Entry(requestEntity).CurrentValues.SetValues(command);
                _helpRequestsContext.SaveChanges();
                var updatedResidentEntity = _helpRequestsContext.ResidentEntities.Find(residentId);
                return updatedResidentEntity.ToResidentDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("UpdateResident error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }

        }
        public int? FindResident(FindResident command)
        {
            try
            {
                if (command.Uprn != null)
                {
                    var response = _helpRequestsContext.ResidentEntities.FirstOrDefault(x => x.Uprn == command.Uprn &&
                                                                                             x.FirstName ==
                                                                                             command.FirstName &&
                                                                                             x.LastName ==
                                                                                             command.LastName);
                    if (response != null)
                    {
                        return response.Id;
                    }
                }

                if (command.DobDay != null || command.DobMonth != null || command.DobYear != null)
                {
                    var response = _helpRequestsContext.ResidentEntities.FirstOrDefault(x =>
                        x.DobDay == command.DobDay &&
                        x.DobMonth == command.DobMonth &&
                        x.DobYear == command.DobYear &&
                        x.FirstName == command.FirstName &&
                        x.LastName == command.LastName);
                    if (response != null)
                    {
                        return response.Id;
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                LambdaLogger.Log("FindResident error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public List<LookupDomain> GetLookups(LookupQuery command)
        {
            Expression<Func<LookupEntity, bool>> queryLookups = x =>
                string.IsNullOrWhiteSpace(command.LookupGroup)
                || x.LookupGroup.Replace(" ", "").ToUpper().Equals(command.LookupGroup.Replace(" ", "").ToUpper());
            try
            {
                var response = _helpRequestsContext.Lookups
                    .Where(queryLookups)
                    .OrderBy(x => x.LookupGroup)
                    .ThenBy(x => x.Lookup)
                    .ToList();
                return response.ToDomain();
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
                //                var entity = _helpRequestsContext.HelpRequestEntities.Find(command.Id);
                //                _helpRequestsContext.Entry(entity).CurrentValues.SetValues(command);
                //                _helpRequestsContext.SaveChanges();
                //                return entity.ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("UpdateHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
            return new HelpRequest();
        }

        public Resident GetResident(int id)
        {
            try
            {
                var resident = _helpRequestsContext.ResidentEntities
                    .Include(x => x.CaseNotes)
                    .FirstOrDefault(x => x.Id == id);
                return resident?.ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetResident error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public HelpRequestWithResident GetHelpRequest(int id)
        {
            try
            {
                var helpRequest = _helpRequestsContext.HelpRequestEntities
                    .Include(x => x.HelpRequestCalls)
                    .Include(x => x.ResidentEntity)
                    .Include(x => x.CaseNotes)
                    .FirstOrDefault(x => x.Id == id);
                return helpRequest?.ToHelpRequestWithResidentDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetResidentAndHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public List<HelpRequestWithResident> SearchHelpRequests(SearchRequest command)
        {
            Expression<Func<HelpRequestEntity, bool>> queryPostCode = x =>
                string.IsNullOrWhiteSpace(command.Postcode)
                || x.ResidentEntity.PostCode.Replace(" ", "").ToUpper().Contains(command.Postcode.Replace(" ", "").ToUpper());

            Expression<Func<HelpRequestEntity, bool>> queryFirstName = x =>
                string.IsNullOrWhiteSpace(command.FirstName)
                || x.ResidentEntity.FirstName.Replace(" ", "").ToUpper().Contains(command.FirstName.Replace(" ", "").ToUpper());

            Expression<Func<HelpRequestEntity, bool>> queryLastName = x =>
                string.IsNullOrWhiteSpace(command.LastName)
                || x.ResidentEntity.LastName.Replace(" ", "").ToUpper().Contains(command.LastName.Replace(" ", "").ToUpper());

            Expression<Func<HelpRequestEntity, bool>> queryHelpNeeded = x =>
                string.IsNullOrWhiteSpace(command.HelpNeeded)
                || x.HelpNeeded.Replace(" ", "").ToUpper().Equals(command.HelpNeeded.Replace(" ", "").ToUpper());


            try
            {
                return _helpRequestsContext.HelpRequestEntities
                    .Include(x => x.HelpRequestCalls)
                    .Include(x => x.CaseNotes)
                    .Include(x => x.ResidentEntity)
                    .Where(queryPostCode)
                    .Where(queryFirstName)
                    .Where(queryLastName)
                    .Where(queryHelpNeeded)
                    .ToList()
                    .ToHelpRequestWithResidentDomain();
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
                //                var requestEntity = command.ToEntity();
                //                var rec = _helpRequestsContext.HelpRequestEntities.SingleOrDefault(x => x.Id == id);
                //                if (requestEntity == null)
                //                {
                //                    throw new Exception("Record not found.");
                //                }
                //                if (requestEntity.GettingInTouchReason != null)
                //                {
                //                    rec.GettingInTouchReason = requestEntity.GettingInTouchReason;
                //                }
                //                if (requestEntity.HelpWithAccessingFood != null)
                //                {
                //                    rec.HelpWithAccessingFood = requestEntity.HelpWithAccessingFood;
                //                }
                //                if (requestEntity.HelpWithAccessingSupermarketFood != null)
                //                {
                //                    rec.HelpWithAccessingSupermarketFood = requestEntity.HelpWithAccessingSupermarketFood;
                //                }
                //                if (requestEntity.HelpWithCompletingNssForm != null)
                //                {
                //                    rec.HelpWithCompletingNssForm = requestEntity.HelpWithCompletingNssForm;
                //                }
                //                if (requestEntity.HelpWithShieldingGuidance != null)
                //                {
                //                    rec.HelpWithShieldingGuidance = requestEntity.HelpWithShieldingGuidance;
                //                }
                //                if (requestEntity.HelpWithNoNeedsIdentified != null)
                //                {
                //                    rec.HelpWithNoNeedsIdentified = requestEntity.HelpWithNoNeedsIdentified;
                //                }
                //                if (requestEntity.HelpWithAccessingMedicine != null)
                //                {
                //                    rec.HelpWithAccessingMedicine = requestEntity.HelpWithAccessingMedicine;
                //                }
                //                if (requestEntity.HelpWithAccessingOtherEssentials != null)
                //                {
                //                    rec.HelpWithAccessingOtherEssentials = requestEntity.HelpWithAccessingOtherEssentials;
                //                }
                //                if (requestEntity.HelpWithDebtAndMoney != null)
                //                {
                //                    rec.HelpWithDebtAndMoney = requestEntity.HelpWithDebtAndMoney;
                //                }
                //                if (requestEntity.HelpWithMentalHealth != null)
                //                {
                //                    rec.HelpWithMentalHealth = requestEntity.HelpWithMentalHealth;
                //                }
                //                if (requestEntity.HelpWithHealth != null)
                //                {
                //                    rec.HelpWithHealth = requestEntity.HelpWithHealth;
                //                }
                //                if (requestEntity.HelpWithAccessingInternet != null)
                //                {
                //                    rec.HelpWithAccessingInternet = requestEntity.HelpWithAccessingInternet;
                //                }
                //                if (requestEntity.HelpWithSomethingElse != null)
                //                {
                //                    rec.HelpWithSomethingElse = requestEntity.HelpWithSomethingElse;
                //                }
                //                if (requestEntity.CurrentSupport != null)
                //                {
                //                    rec.CurrentSupport = requestEntity.CurrentSupport;
                //                }
                //
                //                if (requestEntity.CurrentSupportFeedback != null)
                //                {
                //                    rec.CurrentSupportFeedback = requestEntity.CurrentSupportFeedback;
                //                }
                //
                //                if (requestEntity.FirstName != null)
                //                {
                //                    rec.FirstName = requestEntity.FirstName;
                //                }
                //
                //                if (requestEntity.LastName != null)
                //                {
                //                    rec.LastName = requestEntity.LastName;
                //                }
                //
                //                if (requestEntity.DobMonth != null)
                //                {
                //                    rec.DobMonth = requestEntity.DobMonth;
                //                }
                //
                //                if (requestEntity.DobYear != null)
                //                {
                //                    rec.DobYear = requestEntity.DobYear;
                //                }
                //
                //                if (requestEntity.DobDay != null)
                //                {
                //                    rec.DobDay = requestEntity.DobDay;
                //                }
                //
                //                if (requestEntity.ContactTelephoneNumber != null)
                //                {
                //                    rec.ContactTelephoneNumber = requestEntity.ContactTelephoneNumber;
                //                }
                //
                //                if (requestEntity.ContactMobileNumber != null)
                //                {
                //                    rec.ContactMobileNumber = requestEntity.ContactMobileNumber;
                //                }
                //
                //                if (requestEntity.EmailAddress != null)
                //                {
                //                    rec.EmailAddress = requestEntity.EmailAddress;
                //                }
                //
                //                if (requestEntity.AddressFirstLine != null && requestEntity.PostCode != null)
                //                {
                //                    // update new address fields
                //                    rec.AddressFirstLine = requestEntity.AddressFirstLine;
                //                    rec.AddressSecondLine = requestEntity.AddressSecondLine;
                //                    rec.AddressThirdLine = requestEntity.AddressThirdLine;
                //                    rec.PostCode = requestEntity.PostCode;
                //                    rec.Uprn = requestEntity.Uprn;
                //                    rec.Ward = requestEntity.Ward;
                //                }
                //
                //                if (requestEntity.GpSurgeryDetails != null)
                //                {
                //                    rec.GpSurgeryDetails = requestEntity.GpSurgeryDetails;
                //                }
                //
                //                if (requestEntity.NumberOfChildrenUnder18 != null)
                //                {
                //                    rec.NumberOfChildrenUnder18 = requestEntity.NumberOfChildrenUnder18;
                //                }
                //
                //                if (requestEntity.ConsentToShare != null)
                //                {
                //                    rec.ConsentToShare = requestEntity.ConsentToShare;
                //                }
                //
                //                if (requestEntity.CaseNotes != null)
                //                {
                //                    rec.CaseNotes = requestEntity.CaseNotes;
                //                }
                //
                //                if (requestEntity.AdviceNotes != null)
                //                {
                //                    rec.AdviceNotes = requestEntity.AdviceNotes;
                //                }
                //
                //                if (requestEntity.InitialCallbackCompleted != null)
                //                {
                //                    rec.InitialCallbackCompleted = requestEntity.InitialCallbackCompleted;
                //                }
                //
                //                if (requestEntity.CallbackRequired != null)
                //                {
                //                    rec.CallbackRequired = requestEntity.CallbackRequired;
                //                }
                //
                //                if (requestEntity.RecordStatus != null)
                //                {
                //                    rec.RecordStatus = requestEntity.RecordStatus;
                //                }
                //
                //                if (requestEntity.HelpNeeded != null)
                //                {
                //                    rec.HelpNeeded = requestEntity.HelpNeeded;
                //                }
                //                _helpRequestsContext.SaveChanges();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("PatchHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public List<HelpRequestWithResident> GetCallbacks(CallbackQuery command)
        {
            Expression<Func<HelpRequestEntity, bool>> queryHelpNeeded = x =>
                string.IsNullOrWhiteSpace(command.HelpNeeded)
                || x.HelpNeeded.Replace(" ", "").ToUpper().Equals(command.HelpNeeded.Replace(" ", "").ToUpper());
            try
            {
                var response = _helpRequestsContext.HelpRequestEntities.Include(x => x.HelpRequestCalls)
                    .Include(x => x.ResidentEntity)
                    .Include(x => x.CaseNotes)
                    .Where(x => (x.CallbackRequired == true || x.CallbackRequired == null ||
                                 (x.InitialCallbackCompleted == false && x.CallbackRequired == false)))
                    .Where(queryHelpNeeded)
                    .OrderByDescending(x => x.InitialCallbackCompleted)
                    .ThenBy(x => x.DateTimeRecorded)
                    .ToList();
                return response.ToHelpRequestWithResidentDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetCallbacks error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        //        private void SetRecordStatus(HelpRequestEntityOld request)
        //        {
        //            request.RecordStatus = "MASTER";
        //            var duplicates = _helpRequestsContext.HelpRequestEntities
        //                .Where(x => x.Uprn == request.Uprn && x.DobMonth == request.DobMonth
        //                                                   && x.DobDay == request.DobDay && x.DobYear == request.DobYear &&
        //                                                   x.ContactTelephoneNumber == request.ContactTelephoneNumber &&
        //                                                   x.ContactMobileNumber == request.ContactMobileNumber).ToList();
        //            if (duplicates.Count > 0)
        //            {
        //                foreach (var record in duplicates)
        //                {
        //                    var rec = _helpRequestsContext.HelpRequestEntities.Find(record.Id);
        //                    rec.RecordStatus = "DUPLICATE";
        //                }
        //            }
        //            else
        //            {
        //                var exceptions = _helpRequestsContext.HelpRequestEntities
        //                    .Where(x => x.Uprn == request.Uprn).ToList();
        //                if (exceptions.Count > 0)
        //                {
        //                    request.RecordStatus = "EXCEPTION";
        //                }
        //            }
        //        }
    }
}
