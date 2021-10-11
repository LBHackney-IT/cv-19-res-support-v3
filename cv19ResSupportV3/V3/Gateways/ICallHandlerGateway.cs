using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.Gateways
{
    public interface ICallHandlerGateway
    {
        List<CallHandler> GetCallHandlers();
    }
}
