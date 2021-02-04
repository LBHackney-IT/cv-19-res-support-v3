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
using Newtonsoft.Json;
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

        public int? FindHelpRequestByCtasId(string ctasId)
        {
            try
            {
                if (ctasId == null) return null;
                var helpRequestEntity = _helpRequestsContext.HelpRequestEntities.FirstOrDefault(x => x.NhsCtasId == ctasId);
                return helpRequestEntity?.Id;
            }
            catch (Exception e)
            {
                LambdaLogger.Log("FindHelpRequestByCtasId error: ");
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
                LambdaLogger.Log("GetLookups error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public HelpRequest UpdateHelpRequest(int id, UpdateHelpRequest command)
        {
            if (command == null) return null;
            try
            {
                var entity = _helpRequestsContext.HelpRequestEntities.Find(id);
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
            Expression<Func<HelpRequestEntity, bool>> queryPostcode = x =>
                string.IsNullOrWhiteSpace(command.Postcode)
                || x.ResidentEntity.Postcode.Replace(" ", "").ToUpper()
                    .Contains(command.Postcode.Replace(" ", "").ToUpper());

            Expression<Func<HelpRequestEntity, bool>> queryFirstName = x =>
                string.IsNullOrWhiteSpace(command.FirstName)
                || x.ResidentEntity.FirstName.Replace(" ", "").ToUpper()
                    .Contains(command.FirstName.Replace(" ", "").ToUpper());

            Expression<Func<HelpRequestEntity, bool>> queryLastName = x =>
                string.IsNullOrWhiteSpace(command.LastName)
                || x.ResidentEntity.LastName.Replace(" ", "").ToUpper()
                    .Contains(command.LastName.Replace(" ", "").ToUpper());

            Expression<Func<HelpRequestEntity, bool>> queryHelpNeeded = x =>
                string.IsNullOrWhiteSpace(command.HelpNeeded)
                || x.HelpNeeded.Replace(" ", "").ToUpper().Equals(command.HelpNeeded.Replace(" ", "").ToUpper());


            try
            {
                return _helpRequestsContext.HelpRequestEntities
                    .Include(x => x.HelpRequestCalls)
                    .Include(x => x.CaseNotes)
                    .Include(x => x.ResidentEntity)
                    .Where(queryPostcode)
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

        public HelpRequest PatchHelpRequest(int id, PatchHelpRequest command)
        {
            try
            {
                var rec = _helpRequestsContext.HelpRequestEntities.SingleOrDefault(x => x.Id == id);
                if (command == null)
                {
                    throw new Exception("Record not found.");
                }

                if (command.GettingInTouchReason != null)
                {
                    rec.GettingInTouchReason = command.GettingInTouchReason;
                }

                if (command.HelpWithAccessingFood != null)
                {
                    rec.HelpWithAccessingFood = command.HelpWithAccessingFood;
                }

                if (command.HelpWithAccessingSupermarketFood != null)
                {
                    rec.HelpWithAccessingSupermarketFood = command.HelpWithAccessingSupermarketFood;
                }

                if (command.HelpWithCompletingNssForm != null)
                {
                    rec.HelpWithCompletingNssForm = command.HelpWithCompletingNssForm;
                }

                if (command.HelpWithShieldingGuidance != null)
                {
                    rec.HelpWithShieldingGuidance = command.HelpWithShieldingGuidance;
                }

                if (command.HelpWithNoNeedsIdentified != null)
                {
                    rec.HelpWithNoNeedsIdentified = command.HelpWithNoNeedsIdentified;
                }

                if (command.HelpWithAccessingMedicine != null)
                {
                    rec.HelpWithAccessingMedicine = command.HelpWithAccessingMedicine;
                }

                if (command.HelpWithAccessingOtherEssentials != null)
                {
                    rec.HelpWithAccessingOtherEssentials = command.HelpWithAccessingOtherEssentials;
                }

                if (command.HelpWithDebtAndMoney != null)
                {
                    rec.HelpWithDebtAndMoney = command.HelpWithDebtAndMoney;
                }

                if (command.HelpWithMentalHealth != null)
                {
                    rec.HelpWithMentalHealth = command.HelpWithMentalHealth;
                }

                if (command.HelpWithHealth != null)
                {
                    rec.HelpWithHealth = command.HelpWithHealth;
                }

                if (command.HelpWithAccessingInternet != null)
                {
                    rec.HelpWithAccessingInternet = command.HelpWithAccessingInternet;
                }

                if (command.HelpWithSomethingElse != null)
                {
                    rec.HelpWithSomethingElse = command.HelpWithSomethingElse;
                }

                if (command.CurrentSupport != null)
                {
                    rec.CurrentSupport = command.CurrentSupport;
                }

                if (command.CurrentSupportFeedback != null)
                {
                    rec.CurrentSupportFeedback = command.CurrentSupportFeedback;
                }

                if (command.AdviceNotes != null)
                {
                    rec.AdviceNotes = command.AdviceNotes;
                }

                if (command.InitialCallbackCompleted != null)
                {
                    rec.InitialCallbackCompleted = command.InitialCallbackCompleted;
                }

                if (command.CallbackRequired != null)
                {
                    rec.CallbackRequired = command.CallbackRequired;
                }

                if (command.NhsCtasId != null)
                {
                    rec.NhsCtasId = command.NhsCtasId;
                }

                if (command.AssignedTo != null)
                {
                    rec.AssignedTo = command.AssignedTo;
                }

                if (command.HelpNeeded != null)
                {
                    rec.HelpNeeded = command.HelpNeeded;
                }

                _helpRequestsContext.SaveChanges();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("PatchResidentAndHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }

            return _helpRequestsContext.HelpRequestEntities.Find(id).ToDomain();
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

        public List<HelpRequestWithResident> GetResidentHelpRequests(int id)
        {
            try
            {
                var helpRequests = _helpRequestsContext.HelpRequestEntities
                    .Include(x => x.HelpRequestCalls)
                    .Include(x => x.ResidentEntity)
                    .Include(x => x.CaseNotes)
                    .Where(x => x.ResidentId == id);
                return helpRequests.Select(hr => hr.ToHelpRequestWithResidentDomain()).ToList();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetResidentHelpRequests error: ");
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
