using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Amazon.Lambda.Core;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V4.Helpers;
using Microsoft.EntityFrameworkCore;

namespace cv19ResSupportV3.V3.Gateways
{
    public class ResidentGateway : IResidentGateway
    {
        private readonly HelpRequestsContext _helpRequestsContext;

        public ResidentGateway(HelpRequestsContext helpRequestsContext)
        {
            _helpRequestsContext = helpRequestsContext;
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

        public Resident PatchResident(int id, PatchResident command)
        {
            try
            {
                var rec = _helpRequestsContext.ResidentEntities.SingleOrDefault(x => x.Id == id);
                if (command == null)
                {
                    throw new Exception("Record not found.");
                }

                if (command.FirstName != null)
                {
                    rec.FirstName = command.FirstName;
                }

                if (command.LastName != null)
                {
                    rec.LastName = command.LastName;
                }

                if (command.DobMonth != null)
                {
                    rec.DobMonth = command.DobMonth;
                }

                if (command.DobYear != null)
                {
                    rec.DobYear = command.DobYear;
                }

                if (command.DobDay != null)
                {
                    rec.DobDay = command.DobDay;
                }

                if (command.ContactTelephoneNumber != null)
                {
                    rec.ContactTelephoneNumber = command.ContactTelephoneNumber;
                }

                if (command.ContactMobileNumber != null)
                {
                    rec.ContactMobileNumber = command.ContactMobileNumber;
                }

                if (command.EmailAddress != null)
                {
                    rec.EmailAddress = command.EmailAddress;
                }

                if (command.AddressFirstLine != null && command.Postcode != null)
                {
                    // update new address fields
                    rec.AddressFirstLine = command.AddressFirstLine;
                    rec.AddressSecondLine = command.AddressSecondLine;
                    rec.AddressThirdLine = command.AddressThirdLine;
                    rec.Postcode = command.Postcode;
                    rec.Uprn = command.Uprn;
                    rec.Ward = command.Ward;
                }

                if (command.GpSurgeryDetails != null)
                {
                    rec.GpSurgeryDetails = command.GpSurgeryDetails;
                }

                if (command.IsPharmacistAbleToDeliver != null)
                {
                    rec.IsPharmacistAbleToDeliver = command.IsPharmacistAbleToDeliver;
                }

                if (command.NameAddressPharmacist != null)
                {
                    rec.NameAddressPharmacist = command.NameAddressPharmacist;
                }

                if (command.NumberOfChildrenUnder18 != null)
                {
                    rec.NumberOfChildrenUnder18 = command.NumberOfChildrenUnder18;
                }

                if (command.NhsNumber != null)
                {
                    rec.NhsNumber = command.NhsNumber;
                }

                if (command.ConsentToShare != null)
                {
                    rec.ConsentToShare = command.ConsentToShare;
                }

                if (command.RecordStatus != null)
                {
                    rec.RecordStatus = command.RecordStatus;
                }

                _helpRequestsContext.SaveChanges();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("PatchResidentAndHelpRequest error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }

            return _helpRequestsContext.ResidentEntities.Find(id).ToDomain();
        }

        public int? FindResident(FindResident command)
        {
            try
            {
                if (Predicates.IsNotNullAndNotEmpty(command.NhsNumber))
                {
                    var matchingResident = _helpRequestsContext.ResidentEntities
                        .FirstOrDefault(r => r.NhsNumber.Replace(" ", "") == command.NhsNumber.Replace(" ", ""));

                    if (matchingResident != null)
                        return matchingResident.Id;
                }

                // If Fname or Lname are missing, no point checking Uprn or Dob. When these two fields
                // are missing, there's no way to confirm whether it's the same person or not (unless nhs numbers match).
                if (Predicates.IsNotNullAndNotEmpty(command.FirstName) &&
                    Predicates.IsNotNullAndNotEmpty(command.LastName))
                {
                    if (Predicates.IsNotNullAndNotEmpty(command.Uprn))
                    {
                        var matchingResident = _helpRequestsContext.ResidentEntities
                            .FirstOrDefault(r =>
                                //uprn is all numbers, no need to change case
                                r.Uprn.Trim() == command.Uprn.Trim() &&
                                r.FirstName.Trim().ToUpper() == command.FirstName.Trim().ToUpper() &&
                                r.LastName.Trim().ToUpper() == command.LastName.Trim().ToUpper());

                        if (matchingResident != null)
                            return matchingResident.Id;
                    }

                    // Will ignore cases, where for instance DobYear and DobMonth are not empty, but DobDay is empty
                    // which I believe is a sensible result, as we can't guarantee that's the same person: there are
                    // 28 to 31 combinations of the Dob, when only those 2 fields are given.
                    if (Predicates.IsNotNullAndNotEmpty(command.DobYear) &&
                        Predicates.IsNotNullAndNotEmpty(command.DobMonth) &&
                        Predicates.IsNotNullAndNotEmpty(command.DobDay))
                    {
                        var matchingResident = _helpRequestsContext.ResidentEntities
                            .FirstOrDefault(r =>
                                r.DobYear.Trim() == command.DobYear.Trim() &&
                                // adding .ToUpper here in case month is specified with alphabetic characters for some cases (Jan, Dec)
                                r.DobMonth.Trim().TrimStart('0').ToUpper() == command.DobMonth.Trim().TrimStart('0').ToUpper() &&
                                r.DobDay.Trim().TrimStart('0') == command.DobDay.Trim().TrimStart('0') &&
                                r.FirstName.Trim().ToUpper() == command.FirstName.Trim().ToUpper() &&
                                r.LastName.Trim().ToUpper() == command.LastName.Trim().ToUpper());

                        if (matchingResident != null)
                            return matchingResident.Id;
                    }

                    if (Predicates.IsNotNullAndNotEmpty(command.NhsCtasId))
                    {
                        var matchingResident = _helpRequestsContext.ResidentEntities
                            .Include(r => r.HelpRequests)
                            .AsEnumerable()
                            .FirstOrDefault(r =>
                                r.HelpRequests.Exists(hr => hr.NhsCtasId.ToUpper() == command.NhsCtasId.ToUpper()) &&
                                r.FirstName.Trim().ToUpper() == command.FirstName.Trim().ToUpper() &&
                                r.LastName.Trim().ToUpper() == command.LastName.Trim().ToUpper());

                        if (matchingResident != null)
                            return matchingResident.Id;
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

        public List<Resident> SearchResidents(FindResident command)
        {
            Expression<Func<ResidentEntity, bool>> queryFirstName = x =>
                string.IsNullOrWhiteSpace(command.FirstName)
                || x.FirstName.Replace(" ", "").ToUpper().Equals(command.FirstName.Replace(" ", "").ToUpper());

            Expression<Func<ResidentEntity, bool>> queryLastName = x =>
                string.IsNullOrWhiteSpace(command.LastName)
                || x.LastName.Replace(" ", "").ToUpper().Equals(command.LastName.Replace(" ", "").ToUpper());

            Expression<Func<ResidentEntity, bool>> queryDobMonth = x =>
                string.IsNullOrWhiteSpace(command.DobMonth)
                || x.DobMonth.Replace(" ", "").ToUpper().Equals(command.DobMonth.Replace(" ", "").ToUpper());

            Expression<Func<ResidentEntity, bool>> queryDobDay = x =>
                string.IsNullOrWhiteSpace(command.DobDay)
                || x.DobDay.Replace(" ", "").ToUpper().Equals(command.DobDay.Replace(" ", "").ToUpper());

            Expression<Func<ResidentEntity, bool>> queryDobYear = x =>
                 string.IsNullOrWhiteSpace(command.DobYear)
                 || x.DobYear.Replace(" ", "").ToUpper().Equals(command.DobYear.Replace(" ", "").ToUpper());

            Expression<Func<ResidentEntity, bool>> queryPostcode = x =>
                string.IsNullOrWhiteSpace(command.Postcode)
                || x.Postcode.Replace(" ", "").ToUpper().Equals(command.Postcode.Replace(" ", "").ToUpper());

            Expression<Func<ResidentEntity, bool>> queryUprn = x =>
                string.IsNullOrWhiteSpace(command.Uprn)
                || x.Uprn.Replace(" ", "").ToUpper().Equals(command.Uprn.Replace(" ", "").ToUpper());
            try
            {
                var response = _helpRequestsContext.ResidentEntities
                    .Where(queryFirstName)
                    .Where(queryLastName)
                    .Where(queryDobDay)
                    .Where(queryDobMonth)
                    .Where(queryDobYear)
                    .Where(queryUprn)
                    .Where(queryPostcode)
                    .Select(re => re.ToDomain()).ToList();
                return response;
            }
            catch (Exception e)
            {
                LambdaLogger.Log("SearchResident error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public Resident UpdateResident(int residentId, UpdateResident command)
        {
            try
            {
                var entity = _helpRequestsContext.ResidentEntities.Find(residentId);
                _helpRequestsContext.Entry(entity).CurrentValues.SetValues(command);
                _helpRequestsContext.SaveChanges();
                return entity.ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("UpdateResident error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }
    }
}
