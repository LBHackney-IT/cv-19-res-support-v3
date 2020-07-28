using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            _helpRequestsContext.HelpRequestEntities.Add(request);
            _helpRequestsContext.SaveChanges();
            return request.Id;
        }

        public HelpRequestEntity UpdateHelpRequest(HelpRequestEntity request)
        {
            _helpRequestsContext.HelpRequestEntities.Attach(request);
            _helpRequestsContext.SaveChanges();
            return request;
        }

        public HelpRequestEntity GetHelpRequest(int id)
        {
            return _helpRequestsContext.HelpRequestEntities
                .FirstOrDefault(x => x.Id == id);
        }

        public List<HelpRequestEntity> SearchHelpRequests(string queryParamsPostCode)
        {
            Expression<Func<HelpRequestEntity, bool>> queryPostCode = x =>
                string.IsNullOrWhiteSpace(queryParamsPostCode)
                || x.PostCode.Replace(" ", "").ToUpper().Contains(queryParamsPostCode.Replace(" ", "").ToUpper());
            return _helpRequestsContext.HelpRequestEntities
                .Where(queryPostCode)
                .ToList();
        }

        public void PatchHelpRequest(int id, HelpRequestEntity request)
        {
            Console.WriteLine($"*************** Id is {id}");
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
            _helpRequestsContext.SaveChanges();
        }

        public List<HelpRequestEntity> GetHelpRequests()
        {
            return _helpRequestsContext.HelpRequestEntities.ToList();
        }

        public List<HelpRequestEntity> GetCallbacks()
        {
            return _helpRequestsContext.HelpRequestEntities
                .Where(x => (x.CallbackRequired == true || x.CallbackRequired == null)
                            && x.DateTimeRecorded <= DateTime.Today.AddDays(-1))
                .OrderBy(x => x.InitialCallbackCompleted)
                .ThenBy(x => x.DateTimeRecorded)
                .ToList();
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
