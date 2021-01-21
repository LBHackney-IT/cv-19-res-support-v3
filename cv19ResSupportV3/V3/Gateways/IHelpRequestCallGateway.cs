using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.Gateways
{
    public interface IHelpRequestCallGateway
    {
        int CreateHelpRequestCall(int id, CreateHelpRequestCall request);
    }
}
