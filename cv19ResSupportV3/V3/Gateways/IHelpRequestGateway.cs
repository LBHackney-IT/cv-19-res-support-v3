using cv19ResRupportV3.V3.Domain;
using cv19ResRupportV3.V3.Infrastructure;

namespace cv19ResRupportV3.V3.Gateways
{
    public interface IHelpRequestGateway
    {
        int CreateHelpRequest(HelpRequestEntity request);
    }
}
