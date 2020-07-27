using System.Collections.Generic;
using System.Linq;
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

        public HelpRequest UpdateHelpRequest(HelpRequestEntity request)
        {
            _helpRequestsContext.HelpRequestEntities.Attach(request);
            _helpRequestsContext.SaveChanges();
            return request.ToDomain();
        }

        public List<HelpRequestEntity> GetHelpRequests()
        {
            return _helpRequestsContext.HelpRequestEntities.ToList();
        }

        public List<HelpRequestEntity> GetCallbacks()
        {
            return _helpRequestsContext.HelpRequestEntities.Where(x => (x.CallbackRequired == true || x.CallbackRequired == null)).ToList();
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
