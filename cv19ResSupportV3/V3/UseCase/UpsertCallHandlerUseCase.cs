using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using cv19ResSupportV3.V4;
using cv19ResSupportV3.V4.Factories;

namespace cv19ResSupportV3.V3.UseCase
{
    public class UpsertCallHandlerUseCase : IUpsertCallHandlerUseCase
    {
        private readonly ICallHandlerGateway _gateway;

        public UpsertCallHandlerUseCase(ICallHandlerGateway gateway)
        {
            _gateway = gateway;
        }

        public CallHandler Execute(CallHandlerRequestBoundary request)
            => request.Id.HasValue
            ? _gateway.UpdateCallHandler(request.ToDomain())
            : _gateway.CreateCallHandler(request.ToDomain());
    }
}
