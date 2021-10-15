using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;

namespace cv19ResSupportV3.V3.Gateways
{
    public interface ICallHandlerGateway
    {
        List<CallHandler> GetCallHandlers();

        CallHandler GetCallHandler(int id);

        CallHandler CreateCallHandler(CallHandler request);

        CallHandler UpdateCallHandler(CallHandler request);
    }
}
