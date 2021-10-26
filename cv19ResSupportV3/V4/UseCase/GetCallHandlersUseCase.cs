using System.Collections.Generic;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase.Interface;

namespace cv19ResSupportV3.V4.UseCase
{
    public class GetCallHandlersUseCase : IGetCallHandlersUseCase
    {
        private readonly ICallHandlerGateway _gateway;

        public GetCallHandlersUseCase(ICallHandlerGateway gateway)
        {
            _gateway = gateway;
        }
        
        public List<CallHandlerResponseBoundary> Execute()
        {
            var gwResponse = _gateway.GetCallHandlers();
            return gwResponse.ToResponse();
        }

        public CallHandlerResponseBoundary Execute(int id)
        {
            var gwResponse = _gateway.GetCallHandler(id);
            return gwResponse.ToResponse();
        }
    }
}
