using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class UpsertCallHandlerUseCase : IUpsertCallHandlerUseCase
    {
        private readonly ICallHandlerGateway _gateway;

        public UpsertCallHandlerUseCase(ICallHandlerGateway gateway)
        {
            _gateway = gateway;
        }

        public CallHandlerResponse Execute(CallHandlerCommand request)
            => request.Id.HasValue
            ? _gateway.UpdateCallHandler(request)
            : _gateway.CreateCallHandler(request);
    }
}
