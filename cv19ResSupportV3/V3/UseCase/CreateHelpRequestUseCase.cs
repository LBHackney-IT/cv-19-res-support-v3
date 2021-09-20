using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class CreateHelpRequestUseCase : ICreateHelpRequestUseCase
    {
        private readonly IHelpRequestGateway _gateway;
        public CreateHelpRequestUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }
        public int Execute(int residentId, CreateHelpRequest command)
        {
            var helpRequestId = _gateway.FindHelpRequestByCtasId(command.NhsCtasId, command.HelpNeeded);
            if (helpRequestId != null) { return (int) helpRequestId; }

            helpRequestId = _gateway.FindHelpRequestByMetadata("nsss_id", command.Metadata);
            if (helpRequestId != null) { return (int) helpRequestId; }

            helpRequestId = _gateway.FindHelpRequestByMetadata("spl_id", command.Metadata);
            if (helpRequestId != null) { return (int) helpRequestId; }

            helpRequestId = _gateway.FindHelpRequestByMetadataAndResidentId("generic_ingestion_id", command.Metadata, residentId);
            if (helpRequestId != null) { return (int) helpRequestId; }

            return _gateway.CreateHelpRequest(residentId, command);
        }
    }
}
