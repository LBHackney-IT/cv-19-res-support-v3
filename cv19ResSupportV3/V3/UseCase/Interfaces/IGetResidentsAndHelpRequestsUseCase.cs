using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.UseCase
{
    public interface IGetResidentsAndHelpRequestsUseCase
    {
        List<HelpRequestResponse> Execute(SearchRequest command);
    }
}
